using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator mainMenuAnimator;
    [SerializeField] private Animator loadingScreenAnimator;

    public void LoadLevel()
    {
        loadingScreenAnimator.SetTrigger("LoadLevel");
    }

    //Is called from load level animation 
    public void ShowGameUI()
    {
        mainMenuAnimator.SetBool("StartScreenVisible", false);
        mainMenuAnimator.SetBool("CreditsVisible", false);
        mainMenuAnimator.SetBool("SettingsVisible", false);
        mainMenuAnimator.SetBool("GameUIVisible", true);
        mainMenuAnimator.SetBool("PausedMenuVisible", false);
        mainMenuAnimator.SetBool("LevelLostVisible", false);
        mainMenuAnimator.SetBool("LevelWonVisible", false);

        Game.Manager.TheButton.ResetButton();
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

        Game.Manager.TheButton.ResetButton();
    }

    public void ShowCredits()
    {
        mainMenuAnimator.SetBool("StartScreenVisible", false);
        mainMenuAnimator.SetBool("CreditsVisible", true);

        Game.Manager.TheButton.ResetButton();
    }

    public void ShowSettings()
    {
        mainMenuAnimator.SetBool("StartScreenVisible", false);
        mainMenuAnimator.SetBool("SettingsVisible", true);

        Game.Manager.TheButton.ResetButton();
    }

    public void ShowPausedMenu()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("PausedMenuVisible", true);

        Game.Manager.TheButton.ResetButton();
    }

    public void HidePausedMenu()
    {
        mainMenuAnimator.SetBool("PausedMenuVisible", false);
        mainMenuAnimator.SetBool("GameUIVisible", true);

        Game.Manager.TheButton.ResetButton();
    }

    public void ShowLevelLost()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("LevelLostVisible", true);

        Game.Manager.TheButton.ResetButton();
    }

    public void ShowLevelWon()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("LevelWonVisible", true);

        Game.Manager.TheButton.ResetButton();
    }

    public void ShowGameEnd()
    {
        mainMenuAnimator.SetBool("GameUIVisible", false);
        mainMenuAnimator.SetBool("GameEndVisible", true);

        Game.Manager.TheButton.ResetButton();
    }
}
