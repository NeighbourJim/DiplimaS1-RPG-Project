using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleUIEventHandler : MonoBehaviour
{
    [SerializeField]
    public UnityEvent messagesStarted;
    [SerializeField]
    public UnityEvent messagesEnded;
    [SerializeField]
    public UnityEvent awaitingConfirm;
    [SerializeField]
    public UnityEvent continueMessages;
}
