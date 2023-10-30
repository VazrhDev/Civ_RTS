using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> _vertices, List<int> _triangles, List<Vector2> _uvs)
    {
        vertices = _vertices;
        triangles = _triangles;
        uvs = _uvs;
    }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<Face> faces;

    public Material material;

    public float innerSize;
    public float outerSize;
    public float height;
    public bool isFlatTopped;

    public Color tileColor;
    public int colorNum;
    public int temperature;
    public int transferRate;

    public Vector2Int offsetCoordinate;
    public Vector3Int cubeCoordinate;

    public List<HexRenderer> neighbours;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        mesh = new Mesh();
        mesh.name = "Hex";

        meshFilter.mesh = mesh;
        meshRenderer.material = material;

        temperature = Random.Range(-1, 1);
        transferRate = Random.Range(-1, 1);
    }

    private void OnEnable()
    {
        //DrawMesh();
    }

    public void OnValidate()
    {
        //if (Application.isPlaying && mesh != null)
        //{
        //    DrawMesh();
        //}
    }

    public void SetMaterial()
    {
        meshRenderer.material = material;

        meshRenderer.material.color = tileColor;
    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    private void DrawFaces()
    {
        faces = new List<Face>();

        // Top Faces
        for (int point = 0; point < 6; point++)
        {
            faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        }

        // Bottom Faces
        for (int point = 0; point < 6; point++)
        {
            faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
        }

        // Outer Faces
        for (int point = 0; point < 6; point++)
        {
            faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
        }

        // inner Faces
        for (int point = 0; point < 6; point++)
        {
            faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point, false));
        }
    }

    private Face CreateFace(float _innerRad, float _outerRad, float _heightA, float _heightB, int _point, bool _reverse = false)
    {
        Vector3 pointA = GetPoint(_innerRad, _heightB, _point);
        Vector3 pointB = GetPoint(_innerRad, _heightB, (_point < 5) ? _point + 1 : 0);
        Vector3 pointC = GetPoint(_outerRad, _heightA, (_point < 5) ? _point + 1 : 0);
        Vector3 pointD = GetPoint(_outerRad, _heightA, _point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (_reverse)
            vertices.Reverse();

        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float _size, float _height, int _index)
    {
        float angle_deg = isFlatTopped ? 60 * _index : 60 * _index - 30;
        float angle_rad = Mathf.PI / 180f * angle_deg;

        return new Vector3((_size * Mathf.Cos(angle_rad)), _height, _size * Mathf.Sin(angle_rad));
    }


    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < faces.Count; i++)
        {
            // Add the Vertices
            vertices.AddRange(faces[i].vertices);
            uvs.AddRange(faces[i].uvs);

            // Offset the triangles
            int offset = (4 * i);
            foreach (int triangle in faces[i].triangles)
            {
                tris.Add(triangle + offset);
            }
        }


        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
    }


    public void OffsetToCube()
    {
        Vector2Int offset = offsetCoordinate;
        var q = offset.x - (offset.y + (offset.y % 2)) / 2;
        var r = offset.y;

        cubeCoordinate = new Vector3Int(q, r, -q - r);
    }

    private bool isSelected = false;

    public void SelectNode()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            meshRenderer.material.color = Color.white;
        }
        else
        {
            meshRenderer.material.color = tileColor;
        }

        DrawMesh();

        TileManager.Instance.SelectNode(this);
    }

    private void OnDrawGizmosSelected()
    {
        foreach (HexRenderer neighbour in neighbours)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, neighbour.transform.position);
        }
    }
}
