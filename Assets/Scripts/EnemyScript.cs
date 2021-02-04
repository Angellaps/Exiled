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

    // Start is called before the first frame update
    void Start()
    {    
        EnemyLife = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {        
     
    }

    public void TakeDamage()
    {
        EnemyLife -= player.PlayerDamage;
        Debug.Log("Enemy took some damage!");
        if (EnemyLife<=0)
        {
            Monsterloot.GenerateLoot();
            Destroy(gameObject);
        }
    }
}
