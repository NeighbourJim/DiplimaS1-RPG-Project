using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDialogueText : MonoBehaviour {

    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;

    Queue<string> messages = new Queue<string>();
    public float characterDelay = 0.05f;
    public bool dialogActive = false;

    Coroutine co;

    DialogZoneChecker playerDialogChecker;

    private void Start()
    {
        playerDialogChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<DialogZoneChecker>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!dialogActive)
            {
                if (playerDialogChecker.inDialogZone)
                {
                    DialogData dialog = playerDialogChecker.GetDialogObject();
                    StartDialogue(dialog.Name, dialog.Dialog);
                }
            }
            else if (dialogActive)
            {
                continueMessages();
            }
        }
    }

    void continueMessages()
    {
        if (messages.Count != 0)
        {
            DisplayText(messages.Dequeue());
        }
        else
        {
            HideBox();
        }
    }

    public void ShowBox()
    {
        dialogueBox.SetActive(true);
        dialogActive = true;
    }

    public void HideBox()
    {
        dialogueBox.SetActive(false);
        dialogActive = false;
    }

    public void StartDialogue(string name, List<string> dialogue)
    {
        SetName(name);

        foreach(var message in dialogue)
        {
            Debug.Log(message);
            messages.Enqueue(message);
        }

        ShowBox();
        DisplayText(messages.Dequeue());
    }

    void SetName(string input)
    {
        nameText.text = input;
    }

    void DisplayText(string input)
    {
        if (co != null) { StopCoroutine(co); }
        co = StartCoroutine(AnimText(input));
    }

    IEnumerator AnimText(string input)
    {
        int i = 0;
        dialogueText.text = "";
        while (i < input.Length)
        {
            dialogueText.text += input[i++];
            yield return new WaitForSeconds(characterDelay);
        }
    }
}
