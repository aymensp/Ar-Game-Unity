using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System;
public class GameManager : MonoBehaviour
{


    public GameObject[] pins;
    public GameObject ball;
    public GameObject somme;
    public Text[] scrs;
    public Text[] scrsRound;
    int turnCounter = 1;
    //int RoundCounter = 1;
    int[] scrsValues = { 0,0,0,0,0,0,0,0,0,0 };
    ArrayList scrsTotalValues = new ArrayList() ;
    int score = 0;
    int TotalScore = 0;
    public Text ScoreUi;
    Vector3[] positions;
    public GameObject ScoreTable;
    public GameObject FinGame;
    public HighScore highScore;
    public Text FinalScore;
    // Start is called before the first frame update
    void Start()
    {
        /*foreach(Transform item in ball.transform)
        {
            if(item.tag == "Ball")
            {
                ball = item.gameObject;
            }
        }*/
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        pins = GameObject.FindGameObjectsWithTag("Pin");
        
        positions = new Vector3[pins.Length];
        for (int i = 0; i < pins.Length; i++)
        {
            positions[i] = pins[i].transform.position;
        }

    }
    public void PlayAgain ()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(pins[0].gameObject.transform.position);
        //ballPos.position = pins[0].gameObject.transform.position;

        if ( ball.transform.position.z > 10 || ball.transform.position.y < -10)
        {
            SoundManager.playStrikeSound();
            CountPinsDown();
            scrs[turnCounter-1].text = scrsValues[turnCounter-1].ToString();
            scrs[turnCounter-1].gameObject.SetActive(true);
            ResetPins();
        }

        

        

    }
    void CheckIfPinsActive ()
    {
        int p=0;
        for (int i = 0; i < pins.Length; i++)
        {
            if(pins[i].activeSelf)
            {
                p++;
            }
        }
        if (p > 0)
        {
            turnCounter++;
        }
        else if (p == 0 && turnCounter % 2 != 0)
        {
            turnCounter = turnCounter+2;
            int TotalScrForEachRounds = (scrsValues[turnCounter - 3] + scrsValues[turnCounter - 2]) * 80;
            scrsTotalValues.Add(TotalScrForEachRounds);
            scrsRound[(turnCounter - 3) / 2].text = scrsTotalValues[(turnCounter - 3) / 2].ToString();
            scrsRound[(turnCounter - 3) / 2].gameObject.SetActive(true);

        }
        else if (p == 0 && turnCounter % 2 == 0)
        {
            turnCounter++;
        }
            
    }
    void CountPinsDown()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355 && pins[i].activeSelf)
            {
                score++;
                pins[i].SetActive(false);
                
            }
        }
        scrsValues[turnCounter - 1] = score;
        score = 0;

        if (turnCounter % 2 == 0)
        {

            int TotalScrForEachRounds = (scrsValues[turnCounter - 2] + scrsValues[turnCounter - 1]) * 80;
            scrsTotalValues.Add(TotalScrForEachRounds);
            scrsRound[(turnCounter - 2) / 2].text = scrsTotalValues[(turnCounter - 2) / 2].ToString();
            scrsRound[(turnCounter - 2) / 2].gameObject.SetActive(true);

        }


        if (TotalScore > highScore.highScore)
        {
            highScore.highScore = TotalScore;
        }

    }

    void ResetPins()
    {
        CheckIfPinsActive();

        Debug.Log('x'+somme.transform.position.x);
        Debug.Log('y'+somme.transform.position.z);
        if (turnCounter > 10)
        {
            for (int a = 0; a < scrsTotalValues.Count; a++)
            {
                TotalScore = Int32.Parse(scrsTotalValues[a].ToString()) + TotalScore;
                ScoreUi.text = TotalScore.ToString();
                ScoreUi.gameObject.SetActive(true);
                FinalScore.text = TotalScore.ToString();
                FinGame.SetActive(true);

            }

            //SceneManager.LoadScene("MainMenu");
        }
        for (int i = 0; i < pins.Length; i++)
        {
            if (turnCounter % 2 != 0)
            {
                pins[i].SetActive(true);

            }
            pins[i].transform.position = positions[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
        }

        /*ball.transform.position = new Vector3(-0.1f, 0.8f, -85.3f);*/
        /*ball.transform.position = new Vector3(0.3f, 0f, -4f);*/
        
        ball.transform.position = somme.transform.position + new Vector3(0f,1f, -4f);
        //Debug.Log(ballPos.position - new Vector3(0f, 1f, 5f));
        //Vector3 pos = new Vector3(0, 2f, -50f);
        /*ball.transform.position = ballPos.position;*/
        /*ball.transform.position = pins[0].gameObject.transform.position-new Vector3(0,0,5);*/
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;

    }

}
