using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDelay : MonoBehaviour
{

    public Text countdowntext;
    public GameObject metronome;


    private List<float> offsets;
    private List<float> realtimes;
    private bool started = false;
    private bool actualstarted = false;
    private int actualcountdown = 20;
    // Start is called before the first frame update
    void Start()
    {
        offsets = new List<float>();
        realtimes = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!started)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                started = true;
                StartCoroutine("Countdown");
                metronome.SetActive(true);
            }
        }
        else
        {
            if(actualstarted)
            {
                if (countdowntext.text != "0")
                {
                    actualcountdown--;
                    countdowntext.text = actualcountdown.ToString();
                    offsets.Add(Time.time);
                }
                else
                {
                    started = false;
                    metronome.SetActive(false);
                    CalcOffset();
                }
            }
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        countdowntext.text = "2";
        yield return new WaitForSeconds(1f);
        countdowntext.text = "1";
        yield return new WaitForSeconds(1f);
        countdowntext.text = "20";
        actualstarted = true;
    }

    public void GetTime(float realtime)
    {
        realtimes.Add(realtime);
    }

    private void CalcOffset()
    {
        float avg = 0;
        int len = 0;
        if(offsets.Count < realtimes.Count)
        {
            len = offsets.Count;
        }
        else
        {
            len = realtimes.Count;
        }
        for(int i = 0; i < len; i++)
        {
            if(i == 0)
            {
                avg = (realtimes[i] + offsets[i]) / 2;
            }
            else
            {
                avg = ((realtimes[i] + offsets[i] / 2) + avg) / 2;
            }
        }

        print("avg: " + avg);
    }
}
