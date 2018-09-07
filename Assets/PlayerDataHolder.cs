using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHolder : MonoBehaviour {

    public static Vector3 PlayerLocation { get; set; }
    public static Vector3 CameraLocation { get; set; }

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
    GameObject player;
    GameObject camera;

    private void Start()
    {
        mp = GetComponent<Monpedia>();
    }

    public void SetData()
    {
        playerTeam[0] = ScriptableObject.CreateInstance<MonData>();
        playerTeam[0].CreateFromBase(mp.FindByID(1));
        playerTeam[0].level = 50;
        playerTeam[0].GenerateMoveset();
    }

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        if (player != null)
        {
            PlayerLocation = player.transform.position;
        }
        if (camera != null)
        {
            CameraLocation = camera.transform.position;
        }
    }

}
