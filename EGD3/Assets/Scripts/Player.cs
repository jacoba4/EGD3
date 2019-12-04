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
    Move[] combo;
    int current_beat = 0;
    Move[] lightmove;
    SerialPort sp = new SerialPort("COM5", 9600);

    void Start()
    {
        if (gameObject.name.Equals("GameObject (1)"))
        {
            sp = new SerialPort("COM6", 9600);
        }


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

        if(gameObject.tag == "p2")
        {
            player_number = 2;
        }

        sp.Open();
        sp.ReadTimeout = 40;
    }

    // Update is called once per frame
    void Update()
    {
        if (frame_open)
        {
            if(sp.IsOpen)
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
                return;
            }

            //If using keyboard
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("1 pressed");
                combo[current_beat] = new Move(1);
                frame_open = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                combo[current_beat] = new Move(2);
                frame_open = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                combo[current_beat] = new Move(3);
                frame_open = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                combo[current_beat] = new Move(4);
                frame_open = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                combo[current_beat] = new Move(5);
                frame_open = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                combo[current_beat] = new Move(6);
                frame_open = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha7))
            {
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
        Debug.Log("CURRENT BEAT: " +current_beat);
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
        for(int i = 0; i < 4; i++)
        {
            if(combo[i] == null)
            {
                nul = true;
            }
        }
        if(nul)
        {
            return;
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
                combat_manager_script.P1Lose();
            }
            else if(gameObject.tag == "p2")
            {
                combat_manager_script.P2Lose();
            }
        }
    }
}
