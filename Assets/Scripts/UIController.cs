using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator mainMenuAnimator;
    [SerializeField] private Animator loadingScreenAnimator;

    public void LoadLevel()
    {
        loadingScreenAnimator.SetTrigger("LoadLevel");
        mainMenuAnimator.SetBool("StartScreenVisible", false);
        mainMenuAnimator.SetBool("PausedMenuVisible", false);
        mainMenuAnimator.SetBool("LevelLostVisible", false);
        mainMenuAnimator.SetBool("LevelWonVisible", false);
        mainMenuAnimator.SetBool("GameUIVisible", true);
    }

    public void ShowStartScreen()
    {
        mainMenuAnimator.SetBool("StartScreenVisible", true);
        mainMenuAnimator.SetBool("CreditsVisible", false);
        mainMenuAnimator.SetBool("SettingsVisible", false);
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("PausedMenuVisible", false);
        mainMenuAnimator.SetBool("LevelLostVisible", false);
        mainMenuAnimator.SetBool("LevelWonVisible", false);
    }

    public void ShowCredits()
    {
        mainMenuAnimator.SetBool("StartScreenVisible", false);
        mainMenuAnimator.SetBool("CreditsVisible", true);
    }

    public void ShowSettings()
    {
        mainMenuAnimator.SetBool("StartScreenVisible", false);
        mainMenuAnimator.SetBool("SettingsVisible", true);
    }

    public void ShowPausedMenu()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("PausedMenuVisible", true);
    }

    public void HidePausedMenu()
    {
        mainMenuAnimator.SetBool("PausedMenuVisible", false);
        mainMenuAnimator.SetBool("GameUIVisible", true);
    }

    public void ShowLevelLost()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("LevelLostVisible", true);
    }

    public void ShowLevelWon()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("LevelWonVisible", true);
    }

    public void ShowGameEnd()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("GameEndVisible", true);
    }
}
