using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Material White;

    [SerializeField]
    Material Green;

    static Material sWhite;
    static Material sGreen;

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

    public class SegmentDigit
    {
        GameObject Segment1;
        GameObject Segment2;
        GameObject Segment3;
        GameObject Segment4;
        GameObject Segment5;
        GameObject Segment6;
        GameObject Segment7;

        public SegmentDigit(Transform Digit)
        {
            Segment1 = Digit.Find("Segment 1").gameObject;
            Segment2 = Digit.Find("Segment 2").gameObject;
            Segment3 = Digit.Find("Segment 3").gameObject;
            Segment4 = Digit.Find("Segment 4").gameObject;
            Segment5 = Digit.Find("Segment 5").gameObject;
            Segment6 = Digit.Find("Segment 6").gameObject;
            Segment7 = Digit.Find("Segment 7").gameObject;
        }

        public void SetDisplay(char desiredNumber)
        {
            switch (desiredNumber)
            {
                case '0':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sGreen;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sWhite;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sGreen;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;

                case '1':
                    Segment1.GetComponent<Renderer>().material = sWhite;
                    Segment2.GetComponent<Renderer>().material = sWhite;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sWhite;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sWhite;
                    Segment7.GetComponent<Renderer>().material = sWhite;

                    break;

                case '2':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sGreen;
                    Segment3.GetComponent<Renderer>().material = sWhite;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sWhite;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;

                case '3':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sWhite;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sWhite;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;

                case '4':
                    Segment1.GetComponent<Renderer>().material = sWhite;
                    Segment2.GetComponent<Renderer>().material = sWhite;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sGreen;
                    Segment7.GetComponent<Renderer>().material = sWhite;

                    break;

                case '5':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sWhite;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sWhite;
                    Segment6.GetComponent<Renderer>().material = sGreen;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;

                case '6':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sGreen;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sWhite;
                    Segment6.GetComponent<Renderer>().material = sGreen;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;

                case '7':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sWhite;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sWhite;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sWhite;
                    Segment7.GetComponent<Renderer>().material = sWhite;

                    break;

                case '8':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sGreen;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sGreen;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;

                case '9':
                    Segment1.GetComponent<Renderer>().material = sGreen;
                    Segment2.GetComponent<Renderer>().material = sWhite;
                    Segment3.GetComponent<Renderer>().material = sGreen;
                    Segment4.GetComponent<Renderer>().material = sGreen;
                    Segment5.GetComponent<Renderer>().material = sGreen;
                    Segment6.GetComponent<Renderer>().material = sGreen;
                    Segment7.GetComponent<Renderer>().material = sGreen;

                    break;
            }

           

        }
    }

    SegmentDisplay scoreDisplay;
    SegmentDisplay tempDisplay;

    // Start is called before the first frame update
    void Awake()
    {
        sGreen = Green;
        sWhite = White;

        tempDisplay = new SegmentDisplay(new SegmentDigit(Digit21), new SegmentDigit(Digit22), new SegmentDigit(Digit23), new SegmentDigit(Digit24));
        scoreDisplay = new SegmentDisplay(new SegmentDigit(Digit11), new SegmentDigit(Digit12), new SegmentDigit(Digit13), new SegmentDigit(Digit14));
    }

    public void setScoreDisplay()
    {
        print("yay");
        scoreDisplay.SetDisplay(GameObject.Find("Game State").GetComponent<GameState>().Score);
    }

    public void setTempDisplay()
    {
        string fmt = "00.00";

        string numberString = GameObject.Find("Game State").GetComponent<GameState>().Temperature.ToString(fmt).Remove(2,1);

        tempDisplay.SetDisplay(numberString);
    }
}
