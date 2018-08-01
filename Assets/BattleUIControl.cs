using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIControl : MonoBehaviour {

    public GameObject battleController;
    Dictionary<MonType, int[]> typeColours = new Dictionary<MonType, int[]>();
    BattleControl spawnScript;
    Copymon mon;
    public Button m1;
    public Button m2;
    public Button m3;
    public Button m4;
    public Button backButton;
    public GameObject baseSelectPanel;
    public GameObject moveSelectPanel;
    public GameObject switchPanel;
    DataList data;
    int[] tmpRGB;


    // Use this for initialization
    void Awake () {
        
        spawnScript = battleController.GetComponent<BattleControl>();
        data = battleController.GetComponent<DataList>();
        PopulateTypeColours();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void PopulateTypeColours()
    {
        typeColours.Add(MonType.normal, new int[] { 245, 232, 206 });
        typeColours.Add(MonType.fire, new int[] { 255, 163, 64 });
        typeColours.Add(MonType.grass, new int[] { 131, 219, 64 });
        typeColours.Add(MonType.water, new int[] { 112, 165, 250 });
    }

    public void ChangeButtonColour(Button button, MonType type)
    {
        ColorBlock colors = button.colors;
        tmpRGB = typeColours[type];
        colors.normalColor = new Color(tmpRGB[0] / 255f, tmpRGB[1] / 255f, tmpRGB[2] / 255f);
        colors.highlightedColor = new Color(tmpRGB[0] * 1.5f / 255f, tmpRGB[1] * 1.5f / 255f, tmpRGB[2] * 1.5f / 255f);
        button.colors = colors;
    }

    public void ChangeButtonText(Button button, string input)
    {
        button.GetComponentInChildren<Text>().text = input;
    }

    public void ShowActionSelect()
    {
        baseSelectPanel.SetActive(true);
        moveSelectPanel.SetActive(false);
        backButton.gameObject.SetActive(false);

    }

    public void ShowMoveSelect()
    {
        baseSelectPanel.SetActive(false);
        moveSelectPanel.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
}
