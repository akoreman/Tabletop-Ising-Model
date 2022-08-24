using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to keep track of the global gamestate.

public class GameState : MonoBehaviour
{
    public bool hasUpPickup = false;
    public bool isPbcOnScreen = false;
    public bool isFieldOnScreen = false;

    public int nX;
    public int nY;

    public float sizeX;
    public float sizeY;

    public int numFlips;

    public float temperature;

    public int score;

    public int numPBC;
    public int[] availablePairs;

    public bool isGameAlive;
    public int consectUp;


    public void SetGlobalParams(int nX, int nY, float sizeX, float sizeY, int numFlips, float startTemperature)
    {
        this.nX = nX;
        this.nY = nY;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.numFlips = numFlips;
        this.temperature = startTemperature;
        this.score = 0;
        this.numPBC = 0;
        this.isGameAlive = true;
        this.consectUp = 0;

        availablePairs = new int[nX + nY];

        for (int i = 0; i < nX + nY; i++)
        {
            this.availablePairs[i] = i;
        }
    }

}
