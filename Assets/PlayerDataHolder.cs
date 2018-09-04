using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataHolder : MonoBehaviour {

    public static Vector3 PlayerLocation { get; set; }

    public static int PlayerMonsterID { get; set; }

    public static int PlayerMonsterLevel { get; set; }
    public static int PlayerMonsterCurXP { get; set; }

    public static int PlayerMonsterCurHPPerc { get; set; }
    public static int PlayerMonsterIVHP { get; set; }
    public static int PlayerMonsterIVAtk { get; set; }
    public static int PlayerMonsterIVDef { get; set; }
    public static int PlayerMonsterIVSpAtk { get; set; }
    public static int PlayerMonsterIVSpDef { get; set; }
    public static int PlayerMonsterIVSpeed { get; set; }
    public static StatusEffect PlayerMonsterStatus { get; set; }

    public void SetData()
    {
        PlayerMonsterID = 10;

        PlayerMonsterLevel = 5;
        PlayerMonsterCurXP = 0;
        PlayerMonsterCurHPPerc = 100;

        PlayerMonsterIVAtk = 31;
        PlayerMonsterIVDef = 31;
        PlayerMonsterIVSpAtk = 31;
        PlayerMonsterIVSpDef = 31;
        PlayerMonsterIVSpeed = 31;

        PlayerMonsterStatus = StatusEffect.none;

        SceneManager.LoadScene("BaseBattle");
    }

}
