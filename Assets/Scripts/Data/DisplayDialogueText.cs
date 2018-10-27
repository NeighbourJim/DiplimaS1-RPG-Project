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
    DialogData dialog;

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
                    dialog = playerDialogChecker.GetDialogObject();
                    if (dialog.Trainer == true && dialog.gameObject.GetComponent<Trainer>().defeated == true)
                    {
                        StartDialogue(dialog.Name, dialog.gameObject.GetComponent<Trainer>().DefeatQuote);
                    }
                    else
                    {
                        StartDialogue(dialog.Name, dialog.Dialog);
                    }
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
            CheckDialogEffect();
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

    void CheckDialogEffect()
    {
        if (dialog.Healer)
        {
            FindObjectOfType<PlayerDataHolder>().FullyHealTeam();
        }
        else if (dialog.Trainer && dialog.gameObject.GetComponent<Trainer>().defeated == false)
        {
            FindObjectOfType<RandomEncounterController>().StartTrainerBattle(dialog.gameObject.GetComponent<Trainer>());
        }
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
