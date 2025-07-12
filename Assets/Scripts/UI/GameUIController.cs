using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private RectTransform characterActionsPanel;
    [SerializeField] private GameObject characterActionPrefab;
    [SerializeField] private float startButtonHeight = 50f;
    [SerializeField] private float buttonHeight = 50f;
    [SerializeField] private float buttonSpacing = 10f;
    [SerializeField] private float buttonAnimationDelay = 0.2f;
    [SerializeField] private int maxButtons = 10;
    public List<ActionButton> characterActionButtons = new List<ActionButton>();
    [SerializeField] private RectTransform levelActionsPanel;
    [SerializeField] private GameObject levelActionPrefab;
    public List<ActionButton> levelActionButtons = new List<ActionButton>();

    public void InitLevel(Level _level)
    {
        if (_level == null)
        {
            Debug.LogError("Level is null in GameUIController.InitLevel");
            return;
        }

        ClearActionButtons(characterActionButtons);
        ClearActionButtons(levelActionButtons);

        foreach (var action in _level.CharacterActions)
        {
            if (characterActionButtons.Count >= maxButtons)
            {
                break;
            }

            CreateButton(action, characterActionsPanel, characterActionPrefab, characterActionButtons);
        }

        foreach (var action in _level.LevelActions)
        {
            if (levelActionButtons.Count >= maxButtons)
            {
                break;
            }

            CreateButton(action, levelActionsPanel, levelActionPrefab, levelActionButtons);
        }
    }

    private ActionButton CreateButton(LevelActions _action, RectTransform _parentPanel, GameObject _actionPrefab, List<ActionButton> _targetList)
    {
        var button = Instantiate(_actionPrefab, _parentPanel).GetComponent<ActionButton>();
        RectTransform buttonRect = button.GetRect();
        buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, -startButtonHeight * characterActionButtons.Count);
        button.SetAction(_action.action, _action.actionName);
        _targetList.Add(button);

        AnimateToPosition animateToPosition = button.GetComponent<AnimateToPosition>();
        if (animateToPosition != null)
        {
            float targetHeight = buttonHeight + buttonSpacing * _targetList.Count;
            StartCoroutine(StartAnimationWithDelay(button, buttonAnimationDelay * _targetList.Count, targetHeight));
        }

        return button;
    }

    IEnumerator StartAnimationWithDelay(ActionButton _button, float _delay, float _targetHeight)
    {
        yield return new WaitForSeconds(_delay);
        AnimateToPosition animateToPosition = _button.GetComponent<AnimateToPosition>();
        if (animateToPosition != null)
        {
            animateToPosition.StartAnimation(_targetHeight);
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
}
