using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamClipTrigger : MonoBehaviour
{

    //to clip camera
    [SerializeField] CamFollowPlayerR cam;
  


    private void OnTriggerEnter(Collider other)
    {
            print(other);
        if (other.tag == "Player")
        {
            cam.cameraYlimit = 84;
        }
    }
}
