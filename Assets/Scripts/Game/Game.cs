using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    LevelWon,
    LevelLost
}

public class Game : MonoBehaviour
{
    public static Game Manager { get; private set; }
    [SerializeField] private TheButton theButton;
    public TheButton TheButton => theButton;
    [SerializeField] private List<Level> levelList = new List<Level>();
    [SerializeField] private Transform levelContainer;
    [SerializeField] private Sock sock;
    public Sock Sock => sock;
    [SerializeField] private UIController uiController;
    public UIController UIController => uiController;
    [SerializeField] private GameUIController gameUIController;
    public GameUIController GameUIController => gameUIController;
    [SerializeField] int startLevelIndex = -1;
    private int currentLevelIndex = -1;
    private Level currentLevel;
    public Level CurrentLevel => currentLevel;
    [SerializeField] private GameState currentState = GameState.MainMenu;

    private void Awake()
    {
        if (!Manager)
        {
            Manager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (startLevelIndex != -1)
        {
            currentLevelIndex = startLevelIndex - 1;
        }
    }

    public GameState CurrentState
    {
        get => currentState;
    }

    public void StartNextLevel(bool reset)
    {
        if ((currentState == GameState.LevelWon || currentState == GameState.MainMenu) && !reset)
        {
            currentLevelIndex++;
        }

        currentState = GameState.Playing;

        if (currentLevel != null)
        {
            currentLevel.DestroyLevel();
            Destroy(currentLevel.gameObject);
        }

        if (currentLevelIndex < 0 || currentLevelIndex >= levelList.Count)
        {
            Debug.LogError("Invalid level index: " + currentLevelIndex);
            // Here you can show an end screen or reset the game
            return;
        }

        uiController.LoadLevel();
    }

    public void LoadLevel()
    {
        currentLevel = Instantiate(levelList[currentLevelIndex], levelContainer);

        currentLevel.InitLevel(sock, this);
    }

    public void EndLevel(bool won)
    {
        if (won)
        {
            currentState = GameState.LevelWon;
            if (currentLevelIndex < levelList.Count - 1)
            {
                UIController.ShowLevelWon();
            }
            else
            {
                UIController.ShowGameEnd();
            }
        }
        else
        {
            currentState = GameState.LevelLost;
            UIController.ShowLevelLost();
        }
    }

    public void QuitGame()
    {
#if UNITY_WEBGL
        Screen.fullScreen = false;
#endif
        Application.Quit();
    }
}
