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
                MonoBehaviour[] scripts = hit.collider.gameObject.GetComponents<MonoBehaviour>();
                foreach(MonoBehaviour s in scripts)
                {
                    if(s is IFadable)
                    {
                        IFadable fadable = (IFadable)s;
                        fadable.FadeOut();
                    }
                }
            }
            if (prevHit != null)
            {
                if (hit.collider.gameObject != prevHit && prevHit.CompareTag("TransBehind"))
                {
                    MonoBehaviour[] scripts = prevHit.GetComponents<MonoBehaviour>();
                    foreach(MonoBehaviour s in scripts)
                    {
                        if(s is IFadable)
                        {
                            IFadable fadable = (IFadable)s;
                            fadable.FadeIn();
                            
                        }
                    }
                }
            }
            prevHit = hit.collider.gameObject;
        }
	}
}
