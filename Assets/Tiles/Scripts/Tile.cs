using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower _prefabTower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable { get { return isPlaceable;} }

    GridManager gridManager;
    Pathfinding pathfinding;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinding = FindObjectOfType<Pathfinding>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!IsPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinding.WillBlockPath(coordinates))
        {
            bool isPlaced = _prefabTower.CreateTower(_prefabTower, transform.position);
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinates);
        }
    }
}
