﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject combat_manager;
    CombatManager combat_manager_script;
    int frametype;
    public int hp = 100;
    int player_number;
    // Start is called before the first frame update
    public bool frame_open;
    public bool kb;
    Move[] combo;
    public int current_beat = 0;
    AudioSource[] asources;
    bool[] stopsources;
    public float volume;
    public float notetime = .25f;
    SerialPort sp = new SerialPort("COM5", 9600);
    int signal;

    public void ReadArudino()
    {
        while (sp.IsOpen)
        {
            signal = sp.ReadByte();
        }
    }


    void Start()
    {
        //audiosource = GetComponent<AudioSource>();
        asources = GetComponents<AudioSource>();
        for(int i = 0; i < asources.Length; i++)
        {
            asources[i].volume = volume;
        }
        stopsources = new bool[7];
        


        combo = new Move[4];
        for(int i = 0; i < 4; i++)
        {
            combo[i] = new Move(-1);
        }


        frame_open = false;
        combat_manager = GameObject.FindGameObjectWithTag("CombatManager");
        combat_manager_script = combat_manager.GetComponent<CombatManager>();

        if(gameObject.tag == "p1")
        {
            player_number = 1;
        }

        if (gameObject.tag == "p2")
        {
            player_number = 2;
        }

        if (player_number == 2)
        {
            sp = new SerialPort("COM6", 9600);
        }

        sp.Open();
        sp.ReadTimeout = 100;
        Thread readInput = new Thread(new ThreadStart(ReadArudino));
        readInput.IsBackground = true;
        readInput.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frame_open && sp.IsOpen)
        {
            int notes_played = 0;
            int move = -1;
            int pos = 0;
            bool[] sigs = new bool[8];

            try
            {
                if (signal == 0)
                {
                    if (current_beat == 1)
                    {
                        pos = 3;
                    }
                    else if (current_beat == 2)
                    {
                        pos = 0;
                    }
                    else if (current_beat == 3)
                    {
                        pos = 1;
                    }
                    else if (current_beat == 4)
                    {
                        pos = 2;
                    }
                    combo[pos] = new Move(-1);
                    return;
                }
                print(signal);
                frame_open = false;
                
                    switch(signal)
                    {
                        case 1:
                            sigs[0] = true;
                            break;

                        case 2:
                            sigs[1] = true;
                            break;

                        case 3:
                            sigs[2] = true;
                            break;

                        case 4:
                            sigs[3] = true;
                            break;

                        case 5:
                            sigs[4] = true;
                            break;

                        case 6:
                            sigs[5] = true;
                            break;

                        case 7:
                            sigs[6] = true;
                            break;
                    case 8:
                        sigs[7] = true;
                        break;
                        
                    }
                }
   
            catch(System.Exception)
            {
                throw;
            }


            if (sigs[0])
            {
                //print("YUH");
                move = 1;
                frame_open = false;
                notes_played++;

                stopsources[move - 1] = false;
                asources[move - 1].Play();
                StartCoroutine("StopNote", move - 1);
            }
            else if (sigs[1])
            {
                move = 2;
                frame_open = false;
                notes_played++;

                stopsources[move - 1] = false;
                asources[move - 1].Play();
                StartCoroutine("StopNote", move - 1);
            }
            else if (sigs[2])
            {
                move = 3;
                frame_open = false;
                notes_played++;

                stopsources[move - 1] = false;
                asources[move - 1].Play();
                StartCoroutine("StopNote", move - 1);
            }
            else if (sigs[3])
            {
                move = 8;
                frame_open = false;
                notes_played++;

                //stopsources[move - 1] = false;
                asources[0].Play();
                asources[2].Play();
                asources[4].Play();
                StartCoroutine("StopNote", 0);
                StartCoroutine("StopNote", 2);
                StartCoroutine("StopNote", 4);
            }
            else if (sigs[4])
            {
                move = 5;
                frame_open = false;
                notes_played++;

                stopsources[move - 1] = false;
                asources[move - 1].Play();
                StartCoroutine("StopNote", move - 1);
            }
            else if (sigs[5])
            {
                move = 6;
                frame_open = false;
                notes_played++;

                stopsources[move - 1] = false;
                asources[move - 1].Play();
                StartCoroutine("StopNote", move - 1);
            }
           else if (sigs[6])
            {
                move = 7;
                frame_open = false;
                notes_played++;

                stopsources[move - 1] = false;
                asources[move - 1].Play();
                StartCoroutine("StopNote", move - 1);
            }

            else if(sigs[7])
            {
                move = 8;
                frame_open = false;
                notes_played++;

                asources[0].Play();
                asources[2].Play();
                asources[4].Play();
                StartCoroutine("StopNote", 0);
                StartCoroutine("StopNote", 2);
                StartCoroutine("StopNote", 4);
            }

            //print(notes_played);

            for (int i = 0; i < stopsources.Length; i++)
            {
                if (stopsources[i] && asources[i].isPlaying)
                {
                    asources[i].Stop();
                }
            }

      

            pos = 0;
            if (current_beat == 1)
            {
                pos = 3;
            }
            else if (current_beat == 2)
            {
                pos = 0;
            }
            else if (current_beat == 3)
            {
                pos = 1;
            }
            else if (current_beat == 4)
            {
                pos = 2;
            }
            
            if (notes_played > 0)
            {

                if (notes_played > 1)
                {
                    combo[pos] = new Move(8);
                }
                else
                { 
                    combo[pos] = new Move(move);
                }
            }
            else
            {
                combo[pos] = new Move(-1);
            }
        }


        else if (frame_open && kb)
        {
            //If using keyboard
            int notes_played = 0;
            int move = -1;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                move = 1;
                frame_open = false;
                notes_played++;

                stopsources[0] = false;
                asources[0].Play();
                StartCoroutine("StopNote",move-1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                move = 2;
                frame_open = false;
                notes_played++;

                stopsources[1] = false;
                asources[1].Play();
                StartCoroutine("StopNote", move - 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                move = 3;
                frame_open = false;
                notes_played++;

                stopsources[2] = false;
                asources[2].Play();
                StartCoroutine("StopNote", move - 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                move = 4;
                frame_open = false;
                notes_played++;

                stopsources[3] = false;
                asources[3].Play();
                StartCoroutine("StopNote", move - 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                move = 5;
                frame_open = false;
                notes_played++;

                stopsources[4] = false;
                asources[4].Play();
                StartCoroutine("StopNote", move - 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                move = 6;
                frame_open = false;
                notes_played++;

                stopsources[5] = false;
                asources[5].Play();
                StartCoroutine("StopNote", move - 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                move = 7;
                frame_open = false;
                notes_played++;

                stopsources[6] = false;
                asources[6].Play();
                StartCoroutine("StopNote", move - 1);
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                stopsources[0] = true;
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                stopsources[1] = true;
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                stopsources[2] = true;
            }
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                stopsources[3] = true;
            }
            if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                stopsources[4] = true;
            }
            if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                stopsources[5] = true;
            }
            if (Input.GetKeyUp(KeyCode.Alpha7))
            {
                stopsources[6] = true;
            }


            for (int i = 0; i < stopsources.Length; i++)
            {
                if (stopsources[i] && asources[i].isPlaying)
                {
                    asources[i].Stop();
                }
            }

            int pos = 0;
            if (current_beat == 1)
            {
                pos = 3;
            }
            if (current_beat == 2)
            {
                pos = 0;
            }
            if (current_beat == 3)
            {
                pos = 1;
            }
            if (current_beat == 4)
            {
                pos = 2;
            }

            if (notes_played > 0)
            {

                if (notes_played > 1)
                {
                    combo[pos] = new Move(8);
                }
                else
                {
                    combo[pos] = new Move(move);
                }
            }
            else
            {
                print("ree");
                combo[pos] = new Move(-1);
            }
        }
            //combo[frametype] = "f";
    }




    public void Beat()
    {
        //Vector3 spawnpos = new Vector3(pos,0,0);
        //Instantiate(spawn,spawnpos, Quaternion.identity);
        //pos
        //frame_open = false;
    }



    public void StartFrame(int beat)
    {
        frametype = beat;
        current_beat++;
        if(current_beat == 5)
        {
            current_beat = 1;
        }
        frame_open = true;

    }
    public void EndFrame()
    {
        frame_open = false;
        //Debug.Log("CURRENT BEAT: " +current_beat);
        if(current_beat == 1)
        {
            ParseCombo();
            ClearCombo();
            combat_manager_script.contest = true;
        }
    }

    private void ParseCombo()
    {
        string HML = "";
        string PJB = "";
        bool nul = false;
        for (int i = 0; i < 4; i++)
        {
            if (combo[i] == null)
            {
                nul = true;
            }
        }
        if (nul)
        {
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            print(combo[i].getNote() + "\n");
        }


        if(combo[0].IsRest() && !combo[1].IsRest() && combo[2].IsRest() && !combo[3].IsRest())
        {
            if(combo[1].IsChord() && combo[3].IsChord())
            {
                PJB = "block";

            }
            else if(!combo[1].IsRest() && !combo[3].IsRest())
            {
                if(combo[3].IsLow())
                {
                    HML = "low";
                    PJB = "jab";
                }
                
                else if(combo[3].IsMid())
                {
                    HML = "mid";
                    PJB = "jab";
                }

                else if(combo[3].IsHigh())
                {
                    HML = "high";
                    PJB = "jab";
                }
            }
        }

        bool powerpossible = true;
        for(int i = 0; i < combo.Length; i++)
        {
            if(combo[i].IsRest())
            {
                powerpossible = false;
            }
        }

        if(powerpossible && combo[0].getNote() == combo[1].getNote()-1 && combo[2].getNote() == combo[0].getNote())
        {
            if(combo[3].IsLow())
            {
                HML = "low";
                PJB = "power";
            }

            else if(combo[3].IsMid())
            {
                HML = "mid";
                PJB = "power";
            }

            else if(combo[3].IsHigh())
            {
                HML = "high";
                PJB = "power";
            }
        }

        combat_manager_script.RecieveMove(HML,PJB,player_number);
    }

    void ClearCombo()
    {
        combo = new Move[4];
    }

    public void Damage(int dmg)
    {
        hp-=dmg;

        if(hp<=0)
        {
            if(gameObject.tag == "p1")
            {
                //combat_manager_script.P1Lose();
            }
            else if(gameObject.tag == "p2")
            {
                //combat_manager_script.P2Lose();
            }

            GameObject.FindGameObjectWithTag("metronome").GetComponent<BetterMetronome>().StopMetronome();
            StartCoroutine("StopGame");
        }
    }

    IEnumerator StopNote(int note)
    {
        yield return new WaitForSeconds(notetime);
        asources[note].Stop();
    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
}
