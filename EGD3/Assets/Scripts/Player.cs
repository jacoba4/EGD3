using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string nextframetype;
    // Start is called before the first frame update
    public bool frame_open;
    void Start()
    {
        frame_open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (frame_open)
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
        frame_open = false;
    }

    public void StartFrame()
    {
        frame_open = true;
    }
    public void EndFrame()
    {
        frame_open = false;
    }
}
