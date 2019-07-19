using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Canvas canvas;

    public ParticleSystem ripple;

    List<ParticleSystem> ripples;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        ripples = new List<ParticleSystem>();
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
            ripples.Add(Instantiate(temp));
            temp.transform.position = transform.position;
            temp.Play();
        }

        DestroyRipples();
    }

    void DestroyRipples()
    {
        for (int i = 0; i < ripples.Count; i++)
        {
            if (!ripples[i].isPlaying)
            {
                ParticleSystem temp = ripples[i];
                ripples.Remove(temp);
                Destroy(temp);
            }
        }
    }
}
