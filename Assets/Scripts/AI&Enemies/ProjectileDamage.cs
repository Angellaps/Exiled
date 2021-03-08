using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float damage = 10.0f;
    Player PlayerScript;

    //Simple Script that handles the damage of the thrown rock of ranged spiders and destroys them when they hit something
    //so they don't stay in scene when not needed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerLife -= damage;
            other.GetComponent<VitalStats>().hpCurrentAmount -= damage;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Interactable"))
        {
            Destroy(gameObject);
        }
    }
}
