using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraControl : MonoBehaviour {

    Camera camera;
    Transform camTransform;

    Vector3 startPos;
    Quaternion startRot;

    BattleControl bControl;
    public GameObject focus;

    public int minimumWait = 5;
    public int maximumWait = 10;
    public float rotateSpeed = 5f;

    public bool started = false;
    bool finished = false;

    private void Start()
    {
        camera = this.GetComponent<Camera>();
        camTransform = this.GetComponent<Transform>();
        bControl = FindObjectOfType<BattleControl>();

        startPos = camTransform.position;
        startRot = camTransform.rotation;
        
        StartCoroutine(waitStart());
    }


    public void ResetCamera()
    {
        StopAllCoroutines();
        camTransform.position = startPos;
        camTransform.rotation = startRot;
        StartCoroutine(waitStart());
    }

   void Rotate()
    {
        StartCoroutine(RotateAroundObject(focus));
    }

    IEnumerator RotateAroundObject(GameObject obj)
    {
        StartCoroutine(DelayedReset(Random.Range(5,11)));
        bool left = Random.Range(0, 2) == 0;
        float r = rotateSpeed;
        while (true)
        {
            if (left)
            {
                camTransform.Translate(Vector3.left * r * Time.deltaTime);
            }
            else
            {
                camTransform.Translate(Vector3.right * r * Time.deltaTime);
            }
            r = r * 0.999f;
            camTransform.LookAt(obj.transform);
            yield return null;
        }
    }

    IEnumerator waitStart()
    {
        yield return new WaitForSeconds(Random.Range(minimumWait, maximumWait + 1));
        started = true;
        finished = false;
        Rotate();
    }

    IEnumerator DelayedReset(int seconds)
    {
        int i = 0;
        while(i <= seconds)
        {
            i++;
            yield return new WaitForSeconds(1);
        }
        ResetCamera();
    }
}
