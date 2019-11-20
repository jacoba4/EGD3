using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject combat_manager;
    int frametype;
    // Start is called before the first frame update
    public bool frame_open;
    string[] combo = new string[4];
    void Start()
    {
        frame_open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (frame_open && Input.GetKeyDown("space"))
        {
            print("timing works");
            combo[frametype] = "f";
            frame_open = false;
        }
    }
    public void Beat()
    {
        //Vector3 spawnpos = new Vector3(pos,0,0);
        //Instantiate(spawn,spawnpos, Quaternion.identity);
        //pos
        frame_open = false;
    }

    public void StartFrame(int beat)
    {
        frametype = beat;
        frame_open = true;
        combo[beat] = null;
    }
    public void EndFrame()
    {
        if (frametype == 3)
        {
            if (tag.Equals("taco"))
            {
                Taco_Surpise script = this.GetComponent<Taco_Surpise>();
                script.GetArr(combo);
            }
        }
        frame_open = false;
        
    }
}
