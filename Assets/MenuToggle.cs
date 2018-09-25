using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToggle : MonoBehaviour {

    public GameObject toToggle;

	public void Toggle()
    {
        toToggle.SetActive(!toToggle.activeSelf);
        if (toToggle.activeSelf)
        {
            gameObject.GetComponentInChildren<Text>().text = "Close Menu";
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = "Open Menu";
        }
    }
}
