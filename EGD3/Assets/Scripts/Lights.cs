using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public float start_intens = .5f;
    float current_intens;
    public float speed = .001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current_intens = GetComponent<Light>().intensity;
        if(current_intens > start_intens)
        {
            current_intens-=speed;
            GetComponent<Light>().intensity = current_intens;
        }
    }

    void Flash()
    {
        GetComponent<Light>().intensity = 1f;
    }
}
