using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour
{
    public GameObject[] ModeControllers;

    void Awake() { // stops controller from being destroyed on scene load.
    //DO NOT TOUCH
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void FindGameMode(string mode) // finds gamemode passed as string, enables the controller for that mode
    //Use OnEnable() in your gamemode as an initialize/setup function
    {
        foreach(GameObject modeController in ModeControllers)
        {
            if(modeController.name == mode)
            {
                modeController.SetActive(true);
                break;
            }
        }
    }
}
