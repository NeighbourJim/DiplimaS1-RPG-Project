using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataHolder : MonoBehaviour {

    public static string PlayerPrevMap { get; set; }
    public static string PlayerCurrMap { get; set; }
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
    GameObject mainCam;

    private void Start()
    {
        mp = GetComponent<Monpedia>();

        if (playerTeam[0] == null)
        {
            playerTeam[0] = ScriptableObject.CreateInstance<MonData>();
            SetData(1,1);
        }
    }

    public void SetData(int id, int level)
    {
        playerTeam[0].CreateFromBase(mp.FindByID(id));
        playerTeam[0].level = level;
        playerTeam[0].GenerateWildStats(playerTeam[0].level);
        playerTeam[0].GenerateMoveset();
    }

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        if (player != null)
        {
            PlayerLocation = player.transform.position;
        }
        if (mainCam != null)
        {
            CameraLocation = mainCam.transform.position;
        }
    }

    private void OnEnable()
    {
        PlayerPrevMap = PlayerCurrMap;
        PlayerCurrMap = SceneManager.GetActiveScene().name;
    }
}
