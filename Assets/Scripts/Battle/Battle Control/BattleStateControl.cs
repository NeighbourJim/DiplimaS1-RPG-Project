using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateControl : MonoBehaviour {
    public TurnState currentState;
    public TurnState previousState;
    private MonBattleData firstToGo;
    private MonBattleData secondToGo;

    BattleControl battleControl;
    public GameObject battleUI;
    BattleUIControl uiButtonControl;
    BattleTextUIControl uiTextControl;
    BattleHPControl uiHPControl;
    BattleCameraControl cameraControl;
    BattleDialogue battleDialogue;
    BattleUIEventHandler uIEventHandler;

    public MoveData playerSelectedMove;
    public MoveData enemySelectedMove;

    public GameObject dataCont;
    public PlayerDataHolder playerData;

    // Use this for initialization
    void Start () {
        battleControl = GetComponent<BattleControl>();
        cameraControl = FindObjectOfType<BattleCameraControl>();
        uiButtonControl = battleUI.GetComponent<BattleUIControl>();
        uiTextControl = battleUI.GetComponent<BattleTextUIControl>();
        uiHPControl = battleUI.GetComponent<BattleHPControl>();
        battleDialogue = battleUI.GetComponent<BattleDialogue>();
        uIEventHandler = FindObjectOfType<BattleUIEventHandler>();

        dataCont = GameObject.Find("GameDataController");
        playerData = dataCont.GetComponent<PlayerDataHolder>();
    }
	
	// Update is called once per frame
    // State Machine Control
	void Update ()
    {
        switch (currentState)
        {
            case (TurnState.BattleStarting):
                ResolveStartingState();
                break;
            case (TurnState.Intro):
                ResolveIntroState();
                break;

            case (TurnState.SelectingAction):
                if (uiTextControl.messagesFinished)
                    ResolveSelectingState();
                break;

            case (TurnState.EnemySelectAction):
                if (uiTextControl.messagesFinished)
                    ResolveEnemySelectingState();
                break;

            case (TurnState.DetermineOrder):
                if (uiTextControl.messagesFinished)
                    ResolveOrderState();
                break;

            case (TurnState.FirstAction):
                if (uiTextControl.messagesFinished)
                    ResolveFirstActionState();
                break;

            case (TurnState.SecondAction):
                if (uiTextControl.messagesFinished)
                    ResolveSecondActionState();
                break;

            case (TurnState.TurnEnding):
                if (uiTextControl.messagesFinished)
                    ResolveTurnEndingState();
                break;

            case (TurnState.FaintCheck):
                if (uiTextControl.messagesFinished)
                    ResolveFaintCheckState();
                break;

            case (TurnState.PlayerFaints):
                if (uiTextControl.messagesFinished)
                    ResolvePlayerFaintState();
                break;
            case (TurnState.EnemyFaints):
                if (uiTextControl.messagesFinished)
                    ResolveEnemyFaintState();
                break;
            case (TurnState.BothFaint):
                if (uiTextControl.messagesFinished)
                    ResolveBothFaintState();
                break;
            case (TurnState.PlayerLose):
                if (uiTextControl.messagesFinished)
                    ResolvePlayerLoseState();
                break;
            case (TurnState.PlayerWin):
                if (uiTextControl.messagesFinished)
                    ResolvePlayerWinState();
                break;
        }
	}

    #region State Resolution
    void ResolveStartingState()
    {        
        Monpedia mp = dataCont.GetComponent<Monpedia>();
        battleControl.InitiateWildBattle(PlayerDataHolder.playerTeam[0], EnemyDataHolder.enemyMonster);
        uiHPControl.SetMonsters(battleControl.playerMon, battleControl.enemyMon);
        AdvanceState(TurnState.Intro);
    }
    void ResolveIntroState()
    {
        battleDialogue.AddToMessages(string.Format("A wild {0} appeared!", battleControl.enemyMon.monName));
        battleDialogue.AddToMessages(string.Format("Go, {0}!", battleControl.playerMon.monName));
        uIEventHandler.continueMessages.Invoke();
        AdvanceState(TurnState.SelectingAction);
    }

    void ResolveSelectingState()
    {
        uiButtonControl.ShowActionSelect();
    }

    void ResolveEnemySelectingState()
    {
        battleControl.SelectEnemyMove();
        AdvanceState(TurnState.DetermineOrder);
    }

    void ResolveOrderState()
    {
        DetermineTurnOrder();
        cameraControl.ResetCamera();
        AdvanceState(TurnState.FirstAction);
    }

    void ResolveFirstActionState()
    {
        uIEventHandler.continueMessages.Invoke();
        if (battleControl.ResolveMidTurnStatusEffect(firstToGo))
        {
            battleDialogue.AddToMessages(string.Format("{0} attacks with {1}!", firstToGo.monName, firstToGo.selectedMove.moveName));
            battleControl.ResolveAttack(firstToGo.selectedMove, firstToGo, secondToGo);
            uIEventHandler.continueMessages.Invoke();
        }
        AdvanceState(TurnState.FaintCheck);
    }

    void ResolveSecondActionState()
    {
        if (battleControl.ResolveMidTurnStatusEffect(secondToGo))
        {
            battleDialogue.AddToMessages(string.Format("{0} attacks with {1}!", secondToGo.monName, secondToGo.selectedMove.moveName));
            battleControl.ResolveAttack(secondToGo.selectedMove, secondToGo, firstToGo);
            uIEventHandler.continueMessages.Invoke();
        }
        AdvanceState(TurnState.TurnEnding);
    }

    void ResolveTurnEndingState()
    {
        if (firstToGo.hasStatus != StatusEffect.none)
            battleControl.ResolveEndTurnStatusEffect(firstToGo);
        if (secondToGo.hasStatus != StatusEffect.none)
            battleControl.ResolveEndTurnStatusEffect(secondToGo);
        AdvanceState(TurnState.SelectingAction);
    }

    void ResolveFaintCheckState()
    {
        if (CheckFainted(battleControl.playerMon))
        {
            AdvanceState(TurnState.PlayerFaints);
        }
        else if (CheckFainted(battleControl.enemyMon))
        {
            AdvanceState(TurnState.EnemyFaints);
        }
        else
        {
            if (previousState == TurnState.FirstAction)
            {
                AdvanceState(TurnState.SecondAction);
            }
            if (previousState == TurnState.SecondAction)
            {
                AdvanceState(TurnState.TurnEnding);
            }
        }
    }

    void ResolvePlayerFaintState()
    {
        battleDialogue.AddToMessages(battleControl.playerMon.monName + " fainted!");
        uIEventHandler.continueMessages.Invoke();
        AdvanceState(TurnState.PlayerLose);
    }

    void ResolveEnemyFaintState()
    {
        battleDialogue.AddToMessages("The foe " + battleControl.enemyMon.monName + " fainted!");
        uIEventHandler.continueMessages.Invoke();
        AdvanceState(TurnState.PlayerWin);
    }

    void ResolveBothFaintState()
    {

    }

    void ResolvePlayerWinState()
    {
        battleDialogue.AddToMessages("You win.");
        AdvanceState(TurnState.BothFaint); // TODO: CHANGE THIS!!!!!!!
    }

    void ResolvePlayerLoseState()
    {
        battleDialogue.AddToMessages("You lose.");
        AdvanceState(TurnState.BothFaint); // TODO: CHANGE THIS!!!!!!!
    }
    #endregion

    

    public void AdvanceState(TurnState state)
    {
        previousState = currentState;
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

    bool CheckFainted(MonBattleData mon)
    {
        return (mon.curHP == 0);
    }
}
