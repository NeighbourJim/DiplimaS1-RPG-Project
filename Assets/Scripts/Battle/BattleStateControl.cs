using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateControl : MonoBehaviour {
    public TurnState currentState;
    private MonData firstToGo;
    private MonData secondToGo;

    BattleControl battleControl;
    DataList moveData;
    public GameObject battleUI;
    BattleUIControl uiScript;

    public MoveData playerSelectedMove;
    public MoveData enemySelectedMove;

    // Use this for initialization
    void Start () {
        battleControl = GetComponent<BattleControl>();
        moveData = GetComponent<DataList>();
        uiScript = battleUI.GetComponent<BattleUIControl>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (currentState)
        {
            case (TurnState.Intro):
                print("A wild " + battleControl.enemyMon.monName + " appeared!");
                print("Go, " + battleControl.playerMon.monName + "!");
                uiScript.ShowActionSelect();
                AdvanceState(TurnState.SelectingAction);
                break;

            case (TurnState.SelectingAction): 
                break;

            case (TurnState.EnemySelectAction):
                battleControl.SelectEnemyMove();
                AdvanceState(TurnState.DetermineOrder);
                break;

            case (TurnState.DetermineOrder):
                DetermineTurnOrder();
                AdvanceState(TurnState.FirstAction);
                break;

            case (TurnState.FirstAction):
                print(string.Format("{0} attacks with {1}!",firstToGo.monName, firstToGo.selectedMove.moveName));
                AdvanceState(TurnState.SecondAction);
                break;

            case (TurnState.SecondAction):
                print(string.Format("{0} attacks with {1}!", secondToGo.monName, secondToGo.selectedMove.moveName));
                AdvanceState(TurnState.SelectingAction);
                break;
            case (TurnState.FirstFaints):

                break;
            case (TurnState.SecondFaints):

                break;
            case (TurnState.BothFaint):

                break;
            case (TurnState.PlayerLose):

                break;
            case (TurnState.PlayerWin):

                break;
        }
	}

    public void AdvanceState(TurnState state)
    {
        currentState = state;
    }

    void DetermineTurnOrder()
    {
        if (battleControl.playerMon.curSpeed > battleControl.enemyMon.curSpeed)
        {
            firstToGo = battleControl.playerMon;
            secondToGo = battleControl.enemyMon;
        }
        else if (battleControl.playerMon.curSpeed < battleControl.enemyMon.curSpeed)
        {
            firstToGo = battleControl.enemyMon;
            secondToGo = battleControl.playerMon;
        }
        else
        {
            if (Random.value < 0.5f)
            {
                firstToGo = battleControl.playerMon;
                secondToGo = battleControl.enemyMon;
            }
            else
            {
                firstToGo = battleControl.enemyMon;
                secondToGo = battleControl.playerMon;
            }                
        }
    }
}
