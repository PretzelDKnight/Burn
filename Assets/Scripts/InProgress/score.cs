using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    float pscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        ScoreSystem.timescore += timedscore;
        ScoreSystem.combomock += comboscore;
    }

    // Update is called once per frame
    void Update()
    {
        pscore += Time.deltaTime;
    }

    void timedscore()
    {
        print((int)pscore);
    }

    void comboscore(int i)
    {
        if (Input.GetKeyDown(KeyCode.X))
            i++;

        if (i > 4)
            pscore = pscore * i;

        Debug.Log("i has been pressed" + i + "times");
    }
}