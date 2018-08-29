using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTextUIControl : MonoBehaviour {

    public Button continueButton;
    public Text textArea;
    public float characterDelay = 0.05f;

    string toDisplay;
    bool completed = false;

    Coroutine co;

    public void DisplayText(string txt)
    {
        co = StartCoroutine(AnimText(txt));
    }

    IEnumerator AnimText(string txt)
    {
        completed = false;
        int i = 0;
        textArea.text = "";
        while(i < txt.Length)
        {
            textArea.text += txt[i++];
            yield return new WaitForSeconds(characterDelay);
        }
    }
}
