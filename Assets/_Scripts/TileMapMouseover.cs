using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouseover : MonoBehaviour {

    [Tooltip("Make lots of these")]
    public GameObject prefabSphere;
    private Collider _collider;
    //private Renderer renderer;

    private Vector3 mousePosition;

    private TileMap tileMap;
    private float tileSizeReciprocal;

    private Transform selectionCube;
    public Vector3 hoverTile;
    private int x;
    private int z;

    [Tooltip("When True, continueously spawn")]
    public bool spawnFastMode;
            

	// Use this for initialization
	void Awake () {
        _collider = GetComponent<Collider>();
        //renderer = GetComponent<Renderer>();
        tileMap = GetComponent<TileMap>();
        selectionCube = transform.Find("Cube");

        tileSizeReciprocal = 1.0f / (float)tileMap.tileSize;

        selectionCube.localScale = Vector3.one * tileMap.tileSize;

        
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
            MouseOver();
        if (Input.GetButton("Fire2"))
            selectionCube.gameObject.SetActive(false);
        if (Input.GetButtonUp("Fire1"))
        {
            //selectionCube.gameObject.SetActive(false);
            //sphere.position = (hoverTile + Vector3.one * 0.5f + Vector3.up * 5f) * tileMap.tileSize - tileMap.offset;
            //sphere.velocity = Vector3.up;
            //sphere.angularVelocity = Vector3.zero;
            //Debug.Log("dropped");
            if (!spawnFastMode)
            {
                Rigidbody newSphere =  Instantiate(prefabSphere, (hoverTile + Vector3.one * 0.5f + Vector3.up * 5f) * tileMap.tileSize - tileMap.offset, Quaternion.identity) as Rigidbody;
            }
        
        }
	
	}

    void MouseOver ()
    {
        mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (_collider.Raycast(ray, out hit, Mathf.Infinity))
        {
            selectionCube.gameObject.SetActive(true);
            mousePosition = (hit.point - transform.position + tileMap.offset) * tileSizeReciprocal;
            x = Mathf.RoundToInt(mousePosition.x - 0.5f);
            z = Mathf.RoundToInt(mousePosition.z - 0.5f);
            hoverTile = new Vector3(x, 0f, z);
            selectionCube.position = (hoverTile + Vector3.one * 0.5f) * tileMap.tileSize - tileMap.offset;
            if (spawnFastMode)
            {
                Rigidbody newSphere =  Instantiate(prefabSphere, (hoverTile + Vector3.one * 0.5f + Vector3.up * 5f) * tileMap.tileSize - tileMap.offset, Quaternion.identity) as Rigidbody;
            }
        }
    }
}
