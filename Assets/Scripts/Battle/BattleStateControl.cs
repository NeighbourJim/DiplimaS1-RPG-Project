using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateControl : MonoBehaviour {


    public enum TurnState
    {
        Intro,
        SelectingAction,
        DetermineOrder,
        FirstAction,
        SecondAction,
        FirstFaints,
        SecondFaints,
        BothFaint,
        BattleOver
    }

    public TurnState currentState;
    private Copymon firstToGo;
    private Copymon secondToGo;
    SpawnMonsters spawnScript;
    DataList moveData;

	// Use this for initialization
	void Start () {
        spawnScript = GetComponent<SpawnMonsters>();
        moveData = GetComponent<DataList>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (currentState)
        {
            case (TurnState.Intro):
                print("A wild " + spawnScript.enemyMon.monName + " appeared!");
                print("Go, " + spawnScript.playerMon.monName + "!");
                AdvanceState(TurnState.SelectingAction);
                break;

            case (TurnState.SelectingAction):
                if (Input.GetButtonDown("Jump"))
                {
                    AdvanceState(TurnState.DetermineOrder);
                }
                break;

            case (TurnState.DetermineOrder):
                DetermineTurnOrder();
                AdvanceState(TurnState.FirstAction);
                break;

            case (TurnState.FirstAction):
                print(string.Format("{0} attacks with {1}!",firstToGo.monName, moveData.moveDex[firstToGo.moveId1].moveName));
                AdvanceState(TurnState.SecondAction);
                break;

            case (TurnState.SecondAction):
                print(string.Format("{0} attacks with {1}!", secondToGo.monName, moveData.moveDex[secondToGo.moveId1].moveName));
                AdvanceState(TurnState.SelectingAction);
                break;
            case (TurnState.FirstFaints):

                break;
            case (TurnState.SecondFaints):

                break;
            case (TurnState.BothFaint):

                break;
            case (TurnState.BattleOver):

                break;
            
        }
	}

    public void AdvanceState(TurnState state)
    {
        currentState = state;
    }

    void DetermineTurnOrder()
    {
        if (spawnScript.playerMon.curSpeed > spawnScript.enemyMon.curSpeed)
        {
            firstToGo = spawnScript.playerMon;
            secondToGo = spawnScript.enemyMon;
        }
        else if (spawnScript.playerMon.curSpeed < spawnScript.enemyMon.curSpeed)
        {
            firstToGo = spawnScript.enemyMon;
            secondToGo = spawnScript.playerMon;
        }
        else
        {
            if (Random.value < 0.5f)
            {
                firstToGo = spawnScript.playerMon;
                secondToGo = spawnScript.enemyMon;
            }
            else
            {
                firstToGo = spawnScript.enemyMon;
                secondToGo = spawnScript.playerMon;
            }                
        }
    }
}
