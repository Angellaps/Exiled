using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<GameObject> interactablesInRange = new List<GameObject>();
    List<GameObject> enemiesInRange = new List<GameObject>();
    [SerializeField]
    List<GameObject> enemiesInFront = new List<GameObject>();
    public float PlayerDamage = 10.0f;
    [SerializeField]
    Collider PlayerCol;
    Animator PlayerAnim;
    float RotationSpeed = 10.0f;
    float PlayerSpeed = 5f;

    [SerializeField]
    GameObject MainPlayer;
    public Camera cam;
    [SerializeField]
    EnemyMeleeAI MeleeEnemy;
    [SerializeField]
    EnemyRangedScript RangedEnemy;
    //Player stats
    public float PlayerLife;
    float PlayerSanity = 100f;
    float PlayerHunger;
    float PlayerThirst = 100f;
    float AttackTimer = 1.0f;
    //Player Inventory
    public InventorySO playerInventory;
    void Start() {
        //LookTarget = Input.mousePosition
        PlayerAnim = GetComponent<Animator>();
        PlayerLife = 100f;
    }

    private void Update() {
        //if (PlayerLife<=0)
        //{
        //    Destroy(gameObject);
        //}
        AttackTimer += Time.deltaTime;
        StatusSystem(PlayerHunger);
        PlayerMovement();
        LeftClickAction();
        //RightClickAction();
        GatherResources();
        //StatusSystem();
    }

    void PlayerMovement() {
        float XDir = Input.GetAxis("Horizontal");
        float ZDir = Input.GetAxis("Vertical");
        Vector3 MoveDir = new Vector3(XDir, 0.0f, ZDir);
        transform.position += MoveDir * PlayerSpeed * Time.deltaTime;
        if (MoveDir != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveDir), RotationSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && PlayerSpeed != 0) {
            PlayerAnim.SetBool("IsWalking", true);
        }
        else {
            PlayerAnim.SetBool("IsWalking", false);
        }
    }


    void LeftClickAction() {

        //TO DO: Move this to the AXE Scriptable object's left click

        if (Input.GetMouseButtonDown(0) && AttackTimer >= 1.0f) {

            //LookAtPointOfInterest();
            //PlayerSpeed = 0f;
            AttackTimer = 0f;
            AttackTimer += Time.deltaTime;
            //Debug.Log(AttackTimer);
            Debug.Log("Attack Pressed");
            PlayerAnim.SetTrigger("Attack");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Interactable") && InteractableRangeCheck(hitObject)) {
                    transform.LookAt(hitObject.transform);
                    hitObject.GetComponent<Interactables>().GenerateLoot();
                }
                else if (hitObject.CompareTag("Ground"))
                {
                    transform.LookAt(hit.point);
                }
            }
            ///!!!! REWORK ENEMY SCRIPTS TO HAVE A UNIVERSAL TAKE DAMAGE METHOD
            foreach(GameObject enemy in enemiesInFront) {
                transform.LookAt(hit.transform);
                //enemy.TakeDamage();
            }

        }
        else {
            PlayerAnim.SetBool("IsAttacking", false);
            if (AttackTimer >= 1.0f) {
                PlayerSpeed = 5f;
            }
        }
    }
    //Delay the destruction of Interactable to line up with the animation WIP
    IEnumerator WaitAndDestroy(GameObject obj, float time) {
        // suspend execution for 'time' seconds
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    public void StatusSystem(float hunger) {
        PlayerHunger = hunger;
        //Debug.Log(PlayerHunger);
        //Calculate player status such as Life , Hunger , Water , Sanity etc.
    }
    void GatherResources() {
        if (Input.GetKeyDown(KeyCode.E)) {
            PlayerAnim.SetBool("IsGathering", true);
        }
        else {
            PlayerAnim.SetBool("IsGathering", false);
        }
    }

    public void OnTriggerEnter(Collider other) {
        //Add Interactables and enemies in range (vacuum sphere) into lists
        //!!! TO DO: on enemy death, remove it from the list!!!
        if (other.CompareTag("Interactable")) {
            interactablesInRange.Add(other.gameObject);
        }        
        if (other.CompareTag("MeleeEnemy")) {
            enemiesInRange.Add(other.gameObject);            
        }   

        //???????????? afta edo kato tha figoun se dika tous scripts i se scripts twn enemies    
        if (other.gameObject.CompareTag("ThrownRock")) {
            RangedTakeDamage();
            Debug.Log(PlayerLife);
            if (PlayerLife <= 0) {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("MeleeEnemy")) {
            MeleeTakeDamage();
            Debug.Log(PlayerLife);
            if (PlayerLife <= 0) {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other) {

        if (other.CompareTag("Interactable")) {
            interactablesInRange.Remove(other.gameObject);
        }

        if (other.CompareTag("MeleeEnemy")) {
            enemiesInRange.Remove(other.gameObject);            
        }
    }


    public void RangedTakeDamage() {
        PlayerLife -= RangedEnemy.damage;
    }
    public void MeleeTakeDamage() {
        PlayerLife -= MeleeEnemy.damage;
    }
    private void OnApplicationQuit() {
        playerInventory.InventoryContainer.Clear();
    }

    //Returns all the enemies in front of the player (180 degrees)
    public List<GameObject> EnemiesInFront() {
         
        foreach(GameObject enemy in enemiesInRange) {
            if((transform.InverseTransformPoint(enemy.transform.position).z) > 0.0f) {
                enemiesInFront.Add(enemy);
            }            
        }
        return enemiesInFront;
    }

    public bool InteractableRangeCheck(GameObject interactable) {
        if (interactablesInRange.Contains(interactable)) {
            return true;
        }
        return false;
    }
    
}
