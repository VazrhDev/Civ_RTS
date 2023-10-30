using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    private Dictionary<Vector3Int, HexRenderer> tiles;

    private List<HexRenderer> path;

    [SerializeField] private Unit unit;
    private HexRenderer currentNode;
    private Unit selectedUnit;

    public static TileManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        Invoke(nameof(StartGrid), 2f);

    }

    void StartGrid()
    {
        tiles = new Dictionary<Vector3Int, HexRenderer>();

        HexRenderer[] hexRenderers = gameObject.GetComponentsInChildren<HexRenderer>();
        //Registre all tiles
        foreach (HexRenderer hex in hexRenderers)
        {
            RegisterTile(hex);
        }

        //Get each tiles set of neighbours
        foreach (HexRenderer hex in hexRenderers)
        {
            List<HexRenderer> neightbours = GetNeighbours(hex);
            hex.neighbours = neightbours;
        }

        // Put the player somewhere

    }

    public void RegisterTile(HexRenderer _hex)
    {
        tiles.Add(_hex.cubeCoordinate, _hex);
    }

    private List<HexRenderer> GetNeighbours(HexRenderer tile)
    {
        List<HexRenderer> neighbours = new List<HexRenderer>();

        Vector3Int[] neighbourCoords = new Vector3Int[]
        {
            new Vector3Int(1, -1, 0),
            new Vector3Int(1, 0, -1),
            new Vector3Int(0, 1, -1),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(-1, 0, 1),
            new Vector3Int(0, -1, 1),
        };

        foreach (Vector3Int neighbourCoord in neighbourCoords)
        {
            Vector3Int tileCoord = tile.cubeCoordinate;

            if (tiles.TryGetValue(tileCoord + neighbourCoord, out HexRenderer neighbour))
            {
                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

    public void SelectNode(HexRenderer selectedHex)
    {
        //path = Pathfinder.FindPath(currentNode, selectedHex);

        selectedUnit.MoveUnit(selectedHex.transform.position);
    }

    public void SelectUnit(Unit selectedHex)
    {
        selectedUnit = selectedHex;
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            foreach (HexRenderer tile in path)
            {
                Gizmos.DrawCube(tile.transform.position + new Vector3(0f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }
}
