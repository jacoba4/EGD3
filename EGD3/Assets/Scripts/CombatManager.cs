using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    P1 p1;
    P2 p2;
    private void Start()
    {
        p1 = new P1();
        p2 = new P2();
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
        switch (p1.PJB)
        {
            case "power":
                switch (p2.PJB)
                {
                    case "power":
                        //check hml
                        break;
                    case "jab":
                        //p2 wins
                        break;
                    case "block":
                        //p1 wins
                        break;
                    case "":
                        //p1 wins
                        break;
                }
                break;

            case "jab":
                switch (p2.PJB)
                {
                    case "power":
                        //p1 wins
                        break;
                    case "jab":
                        //check hml
                        break;
                    case "block":
                        //p2 chip dmg? or nothing
                        break;
                    case "":
                        //p1 wins
                        break;
                }
                break;
            case "block":
                switch (p2.PJB)
                {
                    case "power":
                        //p1 loses
                        break;
                    case "jab":
                        //p1 chip dmg? or nothing
                        break;
                    case "block":
                        //nothing
                        break;
                    case "":
                        //nothing
                        break;
                }
                break;

            case "":
                switch(p2.PJB)
                {
                    case "power":
                        //p1 loses
                        break;
                    case "jab":
                        //p1 loses
                        break;
                    case "block":
                        //nothing happens
                        break;
                    case "":
                        //nothing happens
                        break;
                }
                break;
        }

        Reset();
    }

    //Resets player inputs to defaults
    private void Reset()
    {
        p1 = new P1();
        p2 = new P2();
    }

}
