using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateToolTip : MonoBehaviour {

    public GameObject Tooltip;
    private GameObject tooltip;

    private MoveToolTipControl ttCont;

    MonBattleData playerMonster;

    private void Start()
    {
        
    }

    public void ShowToolTip()
    {
        if (gameObject.GetComponent<Button>().IsInteractable())
        {
            tooltip = GameObject.FindGameObjectWithTag("UITooltip");
            if (tooltip != null)
            {
                Debug.Log("Destroying old tooltip");
                RemoveToolTip();
            }
            tooltip = Instantiate(Tooltip);
            tooltip.transform.SetParent(gameObject.transform, false);
            ttCont = tooltip.GetComponent<MoveToolTipControl>();
            playerMonster = GameObject.Find("BattleController").GetComponent<BattleControl>().playerMon;
            switch (gameObject.name)
            {
                case "Move1Button":
                    ttCont.SetText(playerMonster.learnedMoves[0]);
                    break;
                case "Move2Button":
                    ttCont.SetText(playerMonster.learnedMoves[1]);
                    break;
                case "Move3Button":
                    ttCont.SetText(playerMonster.learnedMoves[2]);
                    break;
                case "Move4Button":
                    ttCont.SetText(playerMonster.learnedMoves[3]);
                    break;
                case "NewMove1Button":
                    ttCont.SetText(playerMonster.learnedMoves[0]);
                    break;
                case "NewMove2Button":
                    ttCont.SetText(playerMonster.learnedMoves[1]);
                    break;
                case "NewMove3Button":
                    ttCont.SetText(playerMonster.learnedMoves[2]);
                    break;
                case "NewMove4Button":
                    ttCont.SetText(playerMonster.learnedMoves[3]);
                    break;
                case "NewMoveButton":
                    ttCont.SetText(playerMonster.CheckLevelMove());
                    break;

            }
        }
    }

    public void RemoveToolTip()
    {
        Destroy(tooltip);
    }
}
