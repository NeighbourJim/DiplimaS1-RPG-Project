using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleXPBarControl : MonoBehaviour {

    public GameObject xpBar;
    private RectTransform xpBarRect;
    public GameObject xpBarInc;
    private RectTransform xpBarIncRect;

    public GameObject battleController;
    BattleControl battleCont;

    MonBattleData playerMonster;

    float barMax;
    float barSize;

	// Use this for initialization
	void Start () {
        xpBarRect = xpBar.GetComponent<RectTransform>();
        xpBarIncRect = xpBarInc.GetComponent<RectTransform>();
        battleCont = battleController.GetComponent<BattleControl>();
        barMax = xpBarRect.rect.width;
        barSize = barMax;
        playerMonster = battleCont.playerMon;
	}

    public void SetXPBarSize(MonBattleData mon)
    {
        float xpPer = GetPer(battleCont.playerMon.curXP, battleCont.playerMon.xpToNextLevel);
        float realBarSize = Mathf.Floor(barMax * xpPer);
        Debug.Log(realBarSize);
        float prevSize = xpBarIncRect.rect.width;

        xpBarIncRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, realBarSize);
        if (xpBarIncRect.rect.width > prevSize)
        {
            StartCoroutine(BarGrow(xpBarRect, realBarSize));
        }
    }

    float GetPer(float numerator, float denominator)
    {
        return numerator / denominator;
    }

    IEnumerator BarGrow(RectTransform bar, float targetSize)
    {
        while (bar.rect.width != targetSize)
        {
            bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bar.rect.width + 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
