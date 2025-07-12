using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text collectedPuzzlePiecesText;
    [SerializeField] private float buttonStartPositionY = -1500f;
    [SerializeField] private float buttonHeight = 50f;
    [SerializeField] private float buttonAnimationDelay = 0.2f;
    [SerializeField] private int maxButtons = 10;
    [SerializeField] private RectTransform characterActionsPanel;
    [SerializeField] private GameObject characterActionPrefab;
    public List<ActionButton> characterActionButtons = new List<ActionButton>();
    private int currentCharacterActionIndex = 0;
    [SerializeField] private RectTransform levelActionsPanel;
    [SerializeField] private GameObject levelActionPrefab;
    public List<ActionButton> levelActionButtons = new List<ActionButton>();
    private int currentLevelActionIndex = 0;
    private Level currentLevel;

    public void InitLevel(Level _level)
    {
        if (_level == null)
        {
            Debug.LogError("Level is null in GameUIController.InitLevel");
            return;
        }

        currentLevel = _level;

        currentCharacterActionIndex = 0;
        currentLevelActionIndex = 0;

        ClearActionButtons(characterActionButtons);
        ClearActionButtons(levelActionButtons);

        foreach (ActionData action in currentLevel.CharacterActions)
        {
            if (characterActionButtons.Count >= maxButtons)
                break;

            CreateButton(action, true);
            currentCharacterActionIndex++;
        }

        foreach (ActionData action in currentLevel.LevelActions)
        {
            if (levelActionButtons.Count >= maxButtons)
                break;

            CreateButton(action, false);
            currentLevelActionIndex++;
        }

        UpdatePuzzlePieceCount(new List<PuzzleColor>()); // Initialize with empty list

        UpdateButtonPositions(false);
    }

    private void CreateButton(ActionData _action, bool _characterActionButtons)
    {
        List<ActionButton> currentList = _characterActionButtons ? characterActionButtons : levelActionButtons;
        RectTransform currentPanel = _characterActionButtons ? characterActionsPanel : levelActionsPanel;
        GameObject currentPrefab = _characterActionButtons ? characterActionPrefab : levelActionPrefab;

        ActionButton button = Instantiate(currentPrefab, currentPanel).GetComponent<ActionButton>();
        button.GetRect().anchoredPosition = new Vector2(0, buttonStartPositionY);
        currentList.Add(button);
        button.SetAction(_action.action, _action.actionName, _action.infiniteUse, _characterActionButtons);
    }

    public void ButtonDestroyed(ActionButton _button)
    {
        if (characterActionButtons.Contains(_button))
        {
            characterActionButtons.Remove(_button);

            if (currentLevel != null && currentCharacterActionIndex < currentLevel.CharacterActions.Count)
            {
                ActionData nextAction = currentLevel.CharacterActions[currentCharacterActionIndex];
                CreateButton(nextAction, true);
                currentCharacterActionIndex++;
            }
        }

        else if (levelActionButtons.Contains(_button))
        {
            levelActionButtons.Remove(_button);

            if (currentLevel != null && currentLevelActionIndex < currentLevel.LevelActions.Count)
            {
                ActionData nextAction = currentLevel.LevelActions[currentLevelActionIndex];
                CreateButton(nextAction, false);
                currentLevelActionIndex++;
            }
        }

        UpdateButtonPositions(true);
    }

    public void UpdateButtonPositions(bool _fromCurrentPosition)
    {
        float height = canvas.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < characterActionButtons.Count; i++)
        {
            float targetY = CalculateButtonPositionY(i, characterActionButtons.Count, height);
            StartCoroutine(StartAnimationWithDelay(characterActionButtons[i], buttonAnimationDelay * characterActionButtons.Count, targetY, _fromCurrentPosition));
        }

        for (int i = 0; i < levelActionButtons.Count; i++)
        {
            float targetY = CalculateButtonPositionY(i, levelActionButtons.Count, height);
            StartCoroutine(StartAnimationWithDelay(levelActionButtons[i], buttonAnimationDelay * levelActionButtons.Count, targetY, _fromCurrentPosition));
        }
    }

    private float CalculateButtonPositionY(int _index, int _maxButtons, float _totalHeight)
    {
        float totalButtonHeight = buttonHeight * _maxButtons;
        float spacing = (_totalHeight - totalButtonHeight) / (_maxButtons + 1);
        float topY = spacing * (_index + 1) + (buttonHeight * _index);
        return topY + (buttonHeight / 2) - _totalHeight / 2;
    }

    IEnumerator StartAnimationWithDelay(ActionButton _button, float _delay, float _targetHeight, bool _fromCurrentPosition)
    {
        if (!_fromCurrentPosition)
            yield return new WaitForSeconds(_delay);

        AnimateToPosition animateToPosition = _button.GetComponent<AnimateToPosition>();
        if (animateToPosition != null)
        {
            animateToPosition.StartAnimation(_button.GetRect().anchoredPosition.y, _targetHeight);
        }
    }

    private void ClearActionButtons(List<ActionButton> _actionButtons)
    {
        foreach (var button in _actionButtons)
        {
            Destroy(button.gameObject);
        }
        _actionButtons.Clear();
    }

    public void UpdatePuzzlePieceCount(List<PuzzleColor> _puzzlePieces)
    {
        if (collectedPuzzlePiecesText != null)
        {
            collectedPuzzlePiecesText.text = _puzzlePieces.Count + " / 4";
        }
        else
        {
            Debug.LogError("collectedPuzzlePiecesText is not assigned in GameUIController");
        }
    }
}
