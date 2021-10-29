using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ScreenType
{
    StartMenu,
    Gameplay,
    Pause,
    End
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    List<UIController> uiControllerList;
    UIController lastActiveUIController;

    private void Awake()
    {
        uiControllerList = GetComponentsInChildren<UIController>().ToList();
        uiControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchScreen(ScreenType.StartMenu);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
    }

    public void SwitchScreen(ScreenType screenType)
    {
        if (lastActiveUIController != null)
        {
            lastActiveUIController.gameObject.SetActive(false);
        }

        UIController selectedUIController = uiControllerList.Find(x => x.ScreenType == screenType);

        if (selectedUIController != null)
        {
            selectedUIController.gameObject.SetActive(true);
            lastActiveUIController = selectedUIController;
        }
        else
        {
            Debug.LogWarning("Screen " + screenType + " was not found!");
        }

        if (screenType == ScreenType.Gameplay || screenType == ScreenType.Pause)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
