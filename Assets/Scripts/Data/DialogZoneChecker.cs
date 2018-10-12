using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogZoneChecker : MonoBehaviour {

    public bool inDialogZone;
    GameObject dialogObject = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DialogZone"))
        {
            inDialogZone = true;
            dialogObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DialogZone"))
        {
            inDialogZone = false;
            dialogObject = null;
        }
    }

    public DialogData GetDialogObject()
    {
        if (dialogObject.GetComponent<DialogData>() != null)
            return dialogObject.GetComponent<DialogData>();
        else
            return null;
    }
}
