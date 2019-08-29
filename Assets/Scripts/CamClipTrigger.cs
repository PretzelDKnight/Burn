using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamClipTrigger : MonoBehaviour
{

    //to clip camera
    [SerializeField] CamFollowPlayerR cam;
    [SerializeField] float limitYvalue;
  


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.cameraYlimit = limitYvalue;
        }
    }

}
