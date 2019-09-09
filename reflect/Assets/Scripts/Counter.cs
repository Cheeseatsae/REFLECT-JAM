using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject Text;
    public float Playerscore =0;
    public float currentDisplayScore = 0;
    public bool Playerhit;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //write update player hit function
    }
    public void foodhit()
    {
        if (Playerhit == true)
        {
            Playerscore++;
          
        }
       
    }
}
