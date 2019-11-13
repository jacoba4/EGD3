using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string nextframetype;
    // Start is called before the first frame update
    public bool nextframe;
    void Start()
    {
        nextframe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextframe)
        {
            if (Input.GetKeyDown("space"))
            {
                print("timing works");
            }
            Beat();
        }
    }
    public void Beat()
    {
        //Vector3 spawnpos = new Vector3(pos,0,0);
        //Instantiate(spawn,spawnpos, Quaternion.identity);
        //pos
        nextframe = false;
    }

    public void SetNextFrame(string type)
    {
        nextframetype = type;
        nextframe = true;
    }
}
