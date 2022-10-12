using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();
            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private void FinishPath()
    {
        enemy.PenaltyGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        foreach(Tile waypoint in path)
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
        FinishPath();
    }
}
