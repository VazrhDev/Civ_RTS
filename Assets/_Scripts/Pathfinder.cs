using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static List<HexRenderer> FindPath(HexRenderer origin, HexRenderer destination)
    {
        Dictionary<HexRenderer, Node> nodesNotEvaluated = new Dictionary<HexRenderer, Node>();
        Dictionary<HexRenderer, Node> nodesAlreadyEvaluated = new Dictionary<HexRenderer, Node>();

        Node startNode = new Node(origin, origin, destination, 0);
        nodesNotEvaluated.Add(origin, startNode);

        bool gotPath = EvaluateNextNode(nodesNotEvaluated, nodesAlreadyEvaluated, origin, destination, out List<HexRenderer> path);

        while (!gotPath)
        {
            gotPath = EvaluateNextNode(nodesNotEvaluated, nodesAlreadyEvaluated, origin, destination, out path);
        }

        return path;
    }

    private static bool EvaluateNextNode(Dictionary<HexRenderer, Node> nodesNotEvaluated, Dictionary<HexRenderer, Node> nodesEvaluated, HexRenderer origin, HexRenderer destination, out List<HexRenderer> path)
    {
        Node currentNode = GetCheapestNode(nodesNotEvaluated.Values.ToArray());

        if (currentNode == null)
        {
            path = new List<HexRenderer>();
            return false;
        }

        nodesNotEvaluated.Remove(currentNode.target);
        nodesEvaluated.Add(currentNode.target, currentNode);

        path = new List<HexRenderer>();

        if (currentNode.target == destination)
        {
            path.Add(currentNode.target);

            while (currentNode.target != origin)
            {
                path.Add(currentNode.parent.target);
                currentNode = currentNode.parent;
            }

            return true;
        }

        List<Node> neighbours = new List<Node>();
        foreach (HexRenderer tile in currentNode.target.neighbours)
        {
            Node node = new Node(tile, origin, destination, currentNode.GetCost());

            neighbours.Add(node);   
        }

        foreach (Node neighbour in neighbours)
        {
            if (nodesEvaluated.Keys.Contains(neighbour.target))
            {
                continue;
            }

            if (neighbour.GetCost() < currentNode.GetCost() || !nodesNotEvaluated.Keys.Contains(neighbour.target))
            {
                neighbour.SetParent(currentNode);
                if (!nodesNotEvaluated.Keys.Contains(neighbour.target))
                {
                    nodesNotEvaluated.Add(neighbour.target, neighbour);
                }
            }
        }

        return false;
    }

    private static Node GetCheapestNode(Node[] nodesNotEvaluated)
    {
        if (nodesNotEvaluated.Length == 0)
            return null;

        Node selectedNode = nodesNotEvaluated[0];

        for (int i = 1; i < nodesNotEvaluated.Length; i++)
        {
            var currentNode = nodesNotEvaluated[i];

            if (currentNode.GetCost() < selectedNode.GetCost())
            {
                selectedNode = currentNode;
            }
            else if (currentNode.GetCost() == selectedNode.GetCost() && currentNode.costToDestination < selectedNode.costToDestination)
            {
                selectedNode = currentNode;
            }
        }

        return selectedNode;
    }

}
