using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public Transform obj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Plane moveAxis = new Plane();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float intersect = 0;
        Vector3 movePoint;

        if (Input.GetMouseButtonDown(0))
        {
            if (!obj)
            {
                if (Physics.Raycast(ray,out hit,100,1<<8))
                {
                    obj = hit.transform;
                    moveAxis = new Plane(Vector3.up, obj.position);                    
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            obj = null;
        }

        if (obj)
        {
            Debug.Log("OliOlio");
            if (moveAxis.Raycast(ray,out intersect))
            {
                movePoint = ray.GetPoint(intersect);
                Debug.Log("x " + movePoint.x + " y " + movePoint.y + " z " + movePoint.z);
                obj.position = movePoint;
            }
        }
    }
}
