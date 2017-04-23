using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool IsDragable = true;

    public NodeSelector[] selectors;

    private List<ApartmentNode> nodesTaken;
    public MeshRenderer meshRenderer;
    public int okCount;

    public Vector3 dragStartPosition;
    public ParticleSystem dustParticles;

    void OnMouseDown()
    {

        if (IsDragable)    // Only do if IsDraggable == true
        {
            dragStartPosition = transform.position;

            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            for(int i=0; i < nodesTaken.Count; i++)
            {
                Apartment.instance.map[(int)nodesTaken[i].coord.x, (int)nodesTaken[i].coord.y] = 0;
            }

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
            curPosition.x = Mathf.Round(2*curPosition.x) / 2;
            curPosition.z = Mathf.Round(2*curPosition.z) / 2;
            
            transform.position = curPosition;

            okCount = 0;

            if(Input.GetKeyDown(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.Rotate(new Vector3(0, -90, 0));
            }

            for (int i = 0; i < selectors.Length; i++)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(selectors[i].transform.position,Vector3.down, out hitInfo, 1<<9))
                {
                    if (hitInfo.collider.CompareTag("Node"))
                    {
                        bool canBeTargeted = hitInfo.collider.GetComponent<ApartmentNode>().TargetMe();
                        if (canBeTargeted)
                        {
                            selectors[i].targetNode = hitInfo.collider.GetComponent<ApartmentNode>();
                            selectors[i].nodePosition = NodePosition.Correct;
                            okCount++;
                        }
                        else
                        {
                            selectors[i].nodePosition = NodePosition.Incorrect;
                        }
                    }
                    else
                        selectors[i].nodePosition = NodePosition.Outside;
                }
            }
            if(meshRenderer != null && okCount == selectors.Length)
            {
                meshRenderer.material.color = Color.green;
            }
            else
            {
                meshRenderer.material.color = Color.white;
            }
        }
    }

    bool IsOutside()
    {
        bool outside = true;
        for (int i = 0; i < selectors.Length; i++)
            if (selectors[i].nodePosition != NodePosition.Outside)
                outside = false;
        return outside;
    }

    bool IsCorrect()
    {
        bool correct = true;
        for (int i = 0; i < selectors.Length; i++)
            if (selectors[i].nodePosition != NodePosition.Correct)
                correct = false;
        return correct;
    }

    void OnMouseUp()
    {
        Cursor.visible = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        bool isOK = IsCorrect() || IsOutside();
        meshRenderer.material.color = Color.white;

        if (!isOK)
        {
            transform.position = dragStartPosition;
            for (int i = 0; i < nodesTaken.Count; i++)
            {
                Apartment.instance.map[(int)nodesTaken[i].coord.x, (int)nodesTaken[i].coord.y] = 1;
            }
        }
        else
        {
            nodesTaken.Clear();
            for (int i = 0; i < selectors.Length; i++)
            {
                int coordX = (int)selectors[i].targetNode.coord.x;
                int coordY = (int)selectors[i].targetNode.coord.y;
                Apartment.instance.map[coordX, coordY] = 1;
                nodesTaken.Add(Apartment.instance.objectMap[coordX, coordY]);
                Vector3 parentPos = selectors[i].transform.position - selectors[i].transform.localPosition;
                parentPos.y = 0;
                //selectors[i].transform.parent.position = parentPos;
            }
        }
        //if(isOK)
        //{
        //    for(int i=0; i< selectors.Length; i++)
        //    {

        //    }
        //}
    }

   void Awake()
    {
        nodesTaken = new List<ApartmentNode>();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
       
    }
}