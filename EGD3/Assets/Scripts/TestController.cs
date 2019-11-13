using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject spawn;
    int pos = -5;
    public bool nextframe;
    string nextframetype;
    public ParticleSystem ps;
    ParticleSystem.MainModule psm;
    public Metronome m;
    // Start is called before the first frame update
    void Start()
    {
        nextframe = false;
        psm = ps.main;
        psm.simulationSpeed = (float)(m.fakebpm/10);
    }

    // Update is called once per frame
    void Update()
    {
        if(nextframe)
        {
            //beat();
        }
    }

    public void beat()
    {
        //Vector3 spawnpos = new Vector3(pos,0,0);
        //Instantiate(spawn,spawnpos, Quaternion.identity);
        //pos++;
        int bleh;
        if(int.TryParse(nextframetype,out bleh))
        {
            psm.startColor = Color.red;
        }
        else
        {
            psm.startColor = Color.blue;
        }
        ps.Play();
        nextframe = false;
    }

    public void SetNextFrame(string type)
    {
        switch (type)
        {
            case "e":
                
                break;
            
            case "and":
                
                break;
            
            case "a":
            
                break;

            default:
                
                break;
        }
        //Debug.Log(type);
        nextframetype = type;
        nextframe = true;
    }
}
