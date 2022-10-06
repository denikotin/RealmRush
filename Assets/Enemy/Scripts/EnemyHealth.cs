using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHintPoint = 5;
    public int currentHitPoints = 0;

    private void Start()
    {
        currentHitPoints = maxHintPoint;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;
        if(currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
