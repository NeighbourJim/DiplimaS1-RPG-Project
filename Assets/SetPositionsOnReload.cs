using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionsOnReload : MonoBehaviour {

    GameObject gameDataController;
    PlayerDataHolder playerData;
    GameObject player;
    GameObject camera;

	// Use this for initialization
	void Start () {
        gameDataController = GameObject.Find("GameDataController");
        playerData = gameDataController.GetComponent<PlayerDataHolder>();
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        SetPlayerLocation();
        SetCameraLocation();
	}

    void SetPlayerLocation()
    {
        player.transform.position = PlayerDataHolder.PlayerLocation;
    }
    void SetCameraLocation()
    {
        player.transform.position = PlayerDataHolder.PlayerLocation;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
