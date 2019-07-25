using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCam : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            transform.position += new Vector3(move,0,0) * speed * Time.deltaTime;
        }
    }
}
