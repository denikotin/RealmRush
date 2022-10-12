using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    [SerializeField] Vector2Int endCoordinate;

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reachedMap = new Dictionary<Vector2Int, Node>();

    private GridManager gridManager;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    private void Start()
    {
        startNode = gridManager.Grid[startCoordinate];
        endNode= gridManager.Grid[endCoordinate];

    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinate + direction;
            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reachedMap.ContainsKey(neighbor.coordinate) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reachedMap.Add(neighbor.coordinate, neighbor);
                frontier.Enqueue(neighbor);
            }
        }

    }

    private void BreadthFirstSearch()
    {
        frontier.Clear();
        reachedMap.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        reachedMap.Add(startCoordinate, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinate == endCoordinate)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null )
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;

        }

        path.Reverse();

        return path;
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            bool previousState = grid[coordinate].isWalkable;
            grid[coordinate].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinate].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

        }

        return false;
    }

}
