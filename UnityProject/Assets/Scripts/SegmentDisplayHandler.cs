using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the in-game segment display.

public class SegmentDisplayHandler : MonoBehaviour
{
    [SerializeField]
    public Transform Digit11;

    [SerializeField]
    public Transform Digit12;

    [SerializeField]
    public Transform Digit13;

    [SerializeField]
    public Transform Digit14;

    [SerializeField]
    public Transform Digit21;

    [SerializeField]
    public Transform Digit22;

    [SerializeField]
    public Transform Digit23;

    [SerializeField]
    public Transform Digit24;

    [SerializeField]
    Material inputBlack;

    [SerializeField]
    Material inputGreen;

    static Material Black;
    static Material Green;

    SegmentDisplay scoreDisplay;
    SegmentDisplay tempDisplay;

    //Set-up the displays and the digits.
    void Awake()
    {
        Green = inputGreen;
        Black = inputBlack;

        tempDisplay = new SegmentDisplay(new SegmentDigit(Digit21, Black, Green), new SegmentDigit(Digit22, Black, Green), new SegmentDigit(Digit23, Black, Green), new SegmentDigit(Digit24, Black, Green));
        scoreDisplay = new SegmentDisplay(new SegmentDigit(Digit11, Black, Green), new SegmentDigit(Digit12, Black, Green), new SegmentDigit(Digit13, Black, Green), new SegmentDigit(Digit14, Black, Green));
    }

    //These are called to change the display.
    public void setScoreDisplay()
    {
        scoreDisplay.SetDisplay(GameObject.Find("Game State").GetComponent<GameState>().Score);
    }

    public void setTempDisplay()
    {
        //Format to deal with the period in the temp display.
        string fmt = "00.00";
        string numberString = GameObject.Find("Game State").GetComponent<GameState>().Temperature.ToString(fmt).Remove(2,1);

        tempDisplay.SetDisplay(numberString);
    }
}

//This class handles a display consisting of 4 digits.
public class SegmentDisplay
{
    SegmentDigit[] digitList;

    public SegmentDisplay(SegmentDigit Digit1, SegmentDigit Digit2, SegmentDigit Digit3, SegmentDigit Digit4)
    {
        digitList = new SegmentDigit[4];

        digitList[0] = Digit1;
        digitList[1] = Digit2;
        digitList[2] = Digit3;
        digitList[3] = Digit4;

    }

    //Set the display, either with a string of directly with a float.
    public void SetDisplay(float Number)
    {
        string fmt = "0000.##";
        string numberString = Number.ToString(fmt);

        for (int i = 0; i < 4; i++)
            digitList[i].SetDisplay(numberString[i]);
    }

    public void SetDisplay(string numberString)
    {
        for (int i = 0; i < 4; i++)
            digitList[i].SetDisplay(numberString[i]);
    }
}

//This class handles the segments of a single digit of a display.
public class SegmentDigit
{
    GameObject Segment1;
    GameObject Segment2;
    GameObject Segment3;
    GameObject Segment4;
    GameObject Segment5;
    GameObject Segment6;
    GameObject Segment7;

    Material Green;
    Material Black;

    public SegmentDigit(Transform Digit, Material Black, Material Green)
    {
        Segment1 = Digit.Find("Segment 1").gameObject;
        Segment2 = Digit.Find("Segment 2").gameObject;
        Segment3 = Digit.Find("Segment 3").gameObject;
        Segment4 = Digit.Find("Segment 4").gameObject;
        Segment5 = Digit.Find("Segment 5").gameObject;
        Segment6 = Digit.Find("Segment 6").gameObject;
        Segment7 = Digit.Find("Segment 7").gameObject;

        this.Green = Green;
        this.Black = Black;
    }

    //Set each segment to the correct color for each value 0 through 9.
    public void SetDisplay(char desiredNumber)
    {
        switch (desiredNumber)
        {
            case '0':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Green;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Black;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Green;
                Segment7.GetComponent<Renderer>().material = Green;

                break;

            case '1':
                Segment1.GetComponent<Renderer>().material = Black;
                Segment2.GetComponent<Renderer>().material = Black;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Black;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Black;
                Segment7.GetComponent<Renderer>().material = Black;

                break;

            case '2':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Green;
                Segment3.GetComponent<Renderer>().material = Black;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Black;
                Segment7.GetComponent<Renderer>().material = Green;

                break;

            case '3':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Black;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Black;
                Segment7.GetComponent<Renderer>().material = Green;

                break;

            case '4':
                Segment1.GetComponent<Renderer>().material = Black;
                Segment2.GetComponent<Renderer>().material = Black;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Green;
                Segment7.GetComponent<Renderer>().material = Black;

                break;

            case '5':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Black;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Black;
                Segment6.GetComponent<Renderer>().material = Green;
                Segment7.GetComponent<Renderer>().material = Green;

                break;

            case '6':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Green;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Black;
                Segment6.GetComponent<Renderer>().material = Green;
                Segment7.GetComponent<Renderer>().material = Green;

                break;

            case '7':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Black;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Black;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Black;
                Segment7.GetComponent<Renderer>().material = Black;

                break;

            case '8':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Green;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Green;
                Segment7.GetComponent<Renderer>().material = Green;

                break;

            case '9':
                Segment1.GetComponent<Renderer>().material = Green;
                Segment2.GetComponent<Renderer>().material = Black;
                Segment3.GetComponent<Renderer>().material = Green;
                Segment4.GetComponent<Renderer>().material = Green;
                Segment5.GetComponent<Renderer>().material = Green;
                Segment6.GetComponent<Renderer>().material = Green;
                Segment7.GetComponent<Renderer>().material = Green;

                break;
        }



    }
}
