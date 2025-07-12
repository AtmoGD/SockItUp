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
    [SerializeField] private List<Level> levelList = new List<Level>();
    [SerializeField] private Transform levelContainer;
    [SerializeField] private Sock sock;
    public Sock Sock => sock;
    [SerializeField] private UIController uiController;
    public UIController UIController => uiController;
    [SerializeField] private GameUIController gameUIController;
    public GameUIController GameUIController => gameUIController;
    private int currentLevelIndex = -1;
    private Level currentLevel;
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
    }

    public GameState CurrentState
    {
        get => currentState;
    }

    public void StartNextLevel()
    {
        if (currentState == GameState.LevelWon || currentState == GameState.MainMenu)
        {
            currentLevelIndex++;
        }

        currentState = GameState.Playing;

        if (currentLevel != null)
        {
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
            Debug.Log("Level won!");
            // Handle level won logic here
        }
        else
        {
            currentState = GameState.LevelLost;
            Debug.Log("Level lost!");
            // Handle level lost logic here
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
