using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color blockedColor;
    [SerializeField] Color exploredColor;
    [SerializeField] Color pathColor;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        defaultColor = Color.white;
        blockedColor = Color.grey;
        exploredColor = Color.blue;
        pathColor = Color.yellow;

        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = true;
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColor();
        ToggleLabels();
        
    }

    private void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }

    private void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = $"{coordinates.x} , {coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
