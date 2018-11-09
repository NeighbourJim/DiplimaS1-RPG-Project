using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

    public float blinkSpeed = 0.7f;
    Text text;

    // Use this for initialization`
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(Blinky());
    }


    IEnumerator Blinky()
    {
        while (true)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
