using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform ball;

    [SerializeField]
    float accScaling = 1f;

    [SerializeField]
    float airAccScaling = 1f;

    [SerializeField]
    float jumpVel = 5f;

    [SerializeField]
    float maxVel = 10f;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    int nConPowerUp = 3;

    [SerializeField]
    float camSpeed = 10f;

    Vector3 velocity;
    Vector2 acceleration;
    Rigidbody body;

    bool onGround;
    bool wantJump = false;
    bool cameraMoving = false;

    Vector3 currentCamPos;
    Vector3 currentCamAngles;
    Vector3 finalCamPos = new Vector3(-0.36f, 5.3f, -8.43f);
    Vector3 finalCamAngles = new Vector3(90f, 0f, 0f);

    GameObject gameState;
    GameObject foregroundGeometry;
    GameObject Pickups;

    void Awake()
    {
        // Make the connections needed for the rest of the script.
        body = ball.GetComponent<Rigidbody>();
        gameState = GameObject.Find("Game State");
        foregroundGeometry = GameObject.Find("Foreground Geometry");
        Pickups = GameObject.Find("Pickups");
    }


    void Start()
    {
        ball.localPosition = new Vector3(0f, 0.25f, 0f);
    }

    void Update()
    {        
        // Controls are slower mid-air to give a sluggish feeling mid-air.
        if (onGround)
        {
            acceleration.x = Input.GetAxis("Horizontal") * accScaling;
            acceleration.y = Input.GetAxis("Vertical") * accScaling;
        }
        else
        {
            acceleration.x = Input.GetAxis("Horizontal") * airAccScaling;
            acceleration.y = Input.GetAxis("Vertical") * airAccScaling;
        }

        // Normalize the input vector to make controls more uniform.
        acceleration.Normalize();

        // Set that the player wants to jump, jump itself is handled in FixedUpdate(). 
        if (Input.GetButtonDown("Jump") && onGround)
        {
            wantJump = true;
        }

        if (ball.localPosition.y < -1.95f)
        {
            cameraMoving = true;
            gameState.GetComponent<GameState>().gameAlive = false;

            acceleration.x = 0;
            acceleration.y = 0;
            velocity.x = 0;
            velocity.y = 0;
            velocity.z = 0;
        }

        
        if (cameraMoving)
        {
            currentCamPos = mainCamera.transform.localPosition;
            currentCamAngles = mainCamera.transform.localEulerAngles;

            currentCamPos.x = Mathf.MoveTowards(currentCamPos.x, finalCamPos.x, camSpeed * Time.deltaTime);
            currentCamPos.y = Mathf.MoveTowards(currentCamPos.y, finalCamPos.y, camSpeed * Time.deltaTime);
            currentCamPos.z = Mathf.MoveTowards(currentCamPos.z, finalCamPos.z, camSpeed * Time.deltaTime);

            currentCamAngles.x = Mathf.MoveTowards(currentCamAngles.x, finalCamAngles.x, camSpeed * Time.deltaTime);
            currentCamAngles.y = Mathf.MoveTowards(currentCamAngles.y, finalCamAngles.y, camSpeed * Time.deltaTime);
            currentCamAngles.z = Mathf.MoveTowards(currentCamAngles.z, finalCamAngles.z, camSpeed * Time.deltaTime);

            mainCamera.transform.localPosition = currentCamPos;
            mainCamera.transform.localEulerAngles = currentCamAngles;
        }
        
    }

    void FixedUpdate()
    {
        //Get the current velocity from the rigidbody.
        velocity = body.velocity;

        //Get and clamp the new velocity.
        velocity.x += Time.deltaTime * acceleration.x;
        velocity.z += Time.deltaTime * acceleration.y;

        velocity.x = Mathf.Min(maxVel, velocity.x);
        velocity.z = Mathf.Min(maxVel, velocity.z);

        //Perform the jump.
        if (wantJump && onGround)
        {
            velocity.y += jumpVel;
            wantJump = false;
        }

        //Update the velocity of the solidbody.
        body.velocity = velocity;
    }

    //Keep track whether the ball is currently colliding with the floor.
    void OnCollisionExit(Collision collider)
    {
        if (collider.transform.name != "uppickup" & collider.transform.name != "downpickup")
            onGround = false;
    }

    void OnCollisionStay(Collision collider)
    {
        if (collider.transform.name != "uppickup" & collider.transform.name != "downpickup")
            onGround = true;
    }

    //Handle the triggers when the ball collides with the pickups on the field.
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name == "uppickup")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            //Destroy the pickup.
            Destroy(trigger.gameObject);
            gameState.GetComponent<GameState>().Score += 10;

            //Update the temperature and set the in-game displays.
            gameState.GetComponent<GameState>().Temperature = Mathf.Min(99.99f, gameState.GetComponent<GameState>().Temperature + 0.05f);
            Pickups.GetComponent<TempPickups>().placeUpPickup(Random.Range(0, nX), Random.Range(0, nY));

            foregroundGeometry.GetComponent<SegmentDisplayHandler>().setScoreDisplay();
            foregroundGeometry.GetComponent<SegmentDisplayHandler>().setTempDisplay();

            //If enough consecutive up-pickups have been collected place a new powerup.
            gameState.GetComponent<GameState>().consectUp++;

            if (gameState.GetComponent<GameState>().consectUp >= nConPowerUp)
            {              
                if (gameState.GetComponent<GameState>().pbcOnScreen && !gameState.GetComponent<GameState>().fieldOnScreen)
                {
                    gameState.GetComponent<GameState>().fieldOnScreen = true;
                    gameState.GetComponent<GameState>().consectUp = 0;
                    Pickups.GetComponent<TempPickups>().placeFieldPickup(Random.Range(0, nX), Random.Range(0, nY));
                }
                else if (!gameState.GetComponent<GameState>().pbcOnScreen && gameState.GetComponent<GameState>().fieldOnScreen)
                {
                    if (gameState.GetComponent<GameState>().numPBC < nX + nY)
                    {
                        Pickups.GetComponent<TempPickups>().placePBCPickup(Random.Range(0, nX), Random.Range(0, nY));
                        gameState.GetComponent<GameState>().pbcOnScreen = true;
                        gameState.GetComponent<GameState>().consectUp = 0;
                    }
                }
                else if (!gameState.GetComponent<GameState>().pbcOnScreen && !gameState.GetComponent<GameState>().fieldOnScreen)
                {
                    int r = Random.Range(0, 2);

                    if (r == 0)
                    {
                        if (gameState.GetComponent<GameState>().numPBC < nX + nY)
                        {
                            Pickups.GetComponent<TempPickups>().placePBCPickup(Random.Range(0, nX), Random.Range(0, nY));
                            gameState.GetComponent<GameState>().pbcOnScreen = true;
                            gameState.GetComponent<GameState>().consectUp = 0;
                        }
                    }
                    else if (r == 1)
                    {
                        Pickups.GetComponent<TempPickups>().placeFieldPickup(Random.Range(0, nX), Random.Range(0, nY));
                        gameState.GetComponent<GameState>().fieldOnScreen = true;
                    }
                }    
            }
        }

        if (trigger.name == "downpickup")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            Destroy(trigger.gameObject);
            gameState.GetComponent<GameState>().Score += 0;
            gameState.GetComponent<GameState>().Temperature = Mathf.Max(0.5f, gameState.GetComponent<GameState>().Temperature - 0.05f);
            Pickups.GetComponent<TempPickups>().placeDownPickup(Random.Range(0, nX), Random.Range(0, nY));

            foregroundGeometry.GetComponent<SegmentDisplayHandler>().setScoreDisplay();
            foregroundGeometry.GetComponent<SegmentDisplayHandler>().setTempDisplay();

            gameState.GetComponent<GameState>().consectUp = 0;
        }

        //Destroy the pickup, set the bool in the gamestate and rotate the indicator.
        if (trigger.name == "fieldpickup")
        {
            gameState.GetComponent<GameState>().hasUpPickup = true;
            Destroy(trigger.gameObject);
            foregroundGeometry.GetComponent<BackgroundHandler>().rotateLightGreen();
        }

        //Destroy the pickup, set the variables in the gamestate and place the portals.
        if (trigger.name == "pbcpickup")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            Destroy(trigger.gameObject);

            gameState.GetComponent<GameState>().pbcOnScreen = false;

            int pbcPosition = Random.Range(0, nX + nY - gameState.GetComponent<GameState>().numPBC);
            GameObject.Find("LevelGeometry").GetComponent<TileHandler>().createPBCPair(gameState.GetComponent<GameState>().availablePairs[pbcPosition]);
            
        }

        //If the ball hits one of the portals portal it to the other side of the field.
        if (trigger.name == "portalXL")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            ball.localPosition += new Vector3((float)nX, 0f, 0f);
        }

        if (trigger.name == "portalXR")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            ball.localPosition += new Vector3(-1* (float)nX, 0f, 0f);

        }

        if (trigger.name == "portalYU")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            ball.localPosition += new Vector3(0f, 0f,-1* (float) nY);

        }

        if (trigger.name == "portalYD")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            ball.localPosition += new Vector3(0f, 0f,  (float) nY);


        }


    }
        
}
