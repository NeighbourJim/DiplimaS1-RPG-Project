using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {


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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (currentState)
        {
            case (TurnState.Intro):

                break;

            case (TurnState.SelectingAction):

                break;

            case (TurnState.DetermineOrder):

                break;

            case (TurnState.FirstAction):

                break;

            case (TurnState.SecondAction):

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
}
