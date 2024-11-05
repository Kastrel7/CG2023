using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphicsPipeline : MonoBehaviour
{
    Model myModel = new Model();
    StreamWriter sw;
    Texture2D texture;
    private float angle;
    Renderer screenRenderer;
    GameObject screen;

    void Start()
    {
        //texture = new Texture2D(1024, 1024);
        screen = GameObject.CreatePrimitive(PrimitiveType.Plane);
        screen.transform.localScale = new Vector3(2,2,2);
        screenRenderer = screen.GetComponent<Renderer>();
        screen.transform.up = -Vector3.forward;
        screenRenderer.material.mainTexture = texture;

        /*
        sw = File.CreateText("./output.txt");
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
    }

    private void Update()
    {
        Destroy(texture);
        texture = new Texture2D(1024, 1024);
        screenRenderer.material.mainTexture = texture;
        angle += 1;
        Matrix4x4 M = Matrix4x4.TRS(new Vector3(0, 0, -10), Quaternion.AngleAxis(angle, Vector3.up), Vector3.one);

        /*
        Matrix4x4 mrot = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.right), Vector3.one);
        Matrix4x4 superMatrix = mrot * M;
        List<Vector4> verts = applyTransformation(convertToHomg(myModel.vertices), superMatrix);

        foreach (Vector3Int face in myModel.faces)
        {
            if (IsVisible(verts[face.x]) && IsVisible(verts[face.y]))
                process(verts[face.x], verts[face.y]);
            if (IsVisible(verts[face.y]) && IsVisible(verts[face.z]))
                process(verts[face.y], verts[face.x]);
            if (IsVisible(verts[face.z]) && IsVisible(verts[face.x]))
                process(verts[face.z], verts[face.x]);
        }*/

        
        List<Vector4> newVerts = applyTransformation(convertToHomg(myModel.vertices), M);
        foreach (Vector3Int face in myModel.faces)
        {
            process(newVerts[face.x], newVerts[face.y]);
            process(newVerts[face.y], newVerts[face.z]);
            process(newVerts[face.z], newVerts[face.x]);
        }

        texture.Apply();
    }

    private void process(Vector4 start4D, Vector4 end4D)
    {
        Vector2 start = project(start4D);
        Vector2 end = project(end4D);

        if(LineClip(ref start, ref end))
        {
            Vector2Int startPix = pixelize(start);
            Vector2Int endPix = pixelize(end);
            List<Vector2Int> points = Breshenham(startPix, endPix);
            setPixels(points);
        }
    }

    private Vector2 project(Vector4 v)
    {
        return new Vector2(v.x / v.z, v.y / v.z);
    }

    private bool IsVisible(Vector4 vector4)
    {
        return vector4.z > 0;
    }

    private Vector2Int pixelize(Vector2 point)
    {
        return new Vector2Int((int)Mathf.Round((point.x + 1) * 1023 / 2), (int)Mathf.Round((point.y + 1) * 1023 / 2));
    }

    private void setPixels(List<Vector2Int> points)
    {
        foreach (Vector2Int v in points)
            texture.SetPixel(v.x, v.y, Color.red);
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

    private List<Vector4> applyTransformation(List<Vector4> verts, Matrix4x4 tranformMatrix)
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
        float m, c;
        switch (edge)
        {  
            case 0: //up y = 1
                if (start.x != end.x)
                {
                    m = (end.y - start.y) / (end.x - start.x);
                    c = start.y - (m * start.x);
                    return new Vector2((1 - c) / m, 1);
                }
                return new Vector2(start.x, 1);
            case 1: //down y = -1
                if (start.x != end.x)
                {
                    m = (end.y - start.y) / (end.x - start.x);
                    c = start.y - (m * start.x);
                    return new Vector2((-1 - c) / m, -1);
                }
                return new Vector2(start.x, -1);
            case 2: //left x = -1
                m = (end.y - start.y) / (end.x - start.x);
                c = start.y - (m * start.x);
                return new Vector2(-1, (m * -1) + c);
            default: //right x = 1
                m = (end.y - start.y) / (end.x - start.x);
                c = start.y - (m * start.x);
                return new Vector2(1, (m * 1) + c);
        }
    }



    public List<Vector2Int> Breshenham(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> final = new List<Vector2Int> {start};

        int x = start.x; int y = start.y;
        int dx = end.x - start.x;
        int dy = end.y - start.y;

        if (dx < 0) 
            return Breshenham(end, start);

        if (dy < 0)
            return NegY(Breshenham(NegY(start), NegY(end)));

        if (dy > dx)
            return SwapXY(Breshenham(SwapXY(start), SwapXY(end)));

        //int m = dy / dx;
        int neg = 2 * (dy - dx);
        int pos = 2 * dy;
        int P = pos - dx;

        /*
        if (m < 0) 
        { 
            Breshenham(new Vector2Int(start.x, -(start.y)), new Vector2Int(end.x, -(end.y)));
        }
        */

        while (x<end.x)
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

         
        }

        return final;
    }

    public void displayBresh(List<Vector2Int> bresh)
    {
        for (int i = 0; i < bresh.Count; i++)
            print(bresh[i]);
    }



    private List<Vector2Int> SwapXY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> output = new List<Vector2Int>();

        foreach (Vector2Int v in vector2Ints)
        { 
            output.Add(SwapXY(v)); 
        }
        return output;
    }

    private Vector2Int SwapXY(Vector2Int V)
    {
        return new Vector2Int(V.y, V.x);
    }

    private List<Vector2Int> NegY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> output = new List<Vector2Int>();

        foreach (Vector2Int v in vector2Ints)
        {
            output.Add(NegY(v));
        }
        return output;
    }

    private Vector2Int NegY(Vector2Int V)
    {
        return new Vector2Int(V.x, -V.y);
    }



    public void drawLine(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> points = Breshenham(start, end);

        GameObject screen = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Renderer screenRenderer = screen.GetComponent<Renderer>();
        screen.transform.up = -Vector3.forward;

        Texture2D screenTexture = new Texture2D(1024, 1024);

        screenRenderer.material.mainTexture = screenTexture;

        foreach (Vector2Int point in points)
        {
            screenTexture.SetPixel(point.x, point.y, Color.red);
        }

        screenTexture.Apply();
    }
}
