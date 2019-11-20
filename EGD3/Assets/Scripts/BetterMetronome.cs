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
    public float inputwindow;
    public GameObject cube;
    public TestController testcontroller;
    public GameObject player1;
    public GameObject player2;
    private Player playerscript1;
    private Player playerscript2;

    private float interval;
    private float nextTime;
    private float nextOpenTime;
    private float nextCloseTime;


    private void Start()
    {
        StartMetronome();
        playerscript1 = player1.GetComponent<Player>();
        playerscript2 = player2.GetComponent<Player>();
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
        nextOpenTime = Time.time + 2;
        nextTime = Time.time + 2 + (inputwindow / 2); // set the relative time to now
        nextCloseTime = Time.time + 2 +(inputwindow / 2) + (inputwindow / 2);
        StartCoroutine("StartTicks");
    }

    IEnumerator StartTicks()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine("PreTick");
        yield return new WaitForSeconds(inputwindow / 2);
        StartCoroutine("DoTick");
        yield return new WaitForSeconds(inputwindow / 2);
        StartCoroutine("PostTick");
    }
    IEnumerator PreTick()
    {
        for(; ; )
        {
            Debug.Log("open: " + Time.time);
            cube.SetActive(true);
            playerscript1.StartFrame();


            // do something with this beat



            nextOpenTime += interval; // add interval to our relative time
            yield return new WaitForSeconds(nextOpenTime - Time.time); // wait for the difference delta between now and expected next time of hit
        }
    }
    IEnumerator DoTick() // yield methods return IEnumerator
    {
        for (; ; )
        {
            Debug.Log("bop: " + Time.time);
            AudioSource asource = GetComponent<AudioSource>();
            asource.Play();


            testcontroller.SetNextFrame("test");
            // do something with this beat



            nextTime += interval; // add interval to our relative time
            yield return new WaitForSeconds(nextTime - Time.time); // wait for the difference delta between now and expected next time of hit
            CurrentStep++;
            if (CurrentStep > Step)
            {
                CurrentStep = 1;
                CurrentMeasure++;
            }
        }
    }

    IEnumerator PostTick()
    {
        for (; ; )
        {
            Debug.Log("close: " + Time.time);
            cube.SetActive(false);
            playerscript1.EndFrame();


            // do something with this beat



            nextCloseTime += interval; // add interval to our relative time
            yield return new WaitForSeconds(nextCloseTime - Time.time); // wait for the difference delta between now and expected next time of hit
        }
    }
}
