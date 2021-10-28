using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    StartGame,
    Restart,
    ToMainMenu
}

public class UIButton : MonoBehaviour
{
    public ButtonType buttonType;

    private GameManager gameManager;
    private UIManager uiManager;
    private Button button;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
    }

    private void OnButtonClicked()
    {
        if (uiManager == null) { return; }
        switch (buttonType)
        {
            case ButtonType.StartGame:
                uiManager.SwitchScreen(ScreenType.Gameplay);
                gameManager.SetGameState(GameState.Play);
                break;
            case ButtonType.Restart:
                uiManager.SwitchScreen(ScreenType.Gameplay);
                gameManager.SetGameState(GameState.Play);
                break;
            case ButtonType.ToMainMenu:
                uiManager.SwitchScreen(ScreenType.StartMenu);
                gameManager.SetGameState(GameState.Stop);
                break;
        }
    }
}
