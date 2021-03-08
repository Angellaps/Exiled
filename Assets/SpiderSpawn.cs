using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject RangedEnemy, MeleeEnemy;
    [SerializeField]
    Transform EnemySpawner;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(MeleeEnemy, EnemySpawner);
        Instantiate(RangedEnemy, EnemySpawner);
    }

    
}
