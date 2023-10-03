using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    public HexTileGenerationSettings settings;



    public void RollTileType()
    {
        //tileType = (HexTileGenerationSettings.TileType)Random.Range(0, 3);
    }

    public void AddTile()
    {
        //tile = GameObject.Instantiate(settings.GetTile(tileType));
        //if (gameObject.GetComponent<MeshCollider>() == null)
        //{
        //    MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        //    collider.sharedMesh = GetComponentInChildren<MeshFilter>().mesh;
        //}

        //transform.AddChild(tile);
    }
}
