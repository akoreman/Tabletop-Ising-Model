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
    int numConsecutivePowerUp = 3;

    Vector3 velocity;
    Vector2 acceleration;
    Rigidbody body;

    bool onGround;
    bool wantJump = false;

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
            acceleration.x = Input.GetAxis("Horizontal");
            acceleration.y = Input.GetAxis("Vertical");

            acceleration.Normalize();
            acceleration *= accScaling;
        }
        else
        {
            acceleration.x = Input.GetAxis("Horizontal");
            acceleration.y = Input.GetAxis("Vertical");

            acceleration.Normalize();
            acceleration *= airAccScaling;
        }

        // Set that the player wants to jump, jump itself is handled in FixedUpdate(). 
        if (Input.GetKeyDown("space") && onGround)
            wantJump = true;

        if (ball.localPosition.y < -1.95f)
        {
            gameState.GetComponent<GameState>().isGameAlive = false;

            ball.gameObject.SetActive(false);
        }

    }

    void FixedUpdate()
    {
        //Get the current velocity from the rigidbody.
        velocity = body.velocity;

        //Get and clamp the new velocity.
        velocity.x += Time.deltaTime * acceleration.x;
        velocity.z += Time.deltaTime * acceleration.y;

        velocity = Vector3.ClampMagnitude(velocity, maxVel);

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
            gameState.GetComponent<GameState>().score += 10;

            //Update the temperature and set the in-game displays.
            gameState.GetComponent<GameState>().temperature = Mathf.Min(99.99f, gameState.GetComponent<GameState>().temperature + 0.05f);
            Pickups.GetComponent<TempPickups>().PlaceUpPickup(Random.Range(0, nX), Random.Range(0, nY));

            foregroundGeometry.GetComponent<SegmentDisplayHandler>().SetScoreDisplay();
            foregroundGeometry.GetComponent<SegmentDisplayHandler>().SetTempDisplay();

            //If enough consecutive up-pickups have been collected place a new powerup.
            gameState.GetComponent<GameState>().consectUp++;

            if (gameState.GetComponent<GameState>().consectUp >= numConsecutivePowerUp)
                CreateNewPowerUp();
        }

        if (trigger.name == "downpickup")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            Destroy(trigger.gameObject);
            gameState.GetComponent<GameState>().score += 0;
            gameState.GetComponent<GameState>().temperature = Mathf.Max(0.5f, gameState.GetComponent<GameState>().temperature - 0.05f);
            Pickups.GetComponent<TempPickups>().PlaceDownPickup(Random.Range(0, nX), Random.Range(0, nY));

            foregroundGeometry.GetComponent<SegmentDisplayHandler>().SetScoreDisplay();
            foregroundGeometry.GetComponent<SegmentDisplayHandler>().SetTempDisplay();

            gameState.GetComponent<GameState>().consectUp = 0;
        }

        //Destroy the pickup, set the bool in the gamestate and rotate the indicator.
        if (trigger.name == "fieldpickup")
        {
            gameState.GetComponent<GameState>().hasUpPickup = true;
            Destroy(trigger.gameObject);
            foregroundGeometry.GetComponent<BackgroundHandler>().RotateLightGreen();
        }

        //Destroy the pickup, set the variables in the gamestate and place the portals.
        if (trigger.name == "pbcpickup")
        {
            int nX = gameState.GetComponent<GameState>().nX;
            int nY = gameState.GetComponent<GameState>().nY;

            Destroy(trigger.gameObject);

            gameState.GetComponent<GameState>().isPbcOnScreen = false;

            int pbcPosition = Random.Range(0, nX + nY - gameState.GetComponent<GameState>().numPBC);
            GameObject.Find("LevelGeometry").GetComponent<TileHandler>().CreatePBCPair(gameState.GetComponent<GameState>().availablePairs[pbcPosition]);
            
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

    // Create a new powerup, when a powerup is already present the other type is placed. If no powerup exists a random choice is made.
    void CreateNewPowerUp()
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;

        bool isPbcOnScreen = gameState.GetComponent<GameState>().isPbcOnScreen;
        bool isFieldOnScreen = gameState.GetComponent<GameState>().isFieldOnScreen;

        if (isPbcOnScreen && isFieldOnScreen)
            return;

        

        if (isPbcOnScreen && !isFieldOnScreen)
        {
            gameState.GetComponent<GameState>().isFieldOnScreen = true;
            gameState.GetComponent<GameState>().consectUp = 0;
            Pickups.GetComponent<TempPickups>().PlaceFieldPickup(Random.Range(0, nX), Random.Range(0, nY));

            return;
        }

        if (!isPbcOnScreen && isFieldOnScreen)
        {
            if (gameState.GetComponent<GameState>().numPBC < nX + nY)
                return;

            Pickups.GetComponent<TempPickups>().PlacePBCPickup(Random.Range(0, nX), Random.Range(0, nY));
            gameState.GetComponent<GameState>().isPbcOnScreen = true;
            gameState.GetComponent<GameState>().consectUp = 0;

            return;
        }

        if (!isPbcOnScreen && !isFieldOnScreen)
        {
            int r = Random.Range(0, 2);

            if (r == 0)
            {
                if (gameState.GetComponent<GameState>().numPBC < nX + nY)
                    return;

                Pickups.GetComponent<TempPickups>().PlacePBCPickup(Random.Range(0, nX), Random.Range(0, nY));
                gameState.GetComponent<GameState>().isPbcOnScreen = true;
                gameState.GetComponent<GameState>().consectUp = 0;
                
            }
            else
            {
                Pickups.GetComponent<TempPickups>().PlaceFieldPickup(Random.Range(0, nX), Random.Range(0, nY));
                gameState.GetComponent<GameState>().isFieldOnScreen = true;
            }

            return;
        }
    }
        
}
