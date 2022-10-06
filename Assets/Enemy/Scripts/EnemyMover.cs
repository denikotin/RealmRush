using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float percentageMovement = 0f;
            transform.LookAt(endPosition);

            while(percentageMovement < 1f)
            {
                percentageMovement  += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, percentageMovement);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
