using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour {

    public float radius = 3f;

    [SerializeField]
    private Outline outline;
    public int nodeHP;

    [System.Serializable]
    public class drops {
        public GameObject drop;
        public float chance;
        public int amount;
        public float finalHitChance;
        public int finalHitAmount;
    }

    public List<drops> lootTable = new List<drops>();

    public virtual void InteractAction() {
        // This method will be overwritten by the InteractAction() method in the Scriptable objects        
    }

    public void GenerateLoot() {
        nodeHP--;
        if (nodeHP >= 1) {
            float RNG = Random.Range(0.0f, 100.0f);
            foreach (drops item in lootTable) {
                if (item.chance > RNG) {
                    LootDrop(item.drop, item.amount);
                }
            }
        }
        else {
            foreach (drops item in lootTable) {
                float RNG = Random.Range(0.0f, 100.0f);
                if (item.finalHitChance > RNG) {
                    LootDrop(item.drop, item.finalHitAmount);
                }
            }
            Destroy(this.gameObject);
        }
    }

    //Spawn Loot Drops
    void LootDrop(GameObject drop, int amount) {
        for (int i = 0; i < amount; i++) {
            GameObject baggelis = Instantiate(drop, new Vector3(transform.position.x + Random.Range(-0.5f, 1.5f), transform.position.y + Random.Range(0f, 1f), transform.position.z + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
            baggelis.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4f, 4f), Random.Range(2f, 4f), Random.Range(-4f, 4f)), ForceMode.Impulse);
        }
    }

    //Outline on Mouseover
    private void OnMouseEnter() {
        if (InteractableRangeCheck(gameObject)) {           
            outline.enabled = true;
        }
    }

    private void OnMouseOver() {
        if (InteractableRangeCheck(gameObject) && outline.enabled.Equals(false)){
            outline.enabled = true;
        }
    }

    private void OnMouseExit() {        
            outline.enabled = false;        
    }

    public bool InteractableRangeCheck(GameObject interactable) {
        if (GameObject.Find("Character").GetComponent<Player>().interactablesInRange.Contains(interactable)) {
            return true;
        }
        return false;        
    }
}