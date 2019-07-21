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
        Vector3 requiredSpot = target.transform.position + camOffSet;
        transform.position = requiredSpot;

        transform.LookAt(target.transform);
    }
}
