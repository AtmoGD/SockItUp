using UnityEngine;
using TMPro;

public enum ActionButtonAction
{
    // <------ UI ------>
    StartGame,
    OpenStartScreen,
    OpenCredits,
    OpenSettings,
    OpenPausedMenu,
    HidePausedMenu,
    QuitGame,
    // <------ Character ------>
    MoveCharacterRight,
    MoveCharacterLeft,
    CharacterJump,
    CharacterInteract,
    // <------ Level ------>
    RotateLevelLeft,
    RotateLevelRight,
}

[RequireComponent(typeof(RectTransform))]
public class ActionButton : MonoBehaviour
{
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private ActionButtonAction action = ActionButtonAction.StartGame;
    [SerializeField] private string actionName = "DefaultAction";
    [SerializeField] private bool infiniteUse = false;
    [SerializeField] private TMP_Text buttonText = null;
    [SerializeField] private bool initOnAwake = false;

    private void Awake()
    {
        if (!initOnAwake)
            return;

        buttonText.text = actionName;
    }

    public RectTransform GetRect()
    {
        return rect;
    }

    public string GetActionName()
    {
        return actionName;
    }

    public void SetAction(ActionButtonAction _newAction, string _actionName, bool _infiniteUse)
    {
        action = _newAction;
        actionName = _actionName;
        infiniteUse = _infiniteUse;
        buttonText.text = _actionName;
    }

    public virtual void StartOverlapping()
    {
        buttonText.text = "";
    }

    public virtual void StopOverlapping()
    {
        buttonText.text = actionName;
    }

    public virtual void DoAction()
    {
        switch (action)
        {
            case ActionButtonAction.StartGame:
                StartGame();
                break;
            case ActionButtonAction.OpenStartScreen:
                OpenStartScreen();
                break;
            case ActionButtonAction.OpenCredits:
                OpenCredits();
                break;
            case ActionButtonAction.OpenSettings:
                OpenSettings();
                break;
            case ActionButtonAction.OpenPausedMenu:
                OpenPausedMenu();
                break;
            case ActionButtonAction.HidePausedMenu:
                HidePausedMenu();
                break;
            case ActionButtonAction.QuitGame:
                QuitGame();
                break;
            case ActionButtonAction.MoveCharacterRight:
                Game.Manager.Sock.MoveCharacter(Vector2.right);
                break;
            case ActionButtonAction.MoveCharacterLeft:
                Game.Manager.Sock.MoveCharacter(Vector2.left);
                break;
            default:
                Debug.LogWarning("Action not implemented: " + action);
                break;
        }

        if (!infiniteUse)
        {
            Destroy(gameObject);
        }
    }

    private void StartGame()
    {
        Game.Manager.StartNextLevel();
    }

    private void OpenStartScreen()
    {
        Game.Manager.UIController.ShowStartScreen();
    }

    private void OpenCredits()
    {
        Game.Manager.UIController.ShowCredits();
    }

    private void OpenSettings()
    {
        Game.Manager.UIController.ShowSettings();
    }

    private void OpenPausedMenu()
    {
        Game.Manager.UIController.ShowPausedMenu();
    }

    private void HidePausedMenu()
    {
        Game.Manager.UIController.HidePausedMenu();
    }

    private void QuitGame()
    {
        Game.Manager.QuitGame();
    }
}
