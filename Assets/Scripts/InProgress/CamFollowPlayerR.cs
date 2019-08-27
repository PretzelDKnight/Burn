using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayerR : MonoBehaviour
{
    [SerializeField] GameObject target;
    public float cameraYlimit;

    //Valuses of the postion of the camera needed to be added to the player postion on start
    [SerializeField] Vector3 camOffSet;
    Vector3 requiredSpot;
    bool standardTrack;
    float checkY;


    // Updating the positon of camera to follow the player at all times
    void LateUpdate()
    {
        standardTrack = true;
        checkY = target.transform.position.y + camOffSet.y;
        if(checkY > cameraYlimit)
        {
            standardTrack = false;
        }

        if(standardTrack)
            requiredSpot = target.transform.position + camOffSet;
        else if(!standardTrack)
            requiredSpot = new Vector3(target.transform.position.x + camOffSet.x, cameraYlimit, target.transform.position.z + camOffSet.z);

        transform.position = requiredSpot;

        //transform.LookAt(target.transform);
    }
}
