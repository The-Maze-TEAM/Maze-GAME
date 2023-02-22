using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Material trapMat;
	public Material goalMat;
	public Toggle colorblindMode;
	public void PlayMaze() //loads some basic settings, then loads gamemode menu
	{
		if (colorblindMode != null && colorblindMode.isOn)
    	{
			trapMat.color = new Color32(255, 112, 0, 1);
			goalMat.color = Color.blue;
    	}
		else if (!colorblindMode.isOn)
		{
			trapMat.color = Color.red;
			goalMat.color = Color.green;
		}
		//SceneManager.LoadScene("maze");

	}

    public void QuitMaze()
    {
        #if UNITY_EDITOR
        Debug.Log("Quit Game");
        #else
        Application.Quit();
        #endif
    }
}
