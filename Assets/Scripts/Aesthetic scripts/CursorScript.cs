using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Canvas canvas;

    public ParticleSystem ripple;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = canvas.planeDistance - 0.03f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ParticleSystem temp = Instantiate(ripple);
            //temp.transform.parent = canvas.transform;
            temp.tag = "Ripple";
            temp.transform.position = transform.position;
        }
    }
}
