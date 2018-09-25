using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateIV : MonoBehaviour {

    InputField t;

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<InputField>();
	}


    public void FixIV()
    {
        int num = 0;

        if (int.TryParse(t.text, out num))
        {
            if (num > 31)
                num = 31;
            else if (num < 1)
                num = 0;
        }
        else
        {
            num = 0;
        }

        t.text = num.ToString();
    }
}
