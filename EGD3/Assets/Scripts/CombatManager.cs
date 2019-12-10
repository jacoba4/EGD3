using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


struct P1
{
    public string HML;
    public string PJB;
}

struct P2
{
    public string HML;
    public string PJB;
}

public class CombatManager : MonoBehaviour
{
    public int jab_damage = 10;
    public int power_damage = 20;
    public int chip_damage = 5;
    P1 p1;
    P2 p2;
    public GameObject p1win;
    public GameObject p2win;
    public bool contest;
    public GameObject noteattack;
    private void Start()
    {
        p1 = new P1();
        p2 = new P2();
        contest = false;
    }

    void Update()
    {
        if(contest)
        {
            Contest();
            contest = false;
        }
    }

    //Recieves move from the specified player
    public void RecieveMove(string HML, string PJB, int player)
    {
        if(player == 1)
        {
            p1.HML = HML;
            p1.PJB = PJB;
        }
        if(player == 2)
        {
            p2.HML = HML;
            p2.PJB = PJB;
        }
    }

    //Called from metronome when input window ends
    //Compares both player inputs and determines who wins
    //Call functions for losing hp inside
    private void Contest()
    {
        Player p1s = GameObject.FindGameObjectWithTag("p1").GetComponent<Player>();
        Player p2s = GameObject.FindGameObjectWithTag("p2").GetComponent<Player>();
        int hml = 2;

        print("Player 1: " + p1.HML + ", " + p1.PJB);
        print("Player 2: " + p2.HML + ", " + p2.PJB);
        /*
        switch (p1.PJB)
        {
            case "power":
                switch (p2.PJB)
                {
                    case "power":
                        hml = HMLP1Wins();
                        switch(hml)
                        {
                            case 0:
                                //p1 loses
                                CreateAttack("p2",power_damage);
                                //p1s.Damage(power_damage);
                                break;
                            case 1:
                                //p1 wins
                                CreateAttack("p1",power_damage);
                                //p2s.Damage(power_damage);
                                break;
                            case 2:
                                //tie
                                break;
                        }
                        break;
                    case "jab":
                        //p2 wins
                        CreateAttack("p2",jab_damage);
                        //p1s.Damage(jab_damage);
                        break;
                    case "block":
                        //p1 wins
                        CreateAttack("p1",power_damage);
                        //p2s.Damage(power_damage);
                        break;
                    default:
                        //p1 wins
                        CreateAttack("p1",power_damage);
                        //p2s.Damage(power_damage);
                        break;
                }
                break;

            case "jab":
                switch (p2.PJB)
                {
                    case "power":
                        //p1 wins
                        CreateAttack("p1",jab_damage);
                        //p2s.Damage(jab_damage);
                        break;
                    case "jab":
                        //check hml
                        hml = HMLP1Wins();
                        switch(hml)
                        {
                            case 0:
                                //p1 loses
                                CreateAttack("p2",jab_damage);
                                //p1s.Damage(jab_damage);
                                break;
                            case 1:
                                //p1 wins
                                CreateAttack("p1",jab_damage);
                                //p2s.Damage(jab_damage);
                                break;
                            case 2:
                                //tie
                                break;
                        }
                        break;
                    case "block":
                        //p2 chip dmg? or nothing
                        CreateAttack("p1",chip_damage);
                        //p2s.Damage(chip_damage);
                        break;
                    default:
                        //p1 wins
                        CreateAttack("p1",jab_damage);
                        //p2s.Damage(jab_damage);
                        break;
                }
                break;
            case "block":
                switch (p2.PJB)
                {
                    case "power":
                        //p1 loses
                        CreateAttack("p2",power_damage);
                        //p1s.Damage(power_damage);
                        break;
                    case "jab":
                        //p1 chip dmg? or nothing
                        CreateAttack("p2",chip_damage);
                        //p1s.Damage(chip_damage);
                        break;
                    case "block":
                        //nothing
                        break;
                    default:
                        //nothing
                        break;
                }
                break;

            default:
                switch(p2.PJB)
                {
                    case "power":
                        //p1 loses
                        CreateAttack("p2",power_damage);
                        //p1s.Damage(power_damage);
                        break;
                    case "jab":
                        //p1 loses
                        CreateAttack("p2",jab_damage);
                        //p1s.Damage(jab_damage);
                        break;
                    case "block":
                        //nothing happens
                        break;
                    default:
                        //nothing happens
                        break;
                }
                break;
        }*/
        
        GameObject p1att = null;
        GameObject p2att = null;
        if(p1.PJB == "power")
        {
            p1att = CreateAttack("p1",power_damage);
        }
        if(p1.PJB == "jab")
        {
            p1att = CreateAttack("p1",jab_damage);
        }
        if(p1.PJB == "block")
        {
            //CREATE BLOCK
        }
        if(p2.PJB == "power")
        {
            p2att = CreateAttack("p2",power_damage);
        }
        if(p2.PJB == "jab")
        {
            p2att = CreateAttack("p2",jab_damage);
        }
        if(p2.PJB == "block")
        {
            //CREATE BLOCK
        }

        if(p1att != null)
        {
            p1att.GetComponent<NoteAttack>().otheratt = p2att;
        }
        if(p2att != null)
        {
            p2att.GetComponent<NoteAttack>().otheratt = p1att;
        }
        Reset();
    }


    private int HMLP1Wins()
    {
        //Returns
        //0: P1 loses
        //1: P1 wins
        //2: tie
        switch(p1.HML)
        {
            case "high":
                switch(p2.HML)
                {
                    case "high":
                        //Tie, nothing happens
                        break;
                    
                    case "mid":
                        //p2 loses
                        return 1;
                    
                    case "low":
                        //p1 loses
                        return 0;
                }
                break;
            
            case "mid":
                switch(p2.HML)
                {
                    case "high":
                        //p1 loses
                        return 0;
                    
                    case "mid":
                        //tie nothing happens
                        return 2;

                    case "low":
                        //p1 wins
                        return 1;
                }
                break;
            
            case "low":
                switch(p2.HML)
                {
                    case "high":
                        //p1 wins
                        return 1;

                    case "mid":
                        //p1 loses
                        return 0;
                    
                    case "low":
                        //tie
                        return 2;
                }
            break;
        }
        return 2;
    }
    //Resets player inputs to defaults
    private void Reset()
    {
        p1 = new P1();
        p2 = new P2();
    }

    private GameObject CreateAttack(string astartplayer, int adamage)
    {
        Vector3 startpos = Vector3.zero;
        string target = "";
        string aspeed = "";
        if(astartplayer == "p1")
        {
            startpos = GameObject.FindGameObjectWithTag("p1").transform.position;
            target = p1.HML;
            aspeed = p1.PJB;
        }
        if(astartplayer == "p2")
        {
            startpos = GameObject.FindGameObjectWithTag("p2").transform.position;
            target = p2.HML;
            aspeed = p2.PJB;
        }

        startpos.y-= 3f;
        GameObject temp = Instantiate(noteattack,startpos,Quaternion.identity);
        temp.GetComponent<NoteAttack>().SetPath(astartplayer,adamage,target,aspeed);
        return temp;
    }

    public void P1Lose()
    {
        p2win.SetActive(true);
    }

    public void P2Lose()
    {
        p1win.SetActive(false);
    }

    private void StartSong()
    {
        GetComponent<AudioSource>().Play();
    }

}
