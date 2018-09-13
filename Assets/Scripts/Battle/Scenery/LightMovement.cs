using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour {

    public float rotX = 0;
    public float rotY = 0.002f;
    public float rotZ = 0;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rotY, transform.eulerAngles.z);
	}
}
