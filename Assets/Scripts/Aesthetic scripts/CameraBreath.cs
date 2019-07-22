using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBreath : MonoBehaviour
{
    float time;

    Vector3 position;

    public GameObject focus;
    public float speed = 1;
    public float strength = 1;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        transform.position = new Vector3( transform.position.x, position.y + Mathf.Sin(time * speed) * strength, transform.position.z);

        transform.LookAt(focus.transform);
    }
}
