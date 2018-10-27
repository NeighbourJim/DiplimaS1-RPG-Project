using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TrainerMonster
{
    public MonData BaseMonster;
    public int MonsterLevel;
}

public class Trainer : MonoBehaviour {

    public TrainerType trainerType = TrainerType.Monster_Trainer;
    public string TrainerName = "?TRAINERNAME?";
    public TrainerMonster[] trainerTeam;
    public List<string> DefeatQuote;
    int PrizeMoney = 0;
    public bool defeated;


    public void CreateFromBase(Trainer t)
    {
        trainerType = t.trainerType;
        TrainerName = t.TrainerName;
        trainerTeam = t.trainerTeam;
        DefeatQuote = t.DefeatQuote;
        PrizeMoney = t.PrizeMoney;
    }
    private void OnValidate()
    {
        if(trainerTeam.Length > 6)
        {
            Debug.LogWarning("A trainer cannot have more than 6 monsters.");
            Array.Resize(ref trainerTeam, 6);
        }
        else if(trainerTeam.Length <= 0)
        {
            Debug.LogWarning("A trainer must have at least 1 monster.");
            Array.Resize(ref trainerTeam, 1);
        }

        for (int i = 0; i < trainerTeam.Length; i++)
        {
            if(trainerTeam[i].BaseMonster == null)
            {
                Debug.LogError("No monster set for slot " + i.ToString() + ".");
            }
            if(trainerTeam[i].MonsterLevel < 1)
            {
                Debug.LogWarning("A trainer monster must be at least level 1");
                trainerTeam[i].MonsterLevel = 1;
            }
            else if (trainerTeam[i].MonsterLevel > 100)
            {
                Debug.LogWarning("A trainer monster must be at most level 100");
                trainerTeam[i].MonsterLevel = 100;
            }
        }
    }
}
