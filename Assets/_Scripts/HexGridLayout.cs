using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.AssetImporters;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Setting")]
    public Vector2Int gridSize;

    [Header("Tite Setting")]
    [SerializeField] float outerSize = 1f;
    [SerializeField] float innerSize = 0f;
    [SerializeField] float height = 1f;
    [SerializeField] bool isFlatTopped = false;
    [SerializeField] Material hexMaterial;

    [SerializeField] private Color[] tileColor;

    private void OnEnable()
    {
        LayoutGrid();
    }

    private void OnValidate()
    {
# if UNITY_EDITOR
        //if (Application.isPlaying)
        //{
        //    LayoutGrid();
        //}
#endif
    }

    private void LayoutGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                // Remove Random Height Later

                int randomNum = Random.Range(0, 3);

                if (randomNum == 2)
                {
                    height = 0.5f;
                }
                else
                {
                    height = 0.1f;
                }


                GameObject tile = new GameObject($"Hex {x},{y}", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.outerSize = outerSize;
                hexRenderer.innerSize = innerSize;
                hexRenderer.height = height;
                hexRenderer.isFlatTopped = isFlatTopped;
                hexRenderer.material = hexMaterial;
                hexRenderer.tileColor = tileColor[randomNum];

                hexRenderer.SetMaterial();
                hexRenderer.DrawMesh();

                tile.transform.SetParent(transform, true);
            }
        }
    }

    public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        float width;
        float height;
        float xPosition = 0;
        float yPosition = 0;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * 3f / 4f;

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = (column * horizontalDistance) + offset;
            yPosition = row * verticalDistance;
        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3) * size;

            horizontalDistance = width * 3f / 4f;
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;

            xPosition = column * horizontalDistance;
            yPosition = (row * verticalDistance) - offset;
        }

        return new Vector3(xPosition, 0, -yPosition);
    }

}
