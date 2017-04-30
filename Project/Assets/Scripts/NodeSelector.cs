using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodePosition
{
    Outside,
    Correct,
    Incorrect
}


public class NodeSelector : MonoBehaviour {

    public bool isActivated;
    public NodePosition nodePosition;
    public bool useInMovment;
    public ApartmentNode targetNode;

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
