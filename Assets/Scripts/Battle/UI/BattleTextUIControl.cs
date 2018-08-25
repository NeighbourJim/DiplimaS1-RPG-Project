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

    public void DisplayText(string txt)
    {
        StopCoroutine(AnimText(txt));
        StartCoroutine(AnimText(txt));
    }

    public void ContinueClicked()
    {
        completed = true;
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
