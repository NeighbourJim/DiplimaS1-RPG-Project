using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBehind : MonoBehaviour {

    GameObject p;
    GameObject c;
    MeshRenderer mr;
    float offset = 5f;

    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
        c = GameObject.FindGameObjectWithTag("MainCamera");
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update () {
		if(p.transform.position.z > transform.position.z + offset && c.transform.position.z < transform.position.z + offset)
        {
            if(mr.enabled)
                mr.enabled = false;
        }
        else
        {
            if(!mr.enabled)
                mr.enabled = true;
        }
	}
}
