using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAttack : MonoBehaviour
{
    
    public float hiheight;
    public float lowheight;
    public float speed = .00001f;
    public float acceleration = .00001f;
    public float speedcap = .00001f;
    public GameObject otheratt;
    GameObject p1;
    GameObject p2;
    Vector3 start;
    Vector3 end;
    string startplayer;
    int damage;
    float counter;
    string target;
    int height;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter+=speed;
        speed+=acceleration;
        acceleration*=2;
        speed = Mathf.Min(speed,speedcap);
        transform.position = MathParabola.Parabola(start,end,height,counter);
    }

    public void SetPath(string astartplayer, int adamage, string atarget, string aspeed)
    {
        p1 = GameObject.FindGameObjectWithTag("p1");
        p2 = GameObject.FindGameObjectWithTag("p2");
        startplayer = astartplayer;
        damage = adamage;
        target = atarget;

        if(startplayer == "p1")
        {
            start = p1.transform.position;
            end = p2.transform.position;
        }
        else if(startplayer == "p2")
        {
            start = p2.transform.position;
            end = p1.transform.position;
            
        }


        start.y -= 3;
        if(target == "high")
        {
            end.y += 3f;
            height = 3;
        }
        if(target == "mid")
        {
            end.y -= 3f;
            height = 0;
        }
        if(target == "low")
        {
            end.y -= 14.6f;
            height = -3;
        }

        if(aspeed == "power")
        {
            speed = .001f;
        }
        if(aspeed == "jab")
        {
            speed = .01f;
        }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "noteattack")
        {
            print("HIT NOTE");
            Destroy(other);
            Destroy(gameObject);
        } 
        if(startplayer == "p1")
        {
            if(other.gameObject.tag == "p2")
            {
                p2.SendMessage("Damage",damage);
                if(otheratt != null)
                {
                    Destroy(otheratt);
                }
                Destroy(gameObject);
            }
        }
        if(startplayer == "p2")
        {
            if(other.gameObject.tag == "p1")
            {
                p1.SendMessage("Damage",damage);
                if(otheratt != null)
                {
                    Destroy(otheratt);
                }
                Destroy(gameObject);
            }
        }
    }
}
