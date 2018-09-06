using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataHolder : MonoBehaviour {

    public static Vector3 PlayerLocation { get; set; }   

    public static MonData[] playerTeam = new MonData[5];

    Monpedia mp;

    private void Start()
    {
        mp = GetComponent<Monpedia>();
    }

    public void SetData()
    {
        playerTeam[0] = ScriptableObject.CreateInstance<MonData>();
        playerTeam[0].CreateFromBase(mp.FindByID(1));
        playerTeam[0].level = 11;
        playerTeam[0].GenerateMoveset();

        SceneManager.LoadScene("BaseBattle");
    }



}
