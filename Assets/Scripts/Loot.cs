using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    [SerializeField]
    GameObject player;
    BoxCollider playerCol;
    [SerializeField]
    public InventorySO playerInventory;
    [SerializeField]
    private AudioClip lootClip;


    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCol = player.GetComponent<BoxCollider>();        
    }


    void OnCollisionEnter(Collision col) {
        if (col.collider.Equals(playerCol)) {
            AudioSource.PlayClipAtPoint(lootClip, transform.position);
            playerInventory.AddItem(GetComponent<Item>().item, GetComponent<Item>().amount);
            //fix for now cause the UI scripts suck ASS
            if(this.GetComponent<Food>() != null) {                
                foreach(VitalStats meme in FindObjectsOfType<VitalStats>()) {
                    meme.Eat(GetComponent<Food>().hungerGainAmount, GetComponent<Food>().thirstGainAmount);
                }                  
            }
            Destroy(this.gameObject);
        }
    }
}
