using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SinglePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        
    SceneManager.LoadScene("maze");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
