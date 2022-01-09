using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text tempText;

    [SerializeField]
    Image fieldIcon;

    public void setTempText()
    {
        tempText.text = "Temp: " + GameObject.Find("Game State").GetComponent<GameState>().Temperature.ToString();
    }

    public void setScoreText()
    {
        scoreText.text = "Score: " + GameObject.Find("Game State").GetComponent<GameState>().Score.ToString();
    }

    public void setFieldIcon()
    {
        if(GameObject.Find("Game State").GetComponent<GameState>().hasUpPickup)
            fieldIcon.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
        else
            fieldIcon.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
    }
}
