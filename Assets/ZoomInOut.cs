using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour {

    public GameObject zoomer;
    Transform zoomT;
    bool zooming = false;

	// Use this for initialization
	void Start () {
        zoomT = zoomer.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(zoomT);
        if(!zooming)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
            if(transform.position.z < -75f)
            {
                zooming = true;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
            if (transform.position.z > 15f)
            {
                zooming = false;
            }
        }
	}
}
