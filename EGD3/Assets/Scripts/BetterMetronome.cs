using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMetronome : MonoBehaviour
{

    public int Base;
    public int Step;
    public float BPM;
    public int CurrentStep = 1;
    public int CurrentMeasure;
    public float inputwindow = 0.2f;
    public bool visuals = false;
    public bool players = false;
    public bool inputdelay = false;
    public bool prints = false;
    public GameObject cube;
    public TestController testcontroller;
    public GameObject player1;
    public GameObject player2;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject fight;
    public GameObject num;
    public GameObject e;
    public GameObject and;
    public GameObject a;
    private GameObject curr_shape;
    ParticleSystem beater;
    GameObject light;
    private Player playerscript1;
    private Player playerscript2;

    private float interval;
    private float nextTime;
    private float nextOpenTime;
    private float nextCloseTime;


    private void Start()
    {
        light = GameObject.FindGameObjectWithTag("light");
        beater = GameObject.FindGameObjectWithTag("beater").GetComponent<ParticleSystem>();
        StartMetronome();
        if(players)
        {
            playerscript1 = player1.GetComponent<Player>();
            playerscript2 = player2.GetComponent<Player>();
        }
        
    }
    public void StartMetronome()
    {
        StopCoroutine("PreTick");
        StopCoroutine("DoTick");
        StopCoroutine("PostTick");
        CurrentStep = 1;
        var multiplier = Base / 4f;
        var tmpInterval = 60f / BPM;
        interval = tmpInterval / multiplier;
        nextOpenTime = Time.time + 4;
        nextTime = Time.time + 4 + (inputwindow / 2); // set the relative time to now
        nextCloseTime = Time.time + 4 +(inputwindow / 2) + (inputwindow / 2);
        StartCoroutine("StartTicks");
    }
    public void Recieve_Arr(string[] arr)
    {

    }
    IEnumerator StartTicks()
    {
        three.SetActive(true);
        yield return new WaitForSeconds(1);
        three.SetActive(false);
        two.SetActive(true);
        yield return new WaitForSeconds(1);
        two.SetActive(false);
        one.SetActive(true);
        yield return new WaitForSeconds(1);
        one.SetActive(false);
        fight.SetActive(true);
        yield return new WaitForSeconds(1);
        fight.SetActive(false);

        StartCoroutine("PreTick");
        yield return new WaitForSeconds(inputwindow / 2);
        StartCoroutine("DoTick");
        GameObject.FindGameObjectWithTag("CombatManager").SendMessage("StartSong");
        yield return new WaitForSeconds(inputwindow / 2);
        StartCoroutine("PostTick");
    }
    IEnumerator PreTick()
    {
        for(; ; )
        {
            //Debug.Log("open: " + Time.time);
            if(visuals)
            {
                cube.SetActive(true);
            }
            
            if(players)
            {
                //Debug.Log("SHOULD BE: " + (CurrentStep-1));
                if(CurrentStep == 1 && CurrentMeasure == 1)
                {
                    playerscript1.StartFrame(CurrentStep);
                    playerscript2.StartFrame(CurrentStep);
                }
                else
                {
                    playerscript1.StartFrame(CurrentStep);
                    playerscript2.StartFrame(CurrentStep);
                }
                
            }
            
            nextOpenTime += interval; // add interval to our relative time
            yield return new WaitForSeconds(nextOpenTime - Time.time); // wait for the difference delta between now and expected next time of hit
        }
    }
    IEnumerator DoTick() // yield methods return IEnumerator
    {
        for (; ; )
        {
            //Debug.Log("bop: " + Time.time);
            AudioSource asource = GetComponent<AudioSource>();
            asource.Play();

            
            beater.Play();

            //testcontroller.SetNextFrame("test");
            // do something with this beat

            if (players)
            {
                if(!(CurrentMeasure == 0 && CurrentStep == 1))
                {
                    curr_shape.SetActive(false);
                }
                
                if (CurrentStep == 1)
                {
                    if(prints)
                    {
                        print(CurrentMeasure.ToString());
                    }
                    curr_shape = num;
                }
                else if (CurrentStep == 2)
                {
                    if(prints)
                    {
                        print("e");
                    }
                    curr_shape = e;
                }
                else if (CurrentStep == 3)
                {
                    if(prints)
                    {
                        print("and");
                    }
                    curr_shape = and;
                }
                else if (CurrentStep == 4)
                {
                    if(prints)
                    {
                        print("a");
                    }
                    curr_shape = a;
                }

                curr_shape.SetActive(true);
            }

            nextTime += interval; // add interval to our relative time
            if(inputdelay)
            {
                SendMessage("GetTime",Time.time);
            }
            yield return new WaitForSeconds(nextTime - Time.time); // wait for the difference delta between now and expected next time of hit
            CurrentStep++;
            if (CurrentStep > Step)
            {
                light.SendMessage("Flash");
                CurrentStep = 1;
                CurrentMeasure++;
            }
        }
    }

    IEnumerator PostTick()
    {
        for (; ; )
        {
            //Debug.Log("close: " + Time.time);
            if (visuals)
            {
                cube.SetActive(false);
            }

            if(players)
            {
                playerscript1.EndFrame();
                playerscript2.EndFrame();
            }
            
            nextCloseTime += interval; // add interval to our relative time
            //Debug.Log(nextCloseTime);
            yield return new WaitForSeconds(nextCloseTime - Time.time); // wait for the difference delta between now and expected next time of hit
        }
    }

    public void StopMetronome()
    {
        StopAllCoroutines();
        CurrentStep = 1;
        CurrentMeasure = 0;
    }
}
