using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    private double bpm = 0;
    public double fakebpm = 140.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    public float inputwindow = 400f;
    private double nextTick = 0.0F;
    private double nextStartTick = 0.0F;
    private double nextEndTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;
    private int beats = 0;
    private double starttime = 0;
    private double endtime = 0;
    private double windowtime = 0;
    private bool getstarttime;
    private bool getendtime;
    private bool windowopen;
    public GameObject windowblock;


    public GameObject controller;
    private TestController controllerscript;
    public GameObject player1;
    public GameObject player2;
    private Player playerscript1;
    private Player playerscript2;
    void Start()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        nextStartTick = (startTick - (inputwindow / 2)) * sampleRate;
        nextEndTick = (startTick + (inputwindow / 2)) * sampleRate;
        Debug.Log(nextStartTick + "\n" + nextTick + "\n" + nextEndTick);
        running = true;
        bpm = fakebpm*4;
        controllerscript = controller.GetComponent<TestController>();
        playerscript1 = player1.GetComponent<Player>();
        playerscript2 = player2.GetComponent<Player>();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if(!running)
        {
            return;
        }

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int datalen = data.Length / channels;
        int n = 0;
        while (n < datalen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while(i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            //Debug.Log(n);
            while (sample + n >= nextStartTick)
            {
                //Debug.Log("start window: " + AudioSettings.dspTime);
                nextStartTick += samplesPerTick;
                getstarttime = true;
            }
         
            while (sample + n >= nextTick)
            {
                getendtime = true;
                nextTick += samplesPerTick;
                amp = 1.0f;
                if(++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                switch(accent)
                {
                    case 1:
                        beats++;
                        //Debug.Log(beats.ToString());
                        controllerscript.SetNextFrame(beats.ToString());
                        
                        break;
                    case 2:
                        //Debug.Log("e");
                        controllerscript.SetNextFrame("e");
                        
                        break;
                    case 3:
                        //Debug.Log("and");
                        controllerscript.SetNextFrame("and");
                        
                        break;
                    case 4:
                        //Debug.Log("a");
                        controllerscript.SetNextFrame("a");
                        
                        break;
                }
                //CloseWindow();
                //Debug.Log("Tick: " + accent + "/" + signatureHi);
                
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }


    private void CloseWindow()
    {
        windowtime = endtime - starttime;
    }

    private void FixedUpdate()
    {
        if(getstarttime)
        {
            starttime = Time.fixedTime;
            getstarttime = false;
            windowblock.SetActive(true);
            Debug.Log("open window: " + Time.fixedTime);
        }

        if(getendtime)
        {
            Debug.Log("tick: " + Time.fixedTime + "time since last tick: " + (Time.fixedTime-endtime));
            endtime = Time.fixedTime;
            windowtime = endtime - starttime;
            getendtime = false;
            windowopen = true;
            
        }
        if(windowopen)
        {
            double temptime = endtime + windowtime;
            if (Time.fixedTime >= temptime - 0.01f && Time.fixedTime <= endtime + windowtime + 0.01f)
            {
                windowblock.SetActive(false);
                Debug.Log("close window: " + Time.time);
                windowopen = false;
            }
        }
        
    }
}