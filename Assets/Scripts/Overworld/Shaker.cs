using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

    public float shakeSpeed = 6.0f;
    public float rotateAngle = 15f;

    Quaternion startRot;

    bool shake;

	// Use this for initialization
	void Start () {
        startRot = transform.rotation;
	}

    private void Update()
    {
        if (shake)
        {
            transform.rotation = Quaternion.Euler(rotateAngle * Mathf.Sin(Time.time * shakeSpeed), transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            transform.rotation = startRot;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        shake = true;
        yield return new WaitForSeconds(0.5f);
        shake = false;
        yield return null;
    }
}
