using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRes : MonoBehaviour {

	public void ChangeResolution()
    {
        int value = gameObject.GetComponent<Dropdown>().value;

        if(value == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if(value == 1)
        {
            Screen.SetResolution(1280, 720, true);
        }
    }

    //public void ChangeRes()
}
