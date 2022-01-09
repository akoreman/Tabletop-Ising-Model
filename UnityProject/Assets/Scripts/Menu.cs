using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //[SerializeField]
    //Button startButton; 

    // Start is called before the first frame update
    void Awake()
    {
        //startButton.onClick.AddListener(onClick);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    /*
    void onClick()
    {
        SceneManager.LoadScene("MainGame");
    }
    */
}
