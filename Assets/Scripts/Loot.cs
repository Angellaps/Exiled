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
        //If loot items touch the player, they get destroyed but added to the playerInventory.
        //Food items are an exception since they apply healing and raise survival stats instantly.
        if (col.collider.Equals(playerCol)) {
            AudioSource.PlayClipAtPoint(lootClip, transform.position);
            if (this.GetComponent<Food>() == null) {
                playerInventory.AddItem(GetComponent<Item>().item, GetComponent<Item>().amount);
            }            
            if(this.GetComponent<Food>() != null) {
                //UI was a bit scuffed. Apparently all UI elements had a VitalStats script?
                //Fixed it with applying changes to all of them instead of trying to find the correct one.
                foreach(VitalStats meme in FindObjectsOfType<VitalStats>()) {
                    meme.Eat(GetComponent<Food>().hungerGainAmount, GetComponent<Food>().thirstGainAmount, GetComponent<Food>().healthGainAmount);
                }                  
            }
            Destroy(this.gameObject);
        }
    }
}
