using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAntiAliasing : MonoBehaviour {

	public void SetAntiAliasing(bool value)
    {
        if(value)
        {
            QualitySettings.antiAliasing = 2;
        }
        else
        {
            QualitySettings.antiAliasing = 0;
        }
    }

    private void Update()
    {
        Debug.Log(QualitySettings.antiAliasing);
    }
}
