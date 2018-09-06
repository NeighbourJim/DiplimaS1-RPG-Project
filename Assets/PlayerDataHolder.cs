using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataHolder : MonoBehaviour {

    public static Vector3 PlayerLocation { get; set; }

    static bool inEncounterZone = false;
    public static bool InEncounterZone
    {
        get
        {
            return inEncounterZone;
        }
        set
        {
            inEncounterZone = value;
        }
    }

    static bool moving = false;
    public static bool Moving
    {
        get
        {
            return moving;
        }
        set
        {
            moving = value;
        }
    }

    public static MonData[] playerTeam = new MonData[5];

    Monpedia mp;

    private void Start()
    {
        mp = GetComponent<Monpedia>();
    }

    public void SetData()
    {
        playerTeam[0] = ScriptableObject.CreateInstance<MonData>();
        playerTeam[0].CreateFromBase(mp.FindByID(12));
        playerTeam[0].level = 11;
        playerTeam[0].GenerateMoveset();

        SceneManager.LoadScene("BaseBattle");
    }



}
