using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float damage = 10.0f;
    Player PlayerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerLife -= damage;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Interactable"))
        {
            Destroy(gameObject);
        }
    }
}
