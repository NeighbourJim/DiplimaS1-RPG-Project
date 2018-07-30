using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassWave : MonoBehaviour {

    public float rotateSpeed = 1f;
    public float rotateAngle = 15f;
    public float waitMax = 3f;
    public bool startSway = false;	

    private void Update()
    {
        if (startSway)
        {
            transform.rotation = Quaternion.Euler(rotateAngle * Mathf.Sin(Time.time * rotateSpeed), transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    void Awake () {
        StartCoroutine(waitRandom());
    }

    IEnumerator waitRandom()
    {
        yield return new WaitForSeconds(Random.Range(0, waitMax));
        startSway = true;
    }
}
