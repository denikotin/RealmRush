using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHintPoint = 5;
    [Tooltip("Adds amount to max hitpoint when enemy dies")]
    [SerializeField] int difficultyRamp = 1;
    private int currentHitPoints = 0;

    Enemy enemy;

    private void OnEnable()
    {
        currentHitPoints = maxHintPoint;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
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
            gameObject.SetActive(false);
            maxHintPoint += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
