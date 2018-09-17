using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToolTipControl : MonoBehaviour {

    public Text nameText, powerText, accuracyText, descriptionText, physSpecText;

    public float xOffset, yOffset;

	public void SetText(MoveData move)
    {
        nameText.text = string.Format("Type: {0}", move.moveType.typeName);
        if (move.basePower > 0)
            powerText.text = string.Format("POW: {0}", move.basePower);
        else
            powerText.text = "";
        accuracyText.text = string.Format("ACC: {0}%", move.accuracy);
        descriptionText.text = move.moveDescription;
        physSpecText.text = move.physSpec.ToString();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Input.mousePosition.x + xOffset, Input.mousePosition.y + yOffset, Input.mousePosition.z);
    }
}
