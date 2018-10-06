using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUIControl : MonoBehaviour {

    public GameObject battleController;
    BattleControl battleControlScript;
    BattleStateControl battleStateControlScript;
    public Button m1;
    public Button m2;
    public Button m3;
    public Button m4;
    public Button backButton;
    public GameObject baseSelectPanel;
    public GameObject moveSelectPanel;
    public GameObject switchPanel;

    public GameObject newMovePanel;
    public Button newMoveButton;
    public Button newMoveM1;
    public Button newMoveM2;
    public Button newMoveM3;
    public Button newMoveM4;
    public Button newMoveNone;

    int[] tmpRGB;
    BattleTextUIControl textUIControl;
    BattleDialogue dialogue;

    MoveData moveLearning;

    // Use this for initialization
    void Awake () {
        
        battleControlScript = battleController.GetComponent<BattleControl>();
        dialogue = GetComponent<BattleDialogue>();
        battleStateControlScript = battleController.GetComponent<BattleStateControl>();
        textUIControl = GetComponent<BattleTextUIControl>();
        newMovePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ChangeButtonColour(Button button, TypeData type)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = type.buttonColour;
        colors.highlightedColor = new Color(type.buttonColour.r * 1.5f, type.buttonColour.g * 1.5f, type.buttonColour.b * 1.5f);
        button.colors = colors;
    }

    public void ChangeButtonText(Button button, string input)
    {
        button.GetComponentInChildren<Text>().text = input;
    }

    public void ShowActionSelect()
    {
        baseSelectPanel.SetActive(true);
        moveSelectPanel.SetActive(false);
        backButton.gameObject.SetActive(false);
        GameObject tooltip = GameObject.FindGameObjectWithTag("UITooltip");
        if (tooltip != null)
        {
            Debug.Log("Destroying old tooltip");
            Destroy(tooltip);
        }
        battleStateControlScript.AdvanceState(TurnState.SelectingAction);
    }

    public void ShowMoveSelect()
    {
        baseSelectPanel.SetActive(false);
        moveSelectPanel.SetActive(true);
        backButton.gameObject.SetActive(true);
        battleStateControlScript.AdvanceState(TurnState.SelectingMove);
    }

    public void ShowMoveLearn(MoveData moveToLearn)
    {
        moveLearning = moveToLearn;
        ToggleInputsOff();
        newMovePanel.SetActive(true);
        ChangeButtonColour(newMoveButton, moveToLearn.moveType);
        ChangeButtonText(newMoveButton, moveToLearn.moveName);

        ChangeButtonColour(newMoveM1, battleControlScript.playerMon.learnedMoves[0].moveType);
        ChangeButtonText(newMoveM1, battleControlScript.playerMon.learnedMoves[0].moveName);

        ChangeButtonColour(newMoveM2, battleControlScript.playerMon.learnedMoves[1].moveType);
        ChangeButtonText(newMoveM2, battleControlScript.playerMon.learnedMoves[1].moveName);

        ChangeButtonColour(newMoveM3, battleControlScript.playerMon.learnedMoves[2].moveType);
        ChangeButtonText(newMoveM3, battleControlScript.playerMon.learnedMoves[2].moveName);

        ChangeButtonColour(newMoveM4, battleControlScript.playerMon.learnedMoves[3].moveType);
        ChangeButtonText(newMoveM4, battleControlScript.playerMon.learnedMoves[3].moveName);
    }

    public void HideMoveLearn()
    {
        newMovePanel.SetActive(false);
        moveLearning = null;
    }

    public void SelectMoveReplacement(int index)
    {
        if (index != -1)
        {
            dialogue.AddToMessages(string.Format("3.. 2.. 1.. Poof! {0} forgot {1}!", battleControlScript.playerMon.monName, battleControlScript.playerMon.learnedMoves[index].moveName));
            dialogue.AddToMessages(string.Format("{0} learned {1}!", battleControlScript.playerMon.monName, moveLearning.moveName));
            battleControlScript.playerMon.ReplaceMove(index, moveLearning);
        }
        else
        {
            dialogue.AddToMessages(string.Format("{0} stopped learning {1}.", battleControlScript.playerMon.monName, moveLearning.moveName));
        }
        HideMoveLearn();
        battleStateControlScript.uIEventHandler.continueMessages.Invoke();
        battleStateControlScript.AdvanceState(TurnState.PlayerWin);
    }

    public void ToggleInputsOff()
    {
        baseSelectPanel.SetActive(false);
        moveSelectPanel.SetActive(false);
        backButton.gameObject.SetActive(false);
        GameObject tooltip = GameObject.FindGameObjectWithTag("UITooltip");
        if (tooltip != null)
        {
            Debug.Log("Destroying old tooltip");
            Destroy(tooltip);
        }
    }

    public string GetClickedButton()
    {
        return(EventSystem.current.currentSelectedGameObject.name);
    }

    public void SelectMove()
    {
        battleControlScript.SetSelectedMove(GetClickedButton());
    }

}
