using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeOccupation
{
    Outside = -1,
    Empty = 0,
    Wall = 1,
    Occupied = 2,
}


public class Apartment : MonoBehaviour {

    public static Apartment instance;

    public int[,] map;

    public ApartmentNode[,] objectMap;

    [SerializeField]
    private GameObject nodePrefab;

    [SerializeField]
    private Vector2 _apartmentSize;
    public static Vector2 apartmentSize { get { return instance._apartmentSize; } }

    void Awake()
    {
        instance = this;
    }

    void LoadMap()
    {
        objectMap = new ApartmentNode[(int)apartmentSize.x, (int)apartmentSize.y];
        map = new int[(int)apartmentSize.x, (int)apartmentSize.y];
        for (int x=0; x<apartmentSize.x; x++)
        {
            for(int y=0; y<apartmentSize.y; y++)
            {
                GameObject gObj = Instantiate(nodePrefab, new Vector3(x - (apartmentSize.x / 2), 0, y - (apartmentSize.y / 2)), Quaternion.identity) as GameObject;
                gObj.transform.parent = transform;
                objectMap[x,y] = gObj.GetComponent<ApartmentNode>();
                objectMap[x, y].coord = new Vector2(x, y);
                map[x, y] = 0;
            }
        }
    }

	// Use this for initialization
	void Start () {
        LoadMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
