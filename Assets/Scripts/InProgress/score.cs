using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    float pscore = 0;
    float countdown = 300;
    float levelscore = 3000;

    // Start is called before the first frame update
    void Start()
    {
        ScoreSystem.timescore += timedscore;
        ScoreSystem.combomock += comboscore;
        ScoreSystem.countdownscore += countdownscores;
    }

    // Update is called once per frame
    void Update()
    {
        //pscore += Time.deltaTime;
    }

    void timedscore()
    {
        //print((int)pscore);
    }

    void comboscore(int i)
    {
        if (Input.GetKeyDown(KeyCode.X))
            i++;

        if (i > 4)
            pscore = pscore * i;

        Debug.Log("i has been pressed" + i + "times");
    }

    void countdownscores()
    {
        countdown -= Time.deltaTime;

        if(countdown != 0)
        {
            levelscore -= 10;
        }

        Debug.Log(levelscore);
    }
}