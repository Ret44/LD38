using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour {

    public bool allowRotation;

    [SerializeField]
    private float rotationSpeed;
    
    private Transform objTransform;

    void Awake()
    {
        objTransform = transform;
    }

    void RotationLogic()
    {
       objTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
    }

    // Update is called once per frame
	void Update () {
        if(allowRotation && Input.GetMouseButton(1))
            RotationLogic();	
	}
}
