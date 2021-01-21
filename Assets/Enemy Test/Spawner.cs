using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject FoundEnemyMelee,FoundRangedEnemy;
    [SerializeField]
    GameObject RangedEnemy, MeleeEnemy;
    [SerializeField]
    Transform EnemySpawner;

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Instantiate(MeleeEnemy, EnemySpawner);
        }
        if (Input.GetKeyDown("2"))
        {
            Instantiate(RangedEnemy, EnemySpawner);
        }
        if (Input.GetKey("3"))
        {
          FoundEnemyMelee = GameObject.FindGameObjectWithTag("MeleeEnemy");
            if (FoundEnemyMelee!=null)
            {
                FoundEnemyMelee.SetActive(false);
            }
          FoundRangedEnemy = GameObject.FindGameObjectWithTag("RangedEnemy");
            if (FoundRangedEnemy!=null)
            {
                FoundRangedEnemy.SetActive(false);
            }
        }
    }
}
