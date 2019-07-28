using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public delegate void scoresystem();
    public delegate void combomockup(int i);
    public static event scoresystem timescore;
    public static event scoresystem countdownscore;
    public static event combomockup combomock;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timescore != null)
            timescore();

        if (countdownscore != null)
            countdownscore();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (combomock != null)
                combomock(i);
        }
    }
}
