using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLoot : MonoBehaviour {
    [System.Serializable]
    public class drops {
        public GameObject drop;
        public float chance;
        public int amountMin, amountMax;
    }
    public List<drops> lootTable = new List<drops>();
    Random rand = new Random();

    //On Kill call this to generate and drop loot
    public void GenerateLoot() {
        float lootRNG = Random.Range(0.0f, 100.0f);
        foreach (drops item in lootTable) {
            if (item.chance > lootRNG) {
                int amount = Random.Range(item.amountMin, item.amountMax+1);
                LootDrop(item.drop, amount);
            }
        }
    }

    //Spawn Loot Drops
    void LootDrop(GameObject drop, int amount) {
        for (int i = 0; i < amount; i++) {
            GameObject baggelis = Instantiate(drop, new Vector3(transform.position.x + Random.Range(-0.5f, 1.5f), transform.position.y + Random.Range(0f, 1f), transform.position.z + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
            baggelis.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4f, 4f), Random.Range(2f, 4f), Random.Range(-4f, 4f)), ForceMode.Impulse);
        }
    }
}





