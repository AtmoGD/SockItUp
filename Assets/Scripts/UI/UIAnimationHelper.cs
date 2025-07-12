using UnityEngine;

public class UIAnimationHelper : MonoBehaviour
{
    public void ShowGameUI()
    {
        Game.Manager.UIController.ShowGameUI();
    }

    public void LoadLevel()
    {
        Game.Manager.LoadLevel();
    }
}
