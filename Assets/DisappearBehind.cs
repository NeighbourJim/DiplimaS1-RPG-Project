using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBehind : MonoBehaviour {

    GameObject p;
    GameObject c;
    float offset = 5f;
    RaycastHit hit;
    GameObject prevHit;

    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
        c = gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (Physics.Linecast(c.transform.position, p.transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("TransBehind"))
            {
                hit.collider.gameObject.GetComponent<Renderer>().enabled = false;  
                //hit.collider.gameObject.GetComponent<BecomeTransparent>().FadeOut();
            }
            if (prevHit != null)
            {
                if (hit.collider.gameObject != prevHit && prevHit.CompareTag("TransBehind"))
                {
                    prevHit.GetComponent<Renderer>().enabled = true;
                    //hit.collider.gameObject.GetComponent<BecomeTransparent>().FadeIn();
                }
            }
            prevHit = hit.collider.gameObject;
        }
	}
}
