using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monpedia : MonoBehaviour {

    public MonData[] monpedia;

    public MonData FindByID(int idToFind)
    {
        foreach(MonData mon in monpedia)
        {
            if(mon != null)
            {
                if(mon.monID == idToFind)
                {
                    return mon;
                }
            }
        }
        return null;
    }

    public MonData FindByName(string nameToFind)
    {
        foreach (MonData mon in monpedia)
        {
            if (mon != null)
            {
                if (mon.monName == nameToFind)
                {
                    return mon;
                }
            }
        }
        return null;
    }
}
