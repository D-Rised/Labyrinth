using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Stop,
    Play
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameState = GameState.Stop;
    }

    public void SetGameState(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.Stop:
                break;
            case GameState.Play:
                break;
        }
        gameState = _gameState;
    }
    public GameState GetGameState() { return gameState; }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && GetGameState() == GameState.Play)
        {
            SetGameState(GameState.Stop);
            UIManager.instance.SwitchScreen(ScreenType.Pause);
        }
    }
}
