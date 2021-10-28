using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager.GetGameState() == GameState.Play)
            {
                gameManager.SetGameState(GameState.Stop);
                uiManager.SwitchScreen(ScreenType.Pause);
            }
            else
            {
                gameManager.SetGameState(GameState.Play);
                uiManager.SwitchScreen(ScreenType.Gameplay);
            }
        }
    }
}
