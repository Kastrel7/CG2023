using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    Model myLetter;
    StreamWriter sw;
    // Start is called before the first frame update
    void Start()
    {
        sw = File.CreateText("./output.txt");
        Model myModel = new Model();
        List<Vector4> verts = convertToHomg(myModel.vertices);

        // myModel.CreateUnityGameObject();

        ////////Rotation Matrix////////
        Vector3 axis = (new Vector3(19, -2, -2)).normalized;
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(Vector3.zero,
                        Quaternion.AngleAxis(44, axis),
                        Vector3.one);
        sw.WriteLine("Rotation Matrix");
        displayMatrix(rotationMatrix);

        ////////Image After Rotation////////
        List<Vector4> imageAfterRotation =
            applyTransformation(verts, rotationMatrix);
        sw.WriteLine("\nImage After Rotation");
        displayImageAfter(imageAfterRotation);

        ////////Scale Matrix////////
        Matrix4x4 scaleMatrix =
            Matrix4x4.TRS(new Vector3(0,0,0),
            Quaternion.identity,
            new Vector3(7, 4, 2)
            );
        sw.WriteLine("\nScale Matrix");
        displayMatrix(scaleMatrix);

        ////////Image after Scale////////
        List<Vector4> imageAfterScale =
            applyTransformation(imageAfterRotation, scaleMatrix);
        sw.WriteLine("\nImage After Scale");
        displayImageAfter(imageAfterScale);

        ////////Translation Matrix////////
        Matrix4x4 translationMatrix =
            Matrix4x4.TRS(new Vector3(-1, 4, -1),
            Quaternion.identity,
            Vector3.one);
        sw.WriteLine("\nTranslation Matrix");
        displayMatrix(translationMatrix);

        ////////Image After Translation////////
        List<Vector4> imageAfterTranslation =
            applyTransformation(imageAfterScale, translationMatrix);
        sw.WriteLine("\nImage After Translation");
        displayImageAfter(imageAfterTranslation);

        ////////Single Matrix of Transformations////////
        Matrix4x4 transformMatrix = translationMatrix*scaleMatrix*rotationMatrix;
        sw.WriteLine("\nSingle Matrix of Transformations");
        displayMatrix(transformMatrix);

        ////////Image After Transformations////////
        List<Vector4> imageAfterTransform =
            applyTransformation(verts, transformMatrix);
        sw.WriteLine("\nImage After Transformations");
        displayImageAfter(imageAfterTransform);





        sw.Close();
    }

    private List<Vector4> convertToHomg(List<Vector3> vertices)
    {
        List<Vector4> output = new List<Vector4>();

        foreach (Vector3 v in vertices)
        {
            output.Add(new Vector4(v.x, v.y, v.z, 1.0f));

        }
        return output;

    }

    private List<Vector4> applyTransformation
        (List<Vector4> verts, Matrix4x4 tranformMatrix)
    {
        List<Vector4> output = new List<Vector4>();
        foreach (Vector4 v in verts)
        { output.Add(tranformMatrix * v); }

        return output;

    }

    private void displayMatrix(Matrix4x4 rotationMatrix)
    {
        for (int i = 0; i < 4; i++)
        {
            sw.WriteLine(rotationMatrix.GetRow(i).x + " , " + rotationMatrix.GetRow(i).y + " , " + rotationMatrix.GetRow(i).z + " , " + rotationMatrix.GetRow(i).w);
        }
    }

    private void displayImageAfter(List<Vector4> imageAfter) 
    {
        for(int i = 0;i < imageAfter.Count;i++)
        {
            sw.WriteLine(imageAfter[i]);
        }
    }
    void Update()
    {
        
    }
}
