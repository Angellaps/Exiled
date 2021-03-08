using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//not implemented yet aiming to fix tooltips and general raycast usage here.
public class RaycastManager : MonoBehaviour
{
    private GameObject raycastTarget;

    [SerializeField] private float rayLength = 50.0f;
    [SerializeField] private LayerMask layermask;

    [SerializeField] private VitalStats hungerStat;
    [SerializeField] private VitalStats healthStat;
    [SerializeField] private VitalStats thirstStat;

    //[SerializeField] private Image crosshair;
    [SerializeField] private Text itemNameText;

    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, rayLength, layermask.value))
        {

            if (hit.collider.CompareTag("Consumable"))
            {
                CrosshairActive();
                raycastTarget = hit.collider.gameObject;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {

            }
        }
        else
        {
            CrosshairDeactive();
        }

    }

    void CrosshairActive()
    {
        //crosshair.color = Color.red;
    }
    void CrosshairDeactive()
    {
        //crosshair.color = Color.white;
    }
}
