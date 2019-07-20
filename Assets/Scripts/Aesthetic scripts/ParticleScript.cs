using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    float time;

    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        if (gameObject.tag == "Ripple")
            particle.Play();
        //Destroy(particle.gameObject, 1f);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Ripple")
            time += Time.deltaTime;

        if (time >=1f)
        {
            Destroy(this.gameObject);
        }
    }
}
