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
        /*
        sw = File.CreateText("./output.txt");
        Model myModel = new Model();
        List<Vector4> verts = convertToHomg(myModel.vertices);

        //myModel.CreateUnityGameObject();

        
        ////////Rotation Matrix////////
        Vector3 axis = (new Vector3(19, -2, -2)).normalized;
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(
                Vector3.zero,
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
            Matrix4x4.TRS(
                new Vector3(0,0,0),
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
            Matrix4x4.TRS(
                new Vector3(-1, 4, -1),
                Quaternion.identity,
                Vector3.one
            );
        sw.WriteLine("\nTranslation Matrix");
        displayMatrix(translationMatrix);

        ////////Image After Translation////////
        List<Vector4> imageAfterTranslation =
            applyTransformation(imageAfterScale, translationMatrix);
        sw.WriteLine("\nImage After Translation");
        displayImageAfter(imageAfterTranslation);

        ////////Single Matrix of Transformations////////
        Matrix4x4 transformMatrix = 
            translationMatrix*scaleMatrix*rotationMatrix;
        sw.WriteLine("\nSingle Matrix of Transformations");
        displayMatrix(transformMatrix);

        ////////Image After Transformations////////
        List<Vector4> imageAfterTransform =
            applyTransformation(verts, transformMatrix);
        sw.WriteLine("\nImage After Transformations");
        displayImageAfter(imageAfterTransform);

        ////////Viewing Matrix////////
        Matrix4x4 viewingMatrix =
            Matrix4x4.LookAt(
                new Vector3(21, 1, 48),
                new Vector3(-2, 7, 2),
                new Vector3(-1, -2, 19)
            );
        sw.WriteLine("\nViewing Matrix");
        displayMatrix(viewingMatrix);

        ////////Image After Viewing Matrix////////
        List<Vector4> imageAfterViewing = 
            applyTransformation(imageAfterTranslation, viewingMatrix);
        sw.WriteLine("\nImage After Viewing Matrix");
        displayImageAfter(imageAfterViewing);

        ////////Projection Matrix////////
        Matrix4x4 projectionMatrix =
            Matrix4x4.Perspective(90, 1, 1, 1000);
        sw.WriteLine("\nProjection Matrix");
        displayMatrix(projectionMatrix);

        ////////Image After Projection////////
        List<Vector4> imageAfterProjection =
            applyTransformation(imageAfterViewing, projectionMatrix);
        sw.WriteLine("\nImage After Projection");
        displayImageAfter(imageAfterProjection);

        ////////Single Matrix For Everything////////
        Matrix4x4 singleMatrix =
            projectionMatrix * viewingMatrix * transformMatrix;
        sw.WriteLine("\nSingle Matrix For Everything");
        displayMatrix(singleMatrix);

        ////////Final Image////////
        List<Vector4> finalImage =
            applyTransformation(verts, singleMatrix);
        sw.WriteLine("\nFinal Image");
        displayImageAfter(finalImage);
        
        sw.Close();
        

        OutCode outCode1 = new OutCode(new Vector2(0, 0));
        outCode1.DisplayOutCode();

        OutCode outCode2 = new OutCode(new Vector2(-1.5f, 1));
        outCode2.DisplayOutCode();

        OutCode outCode3 = new OutCode(new Vector2(1.5f, 1));
        outCode3.DisplayOutCode();
        

        OutCode o01 = new OutCode(new Vector2(-2, -2));
        o01.DisplayOutCode();
        OutCode o02 = new OutCode(new Vector2(2, 0));
        o02.DisplayOutCode();
        (o01 + o02).DisplayOutCode();
        (o01 * o02).DisplayOutCode();
        print(o01 == o02);
        print(o01 != o02);
        
        print(Intersect(new Vector2(-1.5f, 0.5f), new Vector2(0.5f, -1.5f), 0));
        print(Intersect(new Vector2(-1.5f, 0.5f), new Vector2(0.5f, -1.5f), 1));
        print(Intersect(new Vector2(-1.5f, 0.5f), new Vector2(0.5f, -1.5f), 2));
        print(Intersect(new Vector2(-1.5f, 0.5f), new Vector2(0.5f, -1.5f), 3));
        


        Vector2 start = new Vector2(5, 3);
        Vector2 end = new Vector2(7, 8);
        if (LineClip(ref start, ref end))
        {
            print("Accepted");
            print(start.ToString() + end.ToString());
        }
        else
            print("Rejected");
        */

        //print(Breshenham(new Vector2Int(34, 12), new Vector2Int(45, 56)).ToString());

        printBresh(Breshenham(new Vector2Int(12, 31), new Vector2Int(20, 35)));
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

    public bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        OutCode startOutCode = new OutCode(start);
        OutCode endOutCode = new OutCode(end);
        OutCode inScreenOutCode = new OutCode(new Vector2(0, 0));

        if (startOutCode + endOutCode == inScreenOutCode)
            return true;
        if (startOutCode * endOutCode != inScreenOutCode)
            return false;

        if(startOutCode == inScreenOutCode)
            return LineClip(ref end, ref start);

        if (startOutCode.up) 
        { 
            start = Intersect(start, end, 0);
            return LineClip(ref start, ref end);
        }
        if (startOutCode.down)
        {
            start = Intersect(start, end, 1);
            return LineClip(ref start, ref end);
        }
        if (startOutCode.left)
        {
            start = Intersect(start, end, 2);
            return LineClip(ref start, ref end);
        }
        if (startOutCode.right)
        {
            start = Intersect(start, end, 3);
            return LineClip(ref start, ref end);
        }



        return false;
    }

    public Vector2 Intersect(Vector2 start, Vector2 end, int edge)
    {
        float m = (end.y - start.y) / (end.x - start.x);
        float c = start.y - (m*start.x);
        switch (edge)
        {
            case 0: //up y = 1
                return new Vector2((1 - c) / m, 1);
            case 1: //down y = -1
                return new Vector2((-1 - c) / m, -1);
            case 2: //left x = -1
                return new Vector2(-1, (m * -1) + c);
            default: //right x = 1
                return new Vector2(1, (m * 1) + c);
        }

    }

    public List<Vector2Int> Breshenham(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> final = new List<Vector2Int> {start};
        int x = start.x; int y = start.y;
        int dx = end.x - start.x;
        if(dx < 0) 
            return Breshenham(end, start);
        int dy = end.y - start.y;
        if (dy < 0)
        {
            return NegY(Breshenham(NegY(start), NegY(end)));
        }
        int m = dy/dx;
        int neg = 2 * (dy - dx);
        int pos = 2 * dy;
        int P = pos - dx;

        /*
        if (m < 0) 
        { 
            Breshenham(new Vector2Int(start.x, -(start.y)), new Vector2Int(end.x, -(end.y)));
        }
        */

        while (true)
        {
            x++;
            if (P <= 0)
                P += pos;
            else
            {
                y++;
                P += neg;
            }

            final.Add(new Vector2Int(x, y));

            if (x == end.x)
                break;
        }
        return final;
    }

    private List<Vector2Int> NegY(List<Vector2Int> vector2Ints)
    {
        throw new NotImplementedException();
    }

    private Vector2Int NegY(Vector2Int V)
    {
        
    }

    public void printBresh(List<Vector2Int> bresh)
    {
        for (int i = 0; i < bresh.Count; i++)
            print(bresh[i]);
    }
}
