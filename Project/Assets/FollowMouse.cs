using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool IsDragable = true;

    public NodeSelector[] selectors;

    public int okCount;

    public Vector3 dragStartPosition;

    void OnMouseDown()
    {

        if (IsDragable)    // Only do if IsDraggable == true
        {
            dragStartPosition = transform.position;

            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            for (int i = 0; i < selectors.Length; i++)
            {
                selectors[i].isActivated = true;
            }
            Cursor.visible = false;
        }
    }


    void OnMouseDrag()
    {
        if (IsDragable)    // Only do if IsDraggable == true
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); // hardcode the y and z for your use

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
           
            curPosition.z = curPosition.y;
            curPosition.y = 3;
            curPosition.x = Mathf.Round(curPosition.x);
            curPosition.z = Mathf.Round(curPosition.z);
            transform.position = curPosition;

            okCount = 0;

            for (int i = 0; i < selectors.Length; i++)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(selectors[i].transform.position,Vector3.down, out hitInfo))
                {
                    if (hitInfo.collider.CompareTag("Node"))
                    {
                        selectors[i].isOK = hitInfo.collider.GetComponent<ApartmentNode>().TargetMe();
                        if (selectors[i].isOK)
                        {
                            selectors[i].targetPosition = hitInfo.collider.transform.position;

                            okCount++;
                        }
                    }
                    else
                        selectors[i].isOK = false;
                }
            }
        }
    }

    void OnMouseUp()
    {
        Cursor.visible = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        bool isOK = (okCount == selectors.Length);
            
        if (!isOK)
            transform.position = dragStartPosition;
        else
        {
            for(int i=0; i<selectors.Length; i++)
            {
                Vector3 parentPos = selectors[i].transform.position - selectors[i].transform.localPosition;
                parentPos.y = 0;
                selectors[i].transform.parent.position = parentPos;
            }
        }
        //if(isOK)
        //{
        //    for(int i=0; i< selectors.Length; i++)
        //    {

        //    }
        //}
    }

  

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
       
    }
}