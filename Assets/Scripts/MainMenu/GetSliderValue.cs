using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSliderValue : MonoBehaviour {

    public GameObject Slider;

    private Slider slider;
    private Text text;
    public string append;

    private void Start()
    {
        slider = Slider.GetComponent<Slider>();
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        text.text = slider.value.ToString() + append;
	}
}
