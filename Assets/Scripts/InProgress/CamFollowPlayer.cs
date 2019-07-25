using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject target;

    //Valuses of the postion of the camera needed to be added to the player postion on start
    [SerializeField] Vector3 camOffSet;


    // Updating the positon of camera to follow the player at all times
    void LateUpdate()
    {
        Vector3 requiredSpot = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        transform.position = requiredSpot;

        //transform.LookAt(target.transform);
    }
}
