using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject _prefabTower;
    [SerializeField] bool isPlaceable;

    private void OnMouseDown()
    {
        if(isPlaceable)
        {
            var instance = Instantiate(_prefabTower, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
