using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Sprite[] scoreNumbers;
    public int playerscore = 0;
    public float currentDisplayScore = 0;
    public bool playerhit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //write update player hit function
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foodhit();
        }
    }
    public void foodhit()
    {
        playerscore++;
        ScoreNumberAdd();
       
    }

    public void ScoreNumberAdd()
    {
        if(playerscore > currentDisplayScore)
        {
            GetComponent<Image>().sprite = scoreNumbers[playerscore];
            currentDisplayScore = playerscore;
        }
    }
}
