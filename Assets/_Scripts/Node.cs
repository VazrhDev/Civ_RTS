using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node parent;
    public HexRenderer target;
    public HexRenderer destination;
    public HexRenderer origin;

    public int baseCost;
    public int costFromOrigin;
    public int costToDestination;
    public int pathCost;

    public Node(HexRenderer current, HexRenderer origin, HexRenderer destination, int pathCost)
    {
        parent = null;

        this.origin = current;
        this.origin = origin;
        this.destination = destination;

        baseCost = 1;

        this.pathCost = pathCost;
    }

    public int GetCost()
    {
        return pathCost + baseCost + costFromOrigin + costToDestination;
    }

    public void SetParent(Node node)
    {
        this.parent = node;
    }

}
