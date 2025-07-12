using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private float startAnimatingFromY = 50f;
    [SerializeField] private float buttonStartPositionY = -1500f;
    [SerializeField] private float buttonHeight = 50f;
    [SerializeField] private float buttonSpacing = 10f;
    [SerializeField] private float buttonAnimationDelay = 0.2f;
    [SerializeField] private int maxButtons = 10;
    [SerializeField] private RectTransform characterActionsPanel;
    [SerializeField] private GameObject characterActionPrefab;
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

        foreach (ActionData action in _level.CharacterActions)
        {
            if (characterActionButtons.Count >= maxButtons)
                break;

            CreateButton(action, true);
        }

        foreach (ActionData action in _level.LevelActions)
        {
            if (levelActionButtons.Count >= maxButtons)
                break;

            CreateButton(action, false);
        }
    }

    private void CreateButton(ActionData _action, bool _characterActionButtons)
    {
        List<ActionButton> currentList = _characterActionButtons ? characterActionButtons : levelActionButtons;
        RectTransform currentPanel = _characterActionButtons ? characterActionsPanel : levelActionsPanel;
        GameObject currentPrefab = _characterActionButtons ? characterActionPrefab : levelActionPrefab;

        ActionButton button = Instantiate(currentPrefab, currentPanel).GetComponent<ActionButton>();
        currentList.Add(button);
        button.SetAction(_action.action, _action.actionName);

        AnimateToPosition animateToPosition = button.GetComponent<AnimateToPosition>();
        if (animateToPosition != null)
        {
            float targetHeight = ((buttonHeight + buttonSpacing) * currentList.Count) + buttonStartPositionY;
            StartCoroutine(StartAnimationWithDelay(button, buttonAnimationDelay * currentList.Count, targetHeight));
        }
    }

    IEnumerator StartAnimationWithDelay(ActionButton _button, float _delay, float _targetHeight)
    {
        yield return new WaitForSeconds(_delay);
        AnimateToPosition animateToPosition = _button.GetComponent<AnimateToPosition>();
        if (animateToPosition != null)
        {
            animateToPosition.StartAnimation(startAnimatingFromY, _targetHeight);
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
