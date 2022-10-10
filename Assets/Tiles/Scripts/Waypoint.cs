using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower _prefabTower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable { get { return isPlaceable;} }

    private void OnMouseDown()
    {
        if(isPlaceable)
        {
            bool isPlaced = _prefabTower.CreateTower(_prefabTower, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
