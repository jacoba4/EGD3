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
    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;
    private int beats = 0;



    public GameObject controller;
    private TestController controllerscript;
    void Start()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
        bpm = fakebpm*4;
        controllerscript = controller.GetComponent<TestController>();
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
            while (sample + n >= nextTick)
            {
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
                        //Debug.Log(beats);
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
                
                //Debug.Log("Tick: " + accent + "/" + signatureHi);
                
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }
}