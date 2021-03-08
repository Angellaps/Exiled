using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    MonsterLoot Monsterloot;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject Player;
    public float EnemyLife;
    public float damage = 15f;

   
    void Start()
    {    
        EnemyLife = 20.0f;
    }

    //Handles the damage of the enemy
    public void TakeDamage()
    {
        EnemyLife -= player.PlayerDamage;
        Debug.Log("Enemy took some damage!");
        if (EnemyLife<=0)
        {
            Monsterloot.GenerateLoot();
            player.enemiesInRange.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}
