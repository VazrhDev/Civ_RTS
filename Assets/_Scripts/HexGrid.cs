using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HexGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public float radius = 1f;
    public bool isFlatTopped;

    public HexTileGenerationSettings settings;

    public void Clear()
    {
        List<GameObject> children = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
    }

    //[EditorButton("Layout")]
    public void LayoutGrid()
    {
        Clear();

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject title = new GameObject($"Hex C{x},R{y}");
                HexTile hextile = title.AddComponent<HexTile>();
                hextile.settings = settings;
                hextile.RollTileType();
                hextile.AddTile();
            }
        }
    }
}

[CustomEditor(typeof(HexGrid))]
public class customInspectorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HexGrid hgrid = (HexGrid)target;
        if (GUILayout.Button("Layout"))
        {
            hgrid.LayoutGrid();
        }
    }
}
