using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUIControl : MonoBehaviour {

    public GameObject battleController;
    BattleControl battleControlScript;
    BattleStateControl battleStateControlScript;
    Copymon mon;
    public Button m1;
    public Button m2;
    public Button m3;
    public Button m4;
    public Button backButton;
    public GameObject baseSelectPanel;
    public GameObject moveSelectPanel;
    public GameObject switchPanel;
    int[] tmpRGB;


    // Use this for initialization
    void Awake () {
        
        battleControlScript = battleController.GetComponent<BattleControl>();
        battleStateControlScript = battleController.GetComponent<BattleStateControl>();
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
        battleStateControlScript.AdvanceState(TurnState.SelectingAction);
        baseSelectPanel.SetActive(true);
        moveSelectPanel.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    public void ShowMoveSelect()
    {
        battleStateControlScript.AdvanceState(TurnState.SelectingMove);
        baseSelectPanel.SetActive(false);
        moveSelectPanel.SetActive(true);
        backButton.gameObject.SetActive(true);
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
