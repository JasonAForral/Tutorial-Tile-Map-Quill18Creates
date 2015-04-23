using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour 
{
    bool centered = true;

    //vector2 tileCount = new vector2(3, 2
    
    int tileCountX = 100;
    int tileCountY = 50;
    float tileSize = 0.1f;
    void Awake () {

        BuildMesh();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void BuildMesh ()
    {
        Vector3 offset = Vector3.zero;
        if (centered)
        {
            offset = new Vector3(tileCountX, 0f, tileCountY) * tileSize * 0.5f;
            
        }
	
        int vertexCountX = tileCountX + 1;
        int vertexCountY = tileCountY + 1;

        float vectorCountReciprocalX = 1 / vertexCountX;
        float vectorCountReciprocalY = 1 / vertexCountY;

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
                int vertexCount = z + x * vertexCountY;
                vertecies[vertexCount] = new Vector3(x, 0f, z) * tileSize - offset;
                normals[vertexCount] = Vector3.up;
                uv[vertexCount] = new Vector2(x * vectorCountReciprocalX, z * vectorCountReciprocalY);
            }
        }

        for (int x = 0; x < tileCountX; x++)
        {
            for (int z = 0; z < tileCountY; z++)
            {
                int tileCoord = 6 * (z + x * tileCountY);
                int vertexCoord = (z + x * vertexCountY);

                triangles[tileCoord + 0] = vertexCoord + 0;
                triangles[tileCoord + 1] = vertexCoord + 1;
                triangles[tileCoord + 2] = vertexCoord + 1 + vertexCountY;

                triangles[tileCoord + 3] = vertexCoord + 0;
                triangles[tileCoord + 4] = vertexCoord + 1 + vertexCountY;
                triangles[tileCoord + 5] = vertexCoord + vertexCountY;
            }
        }



        // 0, 1, 3, 0, 3, 2

        // create new mesh and populate with the data
        
        Mesh mesh = new Mesh();

        mesh.name = "TileMesh"; 
        mesh.vertices = vertecies;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        mesh.RecalculateBounds();
        mesh.Optimize();

        // Assign mesh to object components

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        

    }
}

