using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphicsPipeline : MonoBehaviour
{
    Model myModel = new Model();
    Texture2D texture;
    StreamWriter sw;
    private float angle;
    Renderer screenRenderer;
    GameObject screen;

    Vector2 a2, b2, c2, a_t, b_t, c_t;
    Vector2 A, A_t, B, B_t;

    public Texture2D texture_file;

    void Start()
    {
        screen = GameObject.CreatePrimitive(PrimitiveType.Plane);
        screen.transform.localScale = new Vector3(2, 2, 2);
        screenRenderer = screen.GetComponent<Renderer>();

        screen.transform.up = -Vector3.forward;

        screenRenderer.material.mainTexture = texture;

        /*

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
        RenderModelToTexture();
    }

    #region Create Model
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
    #endregion

    #region Matrices
    private void Matrices()
    {
        List<Vector4> verts = convertToHomg(myModel.vertices);

        //Rotation Matrix
        Matrix4x4 rotationMatrix = RotationMatrix();
        List<Vector4> imageAfterRotation = Image(verts, rotationMatrix);

        //Scale Matrix
        Matrix4x4 scaleMatrix = ScaleMatrix();
        List<Vector4> imageAfterScale = Image(imageAfterRotation, scaleMatrix);

        //Translation Matrix
        Matrix4x4 translationMatrix = TranslationMatrix();
        List<Vector4> imageAfterTranslation = Image(imageAfterScale, translationMatrix);

        //Single Matrix of Transformations
        Matrix4x4 transformMatrix = MultiplyMatrix(rotationMatrix, scaleMatrix, translationMatrix);
        List<Vector4> imageAfterTransform = Image(verts, transformMatrix);

        //Viewing Matrix
        Matrix4x4 viewingMatrix = ViewingMatrix();
        List<Vector4> imageAfterViewing = Image(imageAfterTranslation, viewingMatrix);

        //Projection Matrix
        Matrix4x4 projectionMatrix = ProjectionMatrix();
        List<Vector4> imageAfterProjection = Image(imageAfterViewing, projectionMatrix);

        //Single Matrix For Everything
        Matrix4x4 everything = MultiplyMatrix(projectionMatrix, viewingMatrix, transformMatrix);
        List<Vector4> finalImage = Image(verts, everything);
    }

    private Matrix4x4 RotationMatrix()
    {
        Vector3 axis = (new Vector3(19, -2, -2)).normalized;
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(
                Vector3.zero,
                Quaternion.AngleAxis(44, axis),
                Vector3.one);
        displayMatrix(rotationMatrix);

        return rotationMatrix;
    }

    private Matrix4x4 ScaleMatrix()
    {
        Matrix4x4 scaleMatrix =
            Matrix4x4.TRS(
                new Vector3(0, 0, 0),
                Quaternion.identity,
                new Vector3(7, 4, 2)
            );
        displayMatrix(scaleMatrix);

        return scaleMatrix;
    }

    private Matrix4x4 TranslationMatrix()
    {
        Matrix4x4 translationMatrix =
            Matrix4x4.TRS(
                new Vector3(-1, 4, -1),
                Quaternion.identity,
                Vector3.one
            );
        displayMatrix(translationMatrix);

        return translationMatrix;
    }

    private Matrix4x4 ViewingMatrix()
    {
        Matrix4x4 viewingMatrix =
            Matrix4x4.LookAt(
                new Vector3(21, 1, 48),
                new Vector3(-2, 7, 2),
                new Vector3(-1, -2, 19)
            );
        displayMatrix(viewingMatrix);

        return viewingMatrix;

    }

    private Matrix4x4 ProjectionMatrix()
    {
        Matrix4x4 projectionMatrix =
            Matrix4x4.Perspective(90, 1, 1, 1000);
        displayMatrix(projectionMatrix);

        return projectionMatrix;

    }

    private Matrix4x4 MultiplyMatrix(Matrix4x4 c, Matrix4x4 b, Matrix4x4 a)
    {
        Matrix4x4 multiplyMatrix =
            c * b * a;
        sw.WriteLine("\nSingle Matrix For Everything");
        displayMatrix(multiplyMatrix);

        return multiplyMatrix;
    }

    private List<Vector4> Image(List<Vector4> image, Matrix4x4 matrix)
    {
        List<Vector4> imageOut =
            applyTransformation(image, matrix);
        sw.WriteLine("\nFinal Image");
        displayImageAfter(imageOut);

        return imageOut;
    }

    private void displayMatrix(Matrix4x4 matrix)
    {
        for (int i = 0; i < 4; i++)
        {
            sw.WriteLine(matrix.GetRow(i).x + " , " + matrix.GetRow(i).y + " , " + matrix.GetRow(i).z + " , " + matrix.GetRow(i).w);
        }
    }

    private void displayImageAfter(List<Vector4> image)
    {
        for (int i = 0; i < image.Count; i++)
        {
            sw.WriteLine(image[i]);
        }
    }
    #endregion

    #region Clipping
    public bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        OutCode startOutCode = new OutCode(start);
        OutCode endOutCode = new OutCode(end);
        OutCode inScreenOutCode = new OutCode(new Vector2(0, 0));

        if (startOutCode + endOutCode == inScreenOutCode)
            return true;
        if (startOutCode * endOutCode != inScreenOutCode)
            return false;

        if (startOutCode == inScreenOutCode)
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

        if (end.x == start.x)
        {
            return edge switch
            {
                0 => new Vector2(start.x, 1), // Up edge
                1 => new Vector2(start.x, -1), // Down edge
                2 => new Vector2(-1, start.y), // Left edge
                3 => new Vector2(1, start.y),  // Right edge
                _ => new Vector2(float.NaN, float.NaN) // No intersection with up or down edges
            };
        }

        if (end.y == start.y)
        {
            return edge switch
            {
                0 => new Vector2(float.NaN, float.NaN), // Up edge (no valid x)
                1 => new Vector2(float.NaN, float.NaN), // Down edge (no valid x)
                _ => new Vector2(float.NaN, float.NaN) // No intersection with left or right edges
            };
        }

        float m = (end.y - start.y) / (end.x - start.x);
        float c = start.y - m * start.x;

        switch (edge)
        {
            case 0: // up
                return new Vector2((1 - c) / m, 1); // y = 1
            case 1: // down
                return new Vector2((-1 - c) / m, -1); // y = -1
            case 2: // left
                return new Vector2(-1, c - m); // left x = -1
            case 3: // right
                return new Vector2(1, m + c); // right x = 1
            default:
                return new Vector2(float.NaN, float.NaN); // Invalid edge
        }
    }
    #endregion

    #region Breshenham
    public List<Vector2Int> Breshenham(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> final = new List<Vector2Int> { start };

        int x = start.x; int y = start.y;
        int dx = end.x - x;
        int dy = end.y - y;

        if (dx < 0) return Breshenham(end, start);
        if (dy < 0) return NegY(Breshenham(NegY(start), NegY(end)));
        if (dy > dx) return SwapXY(Breshenham(SwapXY(start), SwapXY(end)));

        int neg = 2 * (dy - dx);
        int pos = 2 * dy;
        int P = pos - dx;

        while (x < end.x)
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
        List<Vector2Int> swapList = new List<Vector2Int>();

        foreach (Vector2Int v in vector2Ints)
        {
            swapList.Add(SwapXY(v));
        }
        return swapList;
    }

    private Vector2Int SwapXY(Vector2Int V)
    {
        return new Vector2Int(V.y, V.x);
    }

    private List<Vector2Int> NegY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> negatedList = new List<Vector2Int>();

        foreach (Vector2Int v in vector2Ints)
        {
            negatedList.Add(NegY(v));
        }
        return negatedList;
    }

    private Vector2Int NegY(Vector2Int V)
    {
        return new Vector2Int(V.x, -V.y);
    }
    #endregion

    #region Display 2D Texture
    private void RenderModelToTexture()
    {
        Destroy(texture);
        texture = new Texture2D(1024, 1024);
        screenRenderer.material.mainTexture = texture;
        angle += 1;
        Matrix4x4 matrix4X4 = Matrix4x4.TRS(new Vector3(0, 0, -20), Quaternion.AngleAxis(angle, (new Vector3(1, 1, 1)).normalized), Vector3.one);
        Matrix4x4 mrot = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.right), Vector3.one);
        Matrix4x4 superMatrix = mrot * matrix4X4;
        List<Vector4> verts = applyTransformation(convertToHomg(myModel.vertices), matrix4X4);

        for (int i = 0; i < myModel.faces.Count; i++)
        {
            Vector3Int face = myModel.faces[i];
            Vector3Int texture = myModel.texture_index_list[i];

            a_t = myModel.texture_coordinates[texture.x];
            b_t = myModel.texture_coordinates[texture.y];
            c_t = myModel.texture_coordinates[texture.z];

            Vector3 a = verts[face.x];
            Vector3 b = verts[face.y];
            Vector3 c = verts[face.z];

            a2 = pixelize(project(a));
            b2 = pixelize(project(b));
            c2 = pixelize(project(c));

            A = b2 - a2;
            B = c2 - a2;

            A_t = b_t - a_t;
            B_t = c_t - a_t;

            if (Vector3.Cross(b2 - a2, c2 - b2).z < 0)
            {
                EdgeTable edgeTable = new EdgeTable();
                process(verts[face.x], verts[face.y], edgeTable);
                process(verts[face.y], verts[face.z], edgeTable);
                process(verts[face.z], verts[face.x], edgeTable);

                DrawScanLines(edgeTable);
            }
        }
        texture.Apply();
    }

    private void DrawScanLines(EdgeTable edgeTable)
    {
        foreach (var item in edgeTable.edgeTable)
        {
            int y = item.Key;
            int xMin = item.Value.start;
            int xMax = item.Value.end;

            for (int x = xMin; x <= xMax; x++)
            {
                Color color = getColorFromTexture(x, y);
                texture.SetPixel(x, y, color);
            }
        }
    }

    private Color getColorFromTexture(float x_p, int y_p)
    {
        float x = x_p - a2.x;
        float y = y_p - a2.y;

        float r = (x * B.y - y * B.x) / (A.x * B.y - A.y * B.x);
        float s = (A.x * y - x * A.y) / (A.x * B.y - A.y * B.x);

        Vector2 texture_point = a_t + r * A_t + s * B_t;
        texture_point *= 1024;

        Color color = texture_file.GetPixel((int)texture_point.x, (int)texture_point.y);

        return color;
    }

    private void process(Vector4 start4D, Vector4 end4D, EdgeTable edgeTable)
    {
        Vector2 start = project(start4D);
        Vector2 end = project(end4D);

        if (LineClip(ref start, ref end))
        {
            Vector2Int startPix = pixelize(start);
            Vector2Int endPix = pixelize(end);

            List<Vector2Int> points = Breshenham(startPix, endPix);
            edgeTable.Add(points);
        }
    }

    private void setPixels(List<Vector2Int> points)
    {
        foreach (Vector2Int point in points)
        {
            texture.SetPixel(point.x, point.y, Color.red);
        }
    }

    private bool IsVisible(Vector4 vector4)
    {
        return vector4.z < 0;
    }

    private Vector2Int pixelize(Vector2 point)
    {
        return new Vector2Int((int)Mathf.Round((point.x + 1) * 1023 / 2), (int)Mathf.Round((point.y + 1) * 1023 / 2));
    }

    private Vector2 project(Vector4 v)
    {
        return new Vector2(v.x / v.z, v.y / v.z);
    }

    private bool IsBackFace(Vector3 a, Vector3 b)
    {
        return Vector3.Cross(a, b).z > 0;
    }
    #endregion
}
