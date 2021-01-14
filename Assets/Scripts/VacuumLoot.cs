using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumLoot : MonoBehaviour
{
    public GameObject PullOBJ;
    public float ForceSpeed;
    //float VacuumOffset = 0.5f;
    public void OnTriggerStay(Collider coll) 
    {

        if (coll.gameObject.tag == ("Loot")) 
        {
            PullOBJ = coll.gameObject;
            PullOBJ.transform.position = Vector3.MoveTowards
                (PullOBJ.transform.position,
                 transform.position,
                 ForceSpeed * Time.deltaTime);
            //if(Vector3.Distance(PullOBJ.transform.position,transform.position)<=VacuumOffset) 
            //{
            //    Loot(PullOBJ);
            //    //Destroy(PullOBJ);
            //}
        }     
    }
  

    //public void Loot(GameObject loot) 
    //{
    //    //Implement later: Add whatever the player has looted to his inventory/currency
    //}

    
}
