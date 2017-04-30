using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowMouse : MonoBehaviour {

    public int mapID;

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool IsDragable = true;

    public NodeSelector[] selectors;
    public Transform[] detectors;

    private List<ApartmentNode> nodesTaken;
    public MeshRenderer meshRenderer;
    public int okCount;

    public AudioSource customPickupSound;
    public AudioSource customUseSound;

    public Vector3 mouseDownPos;
    public Vector3 mousePos;
    public float distance;
    public Vector3 direction;

    public Vector3 dragStartPosition;
    public ParticleSystem[] dustParticles;

    public bool isPlaced = false;

    public Vector3 defaultPosition;
    public Quaternion defaultRotation;
    
    public Color color;
    private float scaleValue;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    Vector3 RoundVec(Vector3 vec)
    {
        return new Vector3(2 * Mathf.Round(vec.x) / 2, 2 * Mathf.Round(vec.y) / 2, 2* Mathf.Round(vec.z) / 2);
    }

    Vector3 GetMousePoint()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        else return Vector3.zero;
    }

    void OnMouseDown()
    {
        if (GameManager.instance.currentState == GameState.GAME)
        {
            if (IsDragable && GameManager.currentPhase == GamePhase.PUZZLE)
            {
                dragStartPosition = transform.position;
                mouseDownPos = GetMousePoint();

                for (int i = 0; i < nodesTaken.Count; i++)
                {
                    Apartment.instance.map[(int)nodesTaken[i].coord.x, (int)nodesTaken[i].coord.y] = 0;
                    Apartment.instance.objectMap[(int)nodesTaken[i].coord.x, (int)nodesTaken[i].coord.y].SetColor(Color.white);
                }

                for (int i = 0; i < selectors.Length; i++)
                {
                    selectors[i].isActivated = true;
                }
                Cursor.visible = false;
            }
            if (IsDragable && GameManager.currentPhase == GamePhase.SIMS)    // Only do if IsDraggable == true
            {
                if (customPickupSound == null)
                    SoundManager.Grunt.Play();
                else
                    customPickupSound.Play();

                dragStartPosition = transform.position;
                mouseDownPos = GetMousePoint();

                //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                //screenPoint = Camera.main.transform.parent.TransformPoint(screenPoint);
                //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                isPlaced = false;
                for (int i = 0; i < nodesTaken.Count; i++)
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
    }

    bool MakeMove(Vector3 direction, Vector3 curPos)
    {
        for (int i = 0; i < selectors.Length; i++)
        {
            if (selectors[i].useInMovment)
            {
                Debug.DrawLine(selectors[i].transform.position, selectors[i].transform.position + direction, Color.blue, 2.0f);
                RaycastHit hit;
                if (Physics.Raycast(selectors[i].transform.position, direction, out hit, Vector3.Distance(selectors[i].transform.position, curPos), 1 << 8))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    return false;
                }
            }
        }
        return true;
    }


    void OnMouseDrag()
    {
        if (GameManager.instance.currentState == GameState.GAME)
        {
            if (IsDragable && GameManager.currentPhase == GamePhase.PUZZLE)    // Only do if IsDraggable == true
            {


                mousePos = GetMousePoint();

                Vector3 heading = mousePos - mouseDownPos;
                distance = 2 * Mathf.Ceil(heading.magnitude) / 2;
                direction = RoundVec(heading / (heading.magnitude));
                transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
                Vector3 oldPosition = transform.position;
                if (distance > 2)
                {

                    transform.localPosition += direction;
                    mouseDownPos = mousePos;
                }

                okCount = 0;

                for (int i = 0; i < selectors.Length; i++)
                {
                    RaycastHit hitinfo;
                    if (Physics.Raycast(selectors[i].transform.position, Vector3.down, out hitinfo, 1 << 9))
                    {
                        if (hitinfo.collider.CompareTag("Node"))
                        {
                            bool canbetargeted = hitinfo.collider.GetComponent<ApartmentNode>().TargetMe();
                            if (canbetargeted)
                            {
                                selectors[i].targetNode = hitinfo.collider.GetComponent<ApartmentNode>();
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
                if (meshRenderer != null && okCount == selectors.Length)
                {
                    //meshrenderer.material.color = color.green;
                }
                else
                {
                    transform.position = oldPosition;
                }

            }

            if (IsDragable && GameManager.currentPhase == GamePhase.SIMS)    // Only do if IsDraggable == true
            {

                mousePos = GetMousePoint();

                Vector3 heading = mousePos - mouseDownPos;
                distance = 2 * Mathf.Ceil(heading.magnitude) / 2;
                direction = RoundVec(heading / (heading.magnitude));
                transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
                Vector3 oldPosition = transform.position;
                if (distance > 2)
                {

                    transform.localPosition += direction;
                    mouseDownPos = mousePos;
                }

                //Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); // hardcode the y and z for your use

                //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

                //curPosition = Camera.main.transform.parent.TransformPoint(curPosition);

                //curPosition.z = curPosition.y;
                //curPosition.y = 2.5f;
                //curPosition.x = Mathf.Ceil(curPosition.x);
                //curPosition.z = Mathf.Ceil(curPosition.z);

                //transform.position = curPosition;
                //transform.localPosition = new Vector3(Mathf.Ceil(transform.localPosition.x) + 0.5f, transform.localPosition.y, Mathf.Ceil(transform.localPosition.z) + 0.5f);

                okCount = 0;

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    transform.Rotate(new Vector3(0, 90, 0));
                    SoundManager.Rotate.Play();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    transform.Rotate(new Vector3(0, -90, 0));
                    SoundManager.Rotate.Play();
                }

                for (int i = 0; i < selectors.Length; i++)
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(selectors[i].transform.position, Vector3.down, out hitInfo, 1 << 9))
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
                if (meshRenderer != null && okCount == selectors.Length)
                {
                    meshRenderer.material.color = Color.green;
                }
                else
                {
                    meshRenderer.material.color = Color.white;
                }
            }
        }
    }

    void DustUp()
    {
        for (int i = 0; i < dustParticles.Length; i++)
        {
            dustParticles[i].Play();
        }
        meshRenderer.transform.DOShakeScale(0.25f, 0.05f, 1, 25).OnComplete(FixScale);
    }

    void FixScale()
    {
        meshRenderer.transform.localScale = Vector3.one * scaleValue;
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

    public bool IsNeighboredTo(FollowMouse obj)
    {
        for (int i = 0; i < selectors.Length; i++)
        {
            int coordX = (int)selectors[i].targetNode.coord.x;
            int coordY = (int)selectors[i].targetNode.coord.y;
            if (coordX > 0)
            {
                if (Apartment.instance.map[coordX - 1, coordY] == obj.mapID)
                    return true;
            }
            if (coordX < Apartment.apartmentSize.x - 1)
            {
                if (Apartment.instance.map[coordX + 1, coordY] == obj.mapID)
                    return true;
            }
            if (coordY > 0)
            {
                if (Apartment.instance.map[coordX, coordY - 1] == obj.mapID)
                    return true;
            }
            if (coordY < Apartment.apartmentSize.y - 1)
            {
                if (Apartment.instance.map[coordX, coordY + 1] == obj.mapID)
                    return true;
            }
        }
        return false;
    }

    void OnMouseUp()
    {
        if (GameManager.instance.currentState == GameState.GAME)
        {
            //if (GameManager.currentPhase == GamePhase.SIMS)
            {
                Cursor.visible = true;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                bool isOK = IsCorrect() || IsOutside();
                meshRenderer.material.color = Color.white;

                if (!isOK)
                {
                    SoundManager.Fail.Play();
                    transform.position = dragStartPosition;
                    for (int i = 0; i < nodesTaken.Count; i++)
                    {
                        Apartment.instance.map[(int)nodesTaken[i].coord.x, (int)nodesTaken[i].coord.y] = mapID;
                        Apartment.instance.objectMap[(int)nodesTaken[i].coord.x, (int)nodesTaken[i].coord.y].SetColor(color);
                    }
                }
                else
                {
                    nodesTaken.Clear();
                    for (int i = 0; i < selectors.Length; i++)
                    {
                        int coordX = (int)selectors[i].targetNode.coord.x;
                        int coordY = (int)selectors[i].targetNode.coord.y;
                        Apartment.instance.map[coordX, coordY] = mapID;
                        Apartment.instance.objectMap[coordX, coordY].SetColor(color);
                        nodesTaken.Add(Apartment.instance.objectMap[coordX, coordY]);
                        Vector3 parentPos = selectors[i].transform.position - selectors[i].transform.localPosition;
                        parentPos.y = 0;
                        //selectors[i].transform.parent.position = parentPos;
                    }
                    DustUp();
                    isPlaced = true;
                    if (GameManager.currentPhase == GamePhase.SIMS)
                        GameManager.CheckItems();
                                       
                    SoundManager.Thump.Play();
                    if (GameManager.currentPhase == GamePhase.PUZZLE)
                    {
                        GameManager.instance.currentDay.CheckTask();
                    }
                }
            }
        }
    }

    void OnResetGame()
    {
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        isPlaced = false;
    }

    void Awake()
    {
        scaleValue = meshRenderer.transform.localScale.x;
        nodesTaken = new List<ApartmentNode>();
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        GameManager.instance.onResetGameEvent += OnResetGame;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
       
    }
}