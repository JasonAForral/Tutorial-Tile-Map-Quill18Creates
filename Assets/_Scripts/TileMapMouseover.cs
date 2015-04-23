using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouseover : MonoBehaviour {

    private Collider collider;
    private Renderer renderer;

    private Vector3 mousePosition;

    private TileMap tileMap;
    private float tileSizeReciprocal;

    private Transform selectionCube;
    public Vector3 hoverTile;
    private int x;
    private int z;
            

	// Use this for initialization
	void Awake () {
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
        tileMap = GetComponent<TileMap>();
        selectionCube = transform.Find("Cube").transform;

        tileSizeReciprocal = 1.0f / (float)tileMap.tileSize;

        selectionCube.localScale = Vector3.one * tileMap.tileSize;
        
	
	}
	
	// Update is called once per frame
	void Update () {

        MouseOver();
	
	}

    void MouseOver ()
    {
        mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (collider.Raycast(ray, out hit, Mathf.Infinity))
        {
            mousePosition = (hit.point - transform.position + tileMap.offset) * tileSizeReciprocal;
            x = Mathf.RoundToInt(mousePosition.x - 0.5f);
            z = Mathf.RoundToInt(mousePosition.z - 0.5f);
            hoverTile = new Vector3(x, 0f, z);
            selectionCube.position = new Vector3(x + 0.5f, 0.5f, z + 0.5f) * tileMap.tileSize - tileMap.offset;
        }
        else
        {

        }
    }
}
