using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTextUIControl : MonoBehaviour {

    public Button continueButton;
    public Text textArea;
    public float characterDelay = 0.05f;
    public BattleUIEventHandler eventHandler;
    BattleDialogue battleDialogue;

    string toDisplay;

    public bool messagesStarted = false;
    public bool messagesFinished = true;

    Coroutine co;

    private void Start()
    {
        eventHandler = GetComponent<BattleUIEventHandler>();
        battleDialogue = GetComponent<BattleDialogue>();
        continueButton.interactable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if(battleDialogue.PeekNextMessage() != null)
                eventHandler.continueMessages.Invoke();
        }
    }

    public void DisplayNextMessage()
    {
        string message = battleDialogue.GetNextMessage();
        if(message != null)
        {
            DisplayText(message);
        }
        else
        {
            eventHandler.messagesEnded.Invoke();
        }
    }

    public void DisplayText(string txt)
    {
        co = StartCoroutine(AnimText(txt));
    }

    IEnumerator AnimText(string txt)
    {
        int i = 0;
        textArea.text = "";
        while(i < txt.Length)
        {
            textArea.text += txt[i++];
            yield return new WaitForSeconds(characterDelay);
        }
    }


    public void ContinueClick()
    {
        if (battleDialogue.PeekNextMessage() != null)
        {
            if (co != null) { StopCoroutine(co); }
        }
        DisplayNextMessage();
    }

    public void SetMessagesStarted()
    {
        messagesStarted = true;
        messagesFinished = false;
    }

    public void SetMessagesFinished()
    {
        messagesFinished = true;
        messagesStarted = false;
    }
}
