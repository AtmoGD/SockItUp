using UnityEngine;
using TMPro;

public enum ActionButtonAction
{
    StartGame,
    OpenStartScreen,
    OpenCredits,
    OpenSettings,
    OpenPausedMenu,
    HidePausedMenu,
    QuitGame
}

[RequireComponent(typeof(RectTransform))]
public class ActionButton : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private ActionButtonAction action = ActionButtonAction.StartGame;
    [SerializeField] private string actionName = "DefaultAction";
    [SerializeField] private TMP_Text buttonText;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

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
        Debug.Log(gameObject.name + " action performed.");

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
            default:
                Debug.LogWarning("Action not implemented: " + action);
                break;
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
