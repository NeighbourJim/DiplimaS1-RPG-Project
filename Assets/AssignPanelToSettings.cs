using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignPanelToSettings : MonoBehaviour {

    public DataSettings settings;

    private void Start()
    {
        settings.BrightnessImage = GetComponent<Image>();
    }
}
