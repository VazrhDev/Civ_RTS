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

    private HexRenderer[,] hexRenderersList;
    public int overallTemp = -5;


    private void OnEnable()
    {
        hexRenderersList = new HexRenderer[gridSize.x, gridSize.y];

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
                hexRenderer.colorNum = randomNum;
                hexRenderer.AddComponent<MeshCollider>();

                //if (x < 8 && y < 5)
                //{
                //    hexRenderer.tileColor = tileColor[3];
                //    hexRenderer.colorNum = 3;
                //}

                //if (x < 14 && x >= 10 && y < 14 && y >= 11)
                //{
                //    hexRenderer.tileColor = tileColor[4];
                //    hexRenderer.colorNum = 4;
                //}

                if (x == 0 && y == 0)
                {
                    hexRenderer.temperature = overallTemp;
                }

                else if (x == 0 && y > 0)
                {
                    HexRenderer lastHex = hexRenderersList[x, y - 1];

                    int chance = hexRenderer.transferRate + lastHex.transferRate;

                    if (chance == 0)
                    {
                        hexRenderer.temperature = lastHex.temperature;
                    }
                    else if (chance < 0)
                    {
                        if (overallTemp >= -5)
                            hexRenderer.temperature = Mathf.Abs(lastHex.temperature) - hexRenderer.temperature;
                        else
                        {
                            hexRenderer.temperature = -5;
                        }

                    }
                    else
                    {
                        overallTemp = overallTemp + Random.Range(0, 10);

                        hexRenderer.temperature = overallTemp;
                    }
                }

                else if (x > 0) 
                {
                    HexRenderer lastHex = hexRenderersList[x - 1, y];

                    //int tempDiff = (hexRenderer.temperature - lastHex.temperature) * (hexRenderer.transferRate + lastHex.transferRate);

                    //hexRenderer.temperature = hexRenderer.temperature - tempDiff;

                    int chance = hexRenderer.transferRate + lastHex.transferRate;

                    if (chance == 0)
                    {
                        hexRenderer.temperature = lastHex.temperature;
                    }
                    else if (chance < 0) 
                    {
                        if (overallTemp >= -5)
                            hexRenderer.temperature = Mathf.Abs(lastHex.temperature) - hexRenderer.temperature;
                        else
                        {
                            hexRenderer.temperature = -5;
                        }
                    }
                    else
                    {
                        overallTemp = overallTemp + Random.Range(0, 10);

                        hexRenderer.temperature = overallTemp;
                    }
                }

                // Upadte hex color

                if (hexRenderer.temperature <= 7)
                {
                    hexRenderer.tileColor = tileColor[3];
                    hexRenderer.colorNum = 3;
                }

                else if (hexRenderer.temperature >= 20)
                {
                    hexRenderer.tileColor = tileColor[4];
                    hexRenderer.colorNum = 4;
                }

                
                hexRenderer.SetMaterial();
                hexRenderer.DrawMesh();

                tile.transform.SetParent(transform, true);

                hexRenderersList[x, y] = hexRenderer;


                hexRenderer.offsetCoordinate = new Vector2Int(x, y);
                hexRenderer.OffsetToCube();
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
