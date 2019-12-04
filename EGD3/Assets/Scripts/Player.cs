using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Player : MonoBehaviour
{
    public GameObject combat_manager;
    CombatManager combat_manager_script;
    int frametype;
    int hp = 100;
    int player_number;
    // Start is called before the first frame update
    public bool frame_open;
    Move[] combo = new Move[4];
    int current_beat = 0;
    Move[] lightmove;
    SerialPort sp = new SerialPort("COM5", 9600);

    void Start()
    {
        if (gameObject.name.Equals("GameObject (1)"))
        {
            sp = new SerialPort("COM6", 9600);
        }
        lightmove = new Move[4];
        frame_open = false;
        //combat_manager = GameObject.FindGameObjectWithTag("CombatManager");
        //combat_manager_script = combat_manager.GetComponent<CombatManager>();
        sp.Open();
        sp.ReadTimeout = 40;
    }

    // Update is called once per frame
    void Update()
    {
        if (frame_open && sp.IsOpen)
        {
            int signal=sp.ReadByte();

            if (signal==1)
            {
                print(signal);
                combo[current_beat] = new Move(1);
                frame_open = false;
            }
            if (signal == 2)
            {
                print(signal);
                combo[current_beat] = new Move(2);
                frame_open = false;
            }
            if (signal == 3)
            {
                print(signal);
                combo[current_beat] = new Move(3);
                frame_open = false;
            }
            if (signal == 4)
            {
                print(signal);
                combo[current_beat] = new Move(4);
                frame_open = false;
            }
            if (signal == 5)
            {
                print(signal);
                combo[current_beat] = new Move(5);
                frame_open = false;
            }
            if (signal == 6)
            {
                print(signal);
                combo[current_beat] = new Move(6);
                frame_open = false;
            }
            if (signal == 7)
            {
                print(signal);
                combo[current_beat] = new Move(7);
                frame_open = false;
            }
            //combo[frametype] = "f";
        }
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
        frame_open = true;
        current_beat = beat;
        combo[beat] = null;
    }
    public void EndFrame()
    {
        frame_open = false;
        if(current_beat == 1)
        {
            ParseCombo();
            ClearCombo();
        }
    }

    private void ParseCombo()
    {
        string HML = "";
        string PJB = "";
    
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
}
