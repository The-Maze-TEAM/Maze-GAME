using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModeMenu : MonoBehaviour
{
    public GameObject MasterGameModeController;
    // Start is called before the first frame update
    public void LaunchGameMode(string mode)
    {
        MasterGameModeController.GetComponent<GameModeController>().FindGameMode("SinglePlayer");
    }

}
