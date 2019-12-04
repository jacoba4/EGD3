using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    int note;
    string target;
    public Move(int anote)
    {
        note = anote;

        if(note == 1 || note == 2)
        {
            target = "low";
        }
        else if(note == 3 || note == 4 || note == 5)
        {
            target = "mid";
        }
        else if(note == 6 || note == 7)
        {
            target = "high";
        }
        else
        {
            target = "";
        }
    }

    public string getTarget()
    {
        return target;
    }

    public int getNote()
    {
        return note;
    }

    public bool IsRest()
    {
        return(note == -1 && target == "");        
    }

    public bool IsChord()
    {
        return(note > 7);
    }

    public bool IsLow()
    {
        return(target=="low");
    }

    public bool IsMid()
    {
        return(target=="mid");
    }

    public bool IsHigh()
    {
        return(target=="high");
    }
}
