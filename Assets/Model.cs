using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    List<Vector3> vertices;
    List<Vector3Int> faces;

    public Model()
    {
        vertices = new List<Vector3>();
        faces = new List<Vector3Int>();
        loadModelData();
    }

    private void loadModelData()
    {
        loadVertices();
        loadFaces();
    }

    private void loadFaces()
    {
        vertices.Add(new Vector3(-5, -8, -1)); //0
        vertices.Add(new Vector3(-2, -8, -1)); //1
        vertices.Add(new Vector3(2, -8, -1)); //2
        vertices.Add(new Vector3(5, -8, -1)); //3
        vertices.Add(new Vector3(-2, -2, -1)); //4
        vertices.Add(new Vector3(0, 0, -1)); //5
        vertices.Add(new Vector3(-2, 2, -1)); //6
        vertices.Add(new Vector3(-5, 8, -1)); //7
        vertices.Add(new Vector3(-2, 8, -1)); //8
        vertices.Add(new Vector3(2, 8, -1)); //9
        vertices.Add(new Vector3(5, 8, -1)); //10

        vertices.Add(new Vector3(-5, -8, 1)); //11
        vertices.Add(new Vector3(-2, -8, 1)); //12
        vertices.Add(new Vector3(2, -8, 1)); //13
        vertices.Add(new Vector3(5, -8, 1)); //14
        vertices.Add(new Vector3(-2, -2, 1)); //15
        vertices.Add(new Vector3(0, 0, 1)); //16
        vertices.Add(new Vector3(-2, 2, 1)); //17
        vertices.Add(new Vector3(-5, 8, 1)); //18
        vertices.Add(new Vector3(-2, 8, 1)); //19
        vertices.Add(new Vector3(2, 8, 1)); //20
        vertices.Add(new Vector3(5, 8, 1)); //21
    }

    private void loadVertices()
    {
        //front face
        faces.Add(new Vector3Int(0, 1, 8));
        faces.Add(new Vector3Int(0, 8, 7));

        faces.Add(new Vector3Int(6, 5, 10));
        faces.Add(new Vector3Int(6, 10, 9));

        faces.Add(new Vector3Int(2, 3, 5));
        faces.Add(new Vector3Int(2, 5, 4));

        faces.Add(new Vector3Int(4, 5, 6));

        //back face
        faces.Add(new Vector3Int(11, 18, 19));
        faces.Add(new Vector3Int(11, 19, 12));

        faces.Add(new Vector3Int(17, 20, 21));
        faces.Add(new Vector3Int(17, 21, 16));

        faces.Add(new Vector3Int(13, 15, 16));
        faces.Add(new Vector3Int(13, 16, 14));

        faces.Add(new Vector3Int(15, 17, 16));


        //sides
        faces.Add(new Vector3Int(11, 7, 18));
        faces.Add(new Vector3Int(11, 0, 7));

        faces.Add(new Vector3Int(7, 8, 19));
        faces.Add(new Vector3Int(7, 19, 18));

        faces.Add(new Vector3Int(0, 11, 12));
        faces.Add(new Vector3Int(0, 12, 1));

        faces.Add(new Vector3Int(9, 10, 21));
        faces.Add(new Vector3Int(9, 21, 20));

        faces.Add(new Vector3Int(2, 13, 14));
        faces.Add(new Vector3Int(2, 14, 3));

        faces.Add(new Vector3Int(12, 15, 4));
        faces.Add(new Vector3Int(12, 4, 1));

        faces.Add(new Vector3Int(17, 19, 8));
        faces.Add(new Vector3Int(17, 8, 6));

        faces.Add(new Vector3Int(17, 6, 9));
        faces.Add(new Vector3Int(17, 9, 20));

        faces.Add(new Vector3Int(13, 2, 4));
        faces.Add(new Vector3Int(13, 4, 15));

        faces.Add(new Vector3Int(5, 16, 21));
        faces.Add(new Vector3Int(5, 21, 10));

        faces.Add(new Vector3Int(3, 14, 16));
        faces.Add(new Vector3Int(3, 16, 5));
    }

    ////////////////////////////////////////////////////////

    public GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();

        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        /*List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();*/

        for (int i = 0; i < faces.Count; i++)
        {
            //Vector3 normal_for_face = normals[i];

            //normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); //text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); //text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); //text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        /*mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();*/
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
