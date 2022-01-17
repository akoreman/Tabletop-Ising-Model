using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Handles the spacebar press to start.

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
