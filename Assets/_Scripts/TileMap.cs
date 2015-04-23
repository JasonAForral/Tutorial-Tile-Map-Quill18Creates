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



	void Awake () {

        BuildMesh();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void BuildMesh ()
    {
        int width = 2;
        int height = 2;
        int count = 0;

        // generate Mesh D
        Vector3[] vertecies = new Vector3[height * width];
        int[] triangles = new int[6];
        Vector3[] normals = new Vector3[height * width];
        Vector2[] uv = new Vector2[4];

        count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                vertecies[count] = new Vector3(x, 0f, y);
                count++;
            }
        }

        //vertecies[0] = new Vector3(0f, 0f, 0f);
        //vertecies[1] = new Vector3(0f, 0f, 1f);
        //vertecies[2] = new Vector3(1f, 0f, 0f);
        //vertecies[3] = new Vector3(1f, 0f, 1f);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 3;
        
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;

        //triangles[6] = 2;
        //triangles[7] = 3;
        //triangles[8] = 5;

        //triangles[9] = 2;
        //triangles[10] = 5;
        //triangles[11] = 4;

        for (int normCount = 0; normCount < normals.Length; normCount++)
        {
            normals[normCount] = Vector3.up;
        }

        //normals[0] = Vector3.up;
        //normals[1] = Vector3.up;
        //normals[2] = Vector3.up;
        //normals[3] = Vector3.up;

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(1, 1);

        // create new mesh and populate with the data
        
        Mesh mesh = new Mesh();

        mesh.name = "The Mesh"; 
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

