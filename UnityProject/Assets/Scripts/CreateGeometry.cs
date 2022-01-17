using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This handles the startup of a new game, sets all proper variables and sets up the creation of the level geometry.

public class CreateGeometry : MonoBehaviour
{
    [SerializeField]
    int nX = 10;

    [SerializeField]
    int nY = 10;

    [SerializeField]
    float sizeX = 1f;

    [SerializeField]
    float sizeY = 1f;

    [SerializeField]
    int numFlips = 3;

    [SerializeField]
    float startTemperature = 5f;

    GameObject gameState;
    GameObject foregroundGeometry;
    GameObject Pickups;

    void Awake()
    {
        gameState = GameObject.Find("Game State");
        foregroundGeometry = GameObject.Find("Foreground Geometry");
        Pickups = GameObject.Find("Pickups");
    }

    void Start()
    {
        gameState.GetComponent<GameState>().setGlobalParams(nX, nY, sizeX, sizeY, numFlips, startTemperature);

        foregroundGeometry.GetComponent<SegmentDisplayHandler>().setScoreDisplay();
        foregroundGeometry.GetComponent<SegmentDisplayHandler>().setTempDisplay();

        GetComponent<TileHandler>().createGeometry();

        Pickups.GetComponent<TempPickups>().placeUpPickup(Random.Range(0,nX), Random.Range(0, nY));
        Pickups.GetComponent<TempPickups>().placeDownPickup(Random.Range(0, nX), Random.Range(0, nY));
    }

    
    void Update()
    {
        if (!gameState.GetComponent<GameState>().gameAlive && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainGame");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && gameState.GetComponent<GameState>().hasUpPickup)
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            GetComponent<TileHandler>().setAllUp();
            gameState.GetComponent<GameState>().hasUpPickup = false;
            gameState.GetComponent<GameState>().fieldOnScreen = false;

            foregroundGeometry.GetComponent<BackgroundHandler>().foregroundFieldButtonPress();
        
        }
    }    
}
