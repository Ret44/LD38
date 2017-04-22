using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentNode : MonoBehaviour {
        
    [SerializeField]
    private MeshRenderer meshRenderer;

    public bool isTargeted;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}

    public bool TargetMe()
    {
        isTargeted = true;
        return true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isTargeted)
            meshRenderer.material.color = Color.green;        
        else
            meshRenderer.material.color = Color.white;        
	}

    void LateUpdate()
    {
        isTargeted = false;
    }
}
