using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class jump : MonoBehaviour
{
    // Start is called before the first frame update
    private float jumpamount = 70;
    SerialPort sp = new SerialPort("COM8", 9600);
    public Rigidbody rb;

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 40;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            jumpBoi(sp.ReadByte());
            print(sp.ReadByte());
        }
    }

    void jumpBoi(int jumped)
    {
        if (jumped == 1)
        {
            rb.AddForce(transform.up * jumpamount);
            print("fuck this worrked");
        }
    }
}
