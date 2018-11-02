using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeTransparent_Tree : MonoBehaviour, IFadable {

    public GameObject stumpPrefab;
    GameObject myStump;
    MeshRenderer myStumpRenderer;
    MeshRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myStump = Instantiate(stumpPrefab);
        myStump.transform.SetParent(gameObject.transform);
        myStumpRenderer = myStump.GetComponent<MeshRenderer>();
        myStumpRenderer.enabled = false;
        myStump.transform.position = gameObject.transform.position;
        myStump.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
    }

    public void FadeIn()
    {
        myStumpRenderer.enabled = false;
        myRenderer.enabled = true;
    }

    public void FadeOut()
    {
        myStumpRenderer.enabled = true;
        myRenderer.enabled = false;
    }
}
