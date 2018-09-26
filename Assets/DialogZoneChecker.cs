using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogZoneChecker : MonoBehaviour {

    public bool inDialogZone;
    GameObject dialogObject;

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
        return dialogObject.GetComponent<DialogData>();
    }
}
