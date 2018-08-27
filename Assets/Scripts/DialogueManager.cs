using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public Queue<string> stringQueue;

    private void Start()
    {
        stringQueue = new Queue<string>();
    }
}
