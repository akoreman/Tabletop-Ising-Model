using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    // Start is called before the first frame update
    void Start()
    {
        gameState.GetComponent<GameState>().setGlobalParams(nX, nY, sizeX, sizeY, numFlips, startTemperature);

        //GameObject.Find("HUD").GetComponent<HUD>().setScoreText();
        //GameObject.Find("HUD").GetComponent<HUD>().setTempText();
        //GameObject.Find("HUD").GetComponent<HUD>().setFieldIcon();

        foregroundGeometry.GetComponent<SegmentDisplayHandler>().setScoreDisplay();
        foregroundGeometry.GetComponent<SegmentDisplayHandler>().setTempDisplay();

        GetComponent<TileHandler>().createGeometry();


        Pickups.GetComponent<TempPickups>().placeUpPickup(Random.Range(0,nX), Random.Range(0, nY));
        Pickups.GetComponent<TempPickups>().placeDownPickup(Random.Range(0, nX), Random.Range(0, nY));

        //Pickups.GetComponent<TempPickups>().placeFieldPickup(Random.Range(0, nX), Random.Range(0, nY));
        //Pickups.GetComponent<TempPickups>().placePBCPickup(Random.Range(0, nX), Random.Range(0, nY));
    }

    
    // Update is called once per frame
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


            //Pickups.GetComponent<TempPickups>().placeFieldPickup(Random.Range(0, nX), Random.Range(0, nY));

            //GameObject.Find("HUD").GetComponent<HUD>().setFieldIcon();          
        }
    }    
}
