using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleDialogue : MonoBehaviour
{
    Queue<string> battleMessages = new Queue<string>();
    public BattleUIEventHandler eventHandler;

    private void Start()
    {
        eventHandler = GetComponent<BattleUIEventHandler>();
    }

    public string GetNextMessage()
    {
        if(battleMessages.Count != 0)
        {
            return battleMessages.Dequeue();
        }
        else
        {
            return null;
        }
    }

    public void AddToMessages(string toAdd)
    {
        eventHandler.messagesStarted.Invoke();
        battleMessages.Enqueue(toAdd);        
    }
}
