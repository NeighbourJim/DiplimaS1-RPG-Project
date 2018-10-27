using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleStateControl : MonoBehaviour {
    public TurnState currentState;
    public TurnState previousState;
    private MonBattleData firstToGo;
    private MonBattleData secondToGo;

    BattleControl battleControl;
    public GameObject battleUI;
    BattleUIControl uiController;
    BattleTextUIControl uiTextControl;
    BattleHPControl uiHPControl;
    BattleCameraControl cameraControl;
    BattleDialogue battleDialogue;
    public BattleUIEventHandler uIEventHandler;

    public MoveData playerSelectedMove;
    public MoveData enemySelectedMove;

    public GameObject dataCont;
    public PlayerDataHolder playerData;

    SoundManager soundManager;

    // Use this for initialization
    void Start () {
        battleControl = GetComponent<BattleControl>();
        cameraControl = FindObjectOfType<BattleCameraControl>();
        uiController = battleUI.GetComponent<BattleUIControl>();
        uiTextControl = battleUI.GetComponent<BattleTextUIControl>();
        uiHPControl = battleUI.GetComponent<BattleHPControl>();
        battleDialogue = battleUI.GetComponent<BattleDialogue>();
        uIEventHandler = FindObjectOfType<BattleUIEventHandler>();
        soundManager = FindObjectOfType<SoundManager>();

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
            case (TurnState.EnemyIntro):
                //if (uiTextControl.messagesFinished)
                    ResolveEnemyIntroState();
                break;
            case (TurnState.PlayerIntro):
                if (uiTextControl.messagesFinished)
                    ResolvePlayerIntroState();
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

            case (TurnState.FleeAttempt):
                if (uiTextControl.messagesFinished)
                    ResolveFleeState();
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
            case (TurnState.BattleEnding):
                if (uiTextControl.messagesFinished && !uiController.newMovePanel.activeInHierarchy)
                    ResolveBattleEndState();
                break;
            case (TurnState.MonsterLearningNewMove):
                if (uiTextControl.messagesFinished)
                    ResolveMonsterLearnMoveState();
                break;
        }
	}

    #region State Resolution
    void ResolveStartingState()
    {        
        Monpedia mp = dataCont.GetComponent<Monpedia>();
        if (EnemyDataHolder.BattleType == BattleType.Trainer)
        {
            battleControl.InitiateTrainerBattle();
        }
        else
        {
            battleControl.InitiateWildBattle();
        }
        uiHPControl.SetMonsters(battleControl.playerMon, battleControl.enemyMon);
        AdvanceState(TurnState.EnemyIntro);
    }
    void ResolveEnemyIntroState()
    {
        if (battleControl.battleType == BattleType.Trainer)
        {
            battleDialogue.AddToMessages(string.Format("{0} {1} wants to fight!", EnemyDataHolder.EnemyTrainer.trainerType.ToString().Replace('_',' '), EnemyDataHolder.EnemyTrainer.TrainerName));
        }
        else
        {
            battleDialogue.AddToMessages(string.Format("A wild {0} appeared!", battleControl.enemyMon.monName));
        }
        battleControl.SetEnemyVisibility(true);
        soundManager.PlayCry(battleControl.enemyMon.monName);
        uIEventHandler.continueMessages.Invoke();
        AdvanceState(TurnState.PlayerIntro);
    }

    void ResolvePlayerIntroState()
    {
        battleDialogue.AddToMessages(string.Format("Go, {0}!", battleControl.playerMon.nickname));
        battleControl.SetPlayerVisibility(true);
        soundManager.PlayCry(battleControl.playerMon.monName);
        uIEventHandler.continueMessages.Invoke();
        AdvanceState(TurnState.SelectingAction);
    }

    void ResolveSelectingState()
    {
        uiController.ShowActionSelect();
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
        AdvanceState(TurnState.FaintCheck);
    }

    void ResolveTurnEndingState()
    {
        if (firstToGo.hasStatus != StatusEffect.none)
            battleControl.ResolveEndTurnStatusEffect(firstToGo);
        if (secondToGo.hasStatus != StatusEffect.none)
            battleControl.ResolveEndTurnStatusEffect(secondToGo);
        AdvanceState(TurnState.FaintCheck);
    }

    void ResolveFaintCheckState()
    {
        
        if (CheckFainted(battleControl.playerMon))
        {
            Debug.Log("Player faint");
            AdvanceState(TurnState.PlayerFaints);
        }
        else if (CheckFainted(battleControl.enemyMon))
        {
            Debug.Log("Enemy faint");
            AdvanceState(TurnState.EnemyFaints);
        }
        else
        {
            if (previousState == TurnState.FirstAction || previousState == TurnState.FleeAttempt)
            {
                AdvanceState(TurnState.SecondAction);
            }
            if (previousState == TurnState.SecondAction)
            {
                AdvanceState(TurnState.TurnEnding);
            }
            if (previousState == TurnState.TurnEnding)
            {
                AdvanceState(TurnState.SelectingAction);
            }
        }
    }

    void ResolvePlayerFaintState()
    {
        battleDialogue.AddToMessages(battleControl.playerMon.monName + " fainted!");
        soundManager.PlayFaintCry(battleControl.playerMon.monName);
        uIEventHandler.continueMessages.Invoke();
        AdvanceState(TurnState.PlayerLose);
    }

    void ResolveEnemyFaintState()
    {
        battleDialogue.AddToMessages("The foe " + battleControl.enemyMon.monName + " fainted!");
        soundManager.PlayFaintCry(battleControl.enemyMon.monName);
        uIEventHandler.continueMessages.Invoke();
        if(!battleControl.DistributeXP())
            AdvanceState(TurnState.PlayerWin);
    }

    void ResolveBothFaintState()
    {
        battleDialogue.AddToMessages("You win..?");
        AdvanceState(TurnState.BattleEnding);
    }

    void ResolvePlayerWinState()
    {
        battleDialogue.AddToMessages("You win.");
        AdvanceState(TurnState.BattleEnding); 
    }

    void ResolvePlayerLoseState()
    {
        battleDialogue.AddToMessages("You lose.");
        AdvanceState(TurnState.BattleEnding); 
    }

    void ResolveFleeState()
    {
        if (battleControl.battleType == BattleType.WildFleeable)
        {
            battleDialogue.AddToMessages("You tried to flee...");
            if (battleControl.TryToFlee())
            {
                battleDialogue.AddToMessages("You fled from battle!");
                AdvanceState(TurnState.BattleEnding);
            }
            else
            {
                firstToGo = battleControl.playerMon;
                secondToGo = battleControl.enemyMon;
                battleDialogue.AddToMessages("Couldn't get away!");
                battleControl.SelectEnemyMove();
                AdvanceState(TurnState.FaintCheck);
            }
        }
        else
        {
            if (battleControl.battleType == BattleType.Trainer)
            {
                battleDialogue.AddToMessages("Can't flee from a trainer battle!");
            }
            else
            {
                battleDialogue.AddToMessages("Can't flee from this battle!");
            }
            firstToGo = battleControl.playerMon;
            secondToGo = battleControl.enemyMon;
            battleControl.SelectEnemyMove();
            AdvanceState(TurnState.FaintCheck);
        }
    }

    void ResolveBattleEndState()
    {
        if (previousState == TurnState.PlayerLose)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            PlayerDataHolder.playerTeam[0].RetainBattleStatus(battleControl.playerMon);
            SceneManager.LoadScene(PlayerDataHolder.PlayerPrevMap);
        }
    }

    void ResolveMonsterLearnMoveState()
    {
        uiController.ShowMoveLearn(battleControl.playerMon.CheckLevelMove());
    }

    #endregion

    

    public void AdvanceState(TurnState state)
    {
        Debug.Log("State Changing To: " + state.ToString());
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

        if (battleControl.playerMon.selectedMove.priority > battleControl.enemyMon.selectedMove.priority)
        {
            firstToGo = battleControl.playerMon;
            secondToGo = battleControl.enemyMon;
        }
        else if (battleControl.playerMon.selectedMove.priority < battleControl.enemyMon.selectedMove.priority)
        {
            firstToGo = battleControl.enemyMon;
            secondToGo = battleControl.playerMon;
        }
    }

    bool CheckFainted(MonBattleData mon)
    {
        return (mon.curHP == 0);
    }
}
