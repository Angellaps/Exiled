using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject RangedEnemy, MeleeEnemy;
    [SerializeField]
    Transform EnemySpawner;
    
    void Start()
    {
        Instantiate(MeleeEnemy, EnemySpawner);
        Instantiate(RangedEnemy, EnemySpawner);
    }
}
