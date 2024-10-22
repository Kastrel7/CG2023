using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    public List<Vector3> vertices;
    public List<Vector3Int> faces;
    public List<Vector2> texture_coordinates;
    public List<Vector3Int> texture_index_list;
    public List<Vector3> normals;

    public Model()
    {
        vertices = new List<Vector3>();
        texture_coordinates = new List<Vector2>();
        faces = new List<Vector3Int>(); 
        texture_index_list = new List<Vector3Int>();
        normals = new List<Vector3>();
        loadModelData();
    }

    private void loadModelData()
    {
        loadVertices();
        loadTexture();
        loadFaces();
        
        texture_coordinates = Adjust_to_relative(texture_coordinates, 1024, 1024);
    }

    private void loadTexture()
    {
        texture_coordinates.Add(new Vector2(0, 424)); //0
        texture_coordinates.Add(new Vector2(99, 424)); //1
        texture_coordinates.Add(new Vector2(200, 424)); //2
        texture_coordinates.Add(new Vector2(299, 424)); //3
        texture_coordinates.Add(new Vector2(95, 250)); //4
        texture_coordinates.Add(new Vector2(150, 212)); //5
        texture_coordinates.Add(new Vector2(95, 174)); //6
        texture_coordinates.Add(new Vector2(0, 0)); //7
        texture_coordinates.Add(new Vector2(99, 0)); //8
        texture_coordinates.Add(new Vector2(200, 0)); //9
        texture_coordinates.Add(new Vector2(299, 0)); //10

        texture_coordinates.Add(new Vector2(698, 424)); //11
        texture_coordinates.Add(new Vector2(599, 424)); //12
        texture_coordinates.Add(new Vector2(499, 424)); //13
        texture_coordinates.Add(new Vector2(400, 424)); //14
        texture_coordinates.Add(new Vector2(603, 250)); //15
        texture_coordinates.Add(new Vector2(548, 212)); //16
        texture_coordinates.Add(new Vector2(603, 174)); //17
        texture_coordinates.Add(new Vector2(698, 0)); //18
        texture_coordinates.Add(new Vector2(599, 0)); //19
        texture_coordinates.Add(new Vector2(499, 0)); //20
        texture_coordinates.Add(new Vector2(400, 0)); //21

        texture_coordinates.Add(new Vector2(0, 525)); //22
        texture_coordinates.Add(new Vector2(99, 525)); //23
        texture_coordinates.Add(new Vector2(0, 624)); //24
        texture_coordinates.Add(new Vector2(99, 624)); //25

        texture_coordinates.Add(new Vector2(200, 450)); //26
        texture_coordinates.Add(new Vector2(299, 450)); //27
        texture_coordinates.Add(new Vector2(200, 624)); //28
        texture_coordinates.Add(new Vector2(299, 624)); //29

        texture_coordinates.Add(new Vector2(399, 451)); //30
        texture_coordinates.Add(new Vector2(498, 451)); //31
        texture_coordinates.Add(new Vector2(399, 711)); //32
        texture_coordinates.Add(new Vector2(498, 711)); //33

        texture_coordinates.Add(new Vector2(799, 0)); //34
        texture_coordinates.Add(new Vector2(898, 0)); //35
        texture_coordinates.Add(new Vector2(799, 424)); //36
        texture_coordinates.Add(new Vector2(898, 424)); //37

    }

    private void loadVertices()
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

    private void loadFaces()
    {
        //front face
        faces.Add(new Vector3Int(0, 1, 8)); texture_index_list.Add(new Vector3Int(0,1,8 )); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(0, 8, 7)); texture_index_list.Add(new Vector3Int(0,8,7)); normals.Add(new Vector3(0, 0, -1));

        faces.Add(new Vector3Int(6, 5, 10)); texture_index_list.Add(new Vector3Int(6,5,10)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(6, 10, 9)); texture_index_list.Add(new Vector3Int(6,10,9)); normals.Add(new Vector3(0, 0, -1));

        faces.Add(new Vector3Int(2, 3, 5)); texture_index_list.Add(new Vector3Int(2, 3, 5)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(2, 5, 4)); texture_index_list.Add(new Vector3Int(2, 5, 4)); normals.Add(new Vector3(0, 0, -1));

        faces.Add(new Vector3Int(4, 5, 6)); texture_index_list.Add(new Vector3Int(4, 5, 6)); normals.Add(new Vector3(0, 0, -1));

        //back face
        faces.Add(new Vector3Int(11, 18, 19)); texture_index_list.Add(new Vector3Int(11, 18, 19)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(11, 19, 12)); texture_index_list.Add(new Vector3Int(11, 19, 12)); normals.Add(new Vector3(0, 0, 1));

        faces.Add(new Vector3Int(17, 20, 21)); texture_index_list.Add(new Vector3Int(17, 20, 21)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(17, 21, 16)); texture_index_list.Add(new Vector3Int(17, 21, 16)); normals.Add(new Vector3(0, 0, 1));

        faces.Add(new Vector3Int(13, 15, 16)); texture_index_list.Add(new Vector3Int(13, 15, 16)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(13, 16, 14)); texture_index_list.Add(new Vector3Int(13, 16, 14)); normals.Add(new Vector3(0, 0, 1));

        faces.Add(new Vector3Int(15, 17, 16)); texture_index_list.Add(new Vector3Int(15, 17, 16)); normals.Add(new Vector3(0, 0, 1));

        //sides
        faces.Add(new Vector3Int(11, 7, 18)); texture_index_list.Add(new Vector3Int(36, 35, 34)); normals.Add(new Vector3(-1, 0, 0));
        faces.Add(new Vector3Int(11, 0, 7)); texture_index_list.Add(new Vector3Int(36, 37, 35)); normals.Add(new Vector3(-1, 0, 0));

        faces.Add(new Vector3Int(7, 8, 19)); texture_index_list.Add(new Vector3Int(24, 25, 23)); normals.Add(new Vector3(0, 1, 0));
        faces.Add(new Vector3Int(7, 19, 18)); texture_index_list.Add(new Vector3Int(24, 23, 22)); normals.Add(new Vector3(0, 1, 0));

        faces.Add(new Vector3Int(0, 11, 12)); texture_index_list.Add(new Vector3Int(24, 25, 23)); normals.Add(new Vector3(0, -1, 0));
        faces.Add(new Vector3Int(0, 12, 1)); texture_index_list.Add(new Vector3Int(24, 23, 22)); normals.Add(new Vector3(0, -1, 0));

        faces.Add(new Vector3Int(9, 10, 21)); texture_index_list.Add(new Vector3Int(24, 25, 23)); normals.Add(new Vector3(0, 1, 0));
        faces.Add(new Vector3Int(9, 21, 20)); texture_index_list.Add(new Vector3Int(24, 23, 22)); normals.Add(new Vector3(0, 1, 0));

        faces.Add(new Vector3Int(2, 13, 14)); texture_index_list.Add(new Vector3Int(24, 25, 23)); normals.Add(new Vector3(0, -1, 0));
        faces.Add(new Vector3Int(2, 14, 3)); texture_index_list.Add(new Vector3Int(24, 23, 22)); normals.Add(new Vector3(0, -1, 0));

        faces.Add(new Vector3Int(12, 15, 4)); texture_index_list.Add(new Vector3Int(28, 29, 27)); normals.Add(new Vector3(1, 0, 0));
        faces.Add(new Vector3Int(12, 4, 1)); texture_index_list.Add(new Vector3Int(28, 27, 26)); normals.Add(new Vector3(1, 0, 0));

        faces.Add(new Vector3Int(17, 19, 8)); texture_index_list.Add(new Vector3Int(28, 29, 27)); normals.Add(new Vector3(1, 0, 0));
        faces.Add(new Vector3Int(17, 8, 6)); texture_index_list.Add(new Vector3Int(28, 27, 26)); normals.Add(new Vector3(1, 0, 0));

        faces.Add(new Vector3Int(17, 6, 9)); texture_index_list.Add(new Vector3Int(32, 33, 31)); normals.Add(new Vector3(-4, 3, 1));
        faces.Add(new Vector3Int(17, 9, 20)); texture_index_list.Add(new Vector3Int(32, 31, 30)); normals.Add(new Vector3(-4, 3, 1));

        faces.Add(new Vector3Int(13, 2, 4)); texture_index_list.Add(new Vector3Int(32, 33, 31)); normals.Add(new Vector3(-4, -3, 1));
        faces.Add(new Vector3Int(13, 4, 15)); texture_index_list.Add(new Vector3Int(32, 31, 30)); normals.Add(new Vector3(-4, -3, 1));

        faces.Add(new Vector3Int(5, 16, 21)); texture_index_list.Add(new Vector3Int(32, 33, 31)); normals.Add(new Vector3(8, -5, 1));
        faces.Add(new Vector3Int(5, 21, 10)); texture_index_list.Add(new Vector3Int(32, 31, 30)); normals.Add(new Vector3(8, -5, 1));

        faces.Add(new Vector3Int(3, 14, 16)); texture_index_list.Add(new Vector3Int(32, 33, 31)); normals.Add(new Vector3(8, 5, 1));
        faces.Add(new Vector3Int(3, 16, 5)); texture_index_list.Add(new Vector3Int(32, 31, 30)); normals.Add(new Vector3(8, 5, 1));
    }

    List<Vector2> Adjust_to_relative(List<Vector2> pixels, float ResX, float ResY)
    {
        List<Vector2> hold = new List<Vector2>();
        foreach (Vector2 v in pixels)
            hold.Add(new Vector2(v.x/ResX, 1 - v.y/ResY));
        return hold;
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
        List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();

        for (int i = 0; i < faces.Count; i++)
        {
            Vector3 normal_for_face = normals[i];

            normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
