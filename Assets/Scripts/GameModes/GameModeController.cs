using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour
{
    public GameObject[] ModeControllers;

    public void FindGameMode(string mode) // finds gamemode passed as string, enables the controller for that mode
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
