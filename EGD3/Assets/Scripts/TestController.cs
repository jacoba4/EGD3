using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject spawn;
    int pos = -5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beat()
    {
        Vector3 spawnpos = new Vector3(pos,0,0);
        Instantiate(spawn,spawnpos, Quaternion.identity);
        pos++;
    }
}
