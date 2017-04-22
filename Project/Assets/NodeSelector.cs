using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelector : MonoBehaviour {

    public bool isActivated;
    public bool isOK;

    public Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActivated)
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3.down * 10), Color.red);
        }
	}
}
