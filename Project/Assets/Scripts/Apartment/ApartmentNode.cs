using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentNode : MonoBehaviour {
        
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    public TextMesh debugUi;
    public bool debugGrid = false;

    public NodePosition status;
    public Vector2 coord;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}

    public bool TargetMe()
    {
        if (Apartment.instance.map[(int)coord.x, (int)coord.y] != 0)
        {
            status = NodePosition.Incorrect;
            return false;
        }
        status = NodePosition.Correct;
        return true;
    }
	
	// Update is called once per frame
	void Update () {
        if (status == NodePosition.Correct)
            meshRenderer.material.color = Color.green;        
        else if(status == NodePosition.Incorrect)
            meshRenderer.material.color = Color.red;        
        else
            meshRenderer.material.color = Color.white;

        debugUi.text = debugGrid?Apartment.instance.map[(int)coord.x, (int)coord.y].ToString():"";
	}

    void LateUpdate()
    {
        status = NodePosition.Outside;
    }
}
