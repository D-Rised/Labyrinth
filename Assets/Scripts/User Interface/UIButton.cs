using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonType
{
    Start,
    Restart
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
            case ButtonType.Start:
                uiManager.SwitchScreen(ScreenType.Gameplay);
                gameManager.SetGameState(GameState.Play);
                break;
            case ButtonType.Restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }
}
