using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour 
{
    public bool centered = true;

    //vector2 tileCount = new vector2(3, 2
    
    public int tileCountX = 1;
    public int tileCountY = 1;
    public float tileSize = 1f;
    public float randomHeight = 1f;
    public Vector3 offset;
    public float perlinScale = 1f;
    int vertexCountX;
    int vertexCountY;
    //Vector3 tileTable;

    float tileCountReciprocalX;
    float tileCountReciprocalY;

    float vertexCountReciprocalX;
    float vertexCountReciprocalY;

    float perlinOrgX;
    float perlinOrgY;

    void Awake () {

        Initialize();
        BuildTexture();
        BuildMesh();
        

	}

    public void Initialize ()
    {
        //tileTable = 

        tileCountReciprocalX = 1f / (float)tileCountX;
        tileCountReciprocalY = 1f / (float)tileCountY;
        //Debug.Log(tileCountX + ", " + tileCountY);

        vertexCountX = tileCountX + 1;
        vertexCountY = tileCountY + 1;

        vertexCountReciprocalX = 1f / (float)vertexCountX;
        vertexCountReciprocalY = 1f / (float)vertexCountY;

        perlinOrgX = Random.Range(0f, 100f);
        perlinOrgY = Random.Range(0f, 100f);

    }

    public void BuildTexture ()
    {
        //Debug.Log(tileCountX + ", " + tileCountY);
        int textureWidth = 100;
        int textureHeight = 100;
        Texture2D texture = new Texture2D(textureWidth, textureWidth);

        for (int y = 0; y < textureHeight; y++)
        {
                for (int x = 0; x < textureWidth; x++)
            {
                //Debug.Log(x + ", " + y);
                float perlinShade = Mathf.PerlinNoise((x + perlinOrgX) * vertexCountReciprocalX * perlinScale, (y + perlinOrgX) * vertexCountReciprocalY * perlinScale);
                texture.SetPixel(x, y, new Color(perlinShade, perlinShade, perlinShade));//, perlinShade));
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.name = "Perlin Noise";

        Debug.Log(texture.height);
        texture.Apply();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sharedMaterial.mainTexture = texture;
        

    }

    public void BuildMesh ()
    {
        offset = Vector3.zero;
        if (centered)
        {
            offset = new Vector3(tileCountX, 0f, tileCountY) * tileSize * 0.5f;
            
        }
	
        
        int numVerts = vertexCountX * vertexCountY;
        
        int numTiles = tileCountX * tileCountY;
        int numTriangles = numTiles * 2;
        
        // generate Mesh Data
        Vector3[] vertecies = new Vector3[numVerts];
        int[] triangles = new int[numTriangles * 6];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        for (int x = 0; x < vertexCountX; x++)
        {
            for (int z = 0; z < vertexCountY; z++)
            {

                int vertexCountIndex = z + x * vertexCountY;
                //Debug.Log(vertexCountIndex);
                vertecies[vertexCountIndex] = new Vector3(x, (Mathf.PerlinNoise(x * vertexCountReciprocalX * perlinScale, z * vertexCountReciprocalY * perlinScale) - 0.5f) * randomHeight, z) * tileSize - offset;
                normals[vertexCountIndex] = Vector3.up;
                uv[vertexCountIndex] = new Vector2(x * tileCountReciprocalX, z * tileCountReciprocalY);

                //Debug.Log(Mathf.PerlinNoise(x * vectorCountReciprocalX, z * vectorCountReciprocalY));
                //Debug.Log(x + ", " + z + ": " + x * tileCountReciprocalX + ", " + z * tileCountReciprocalY);
                //Debug.Log(x + ", " + z + ": " + new Vector2(x * tileCountReciprocalX, z * tileCountReciprocalY));
            }
        }

        for (int x = 0; x < tileCountX; x++)
        {
            for (int z = 0; z < tileCountY; z++)
            {
                int tileCoordIndex = 6 * (z + x * tileCountY);
                int vertexCoord = (z + x * vertexCountY);

                triangles[tileCoordIndex + 0] = vertexCoord + 0;
                triangles[tileCoordIndex + 1] = vertexCoord + 1;
                triangles[tileCoordIndex + 2] = vertexCoord + 1 + vertexCountY;

                triangles[tileCoordIndex + 3] = vertexCoord + 0;
                triangles[tileCoordIndex + 4] = vertexCoord + 1 + vertexCountY;
                triangles[tileCoordIndex + 5] = vertexCoord + vertexCountY;
            }
        }



        // 0, 1, 3, 0, 3, 2

        // create new mesh and populate with the data
        
        Mesh mesh = new Mesh();
        mesh.Clear();

        mesh.name = "TileMesh"; 
        mesh.vertices = vertecies;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        mesh.RecalculateBounds();
        mesh.Optimize();

        // Assign mesh to object components

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        //meshFilter..Clear();

        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;

        
    }
}

