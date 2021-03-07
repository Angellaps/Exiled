using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IDamage
{
    public List<GameObject> interactablesInRange = new List<GameObject>();
    public List<GameObject> consumablesInRange = new List<GameObject>();
    List<GameObject> enemiesInRange = new List<GameObject>();
    [SerializeField]
    List<GameObject> enemiesInFront = new List<GameObject>();
    public float PlayerDamage = 10.0f;
    [SerializeField]
    Collider PlayerCol;
    public Animator PlayerAnim;
    float RotationSpeed = 10.0f;
    float PlayerSpeed = 5f;
    public delegate void EndGame();
    public static event EndGame endGame;

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
    float PlayerThirst;
    float AttackTimer = 1.0f;
    //Player Inventory
    public InventorySO playerInventory;
    private Sleep sleepDone;

    private UIManager manager;
    private void Awake()
    {
        manager = GameObject.FindObjectOfType<UIManager>();
        sleepDone = GameObject.FindObjectOfType<Sleep>();
    }
    void Start()
    {
        //LookTarget = Input.mousePosition
        PlayerAnim = GetComponent<Animator>();
        PlayerLife = 100f;
    }

    private void Update()
    {
        AttackTimer += Time.deltaTime;
        //StatusSystem(PlayerHunger, PlayerThirst,PlayerLife);
        PlayerMovement();
        LeftClickAction();
        //RightClickAction();
        //GatherResources();
        //StatusSystem();

        //DEATH CHECK
        if (PlayerLife <= 0) {
            PlayerDiedEndGame();
        }
    }

    void PlayerMovement()
    {
        if (sleepDone.canmove == true)
        {
            float XDir = Input.GetAxis("Horizontal");
            float ZDir = Input.GetAxis("Vertical");
            Vector3 MoveDir = new Vector3(XDir, 0.0f, ZDir);
            transform.position += MoveDir * PlayerSpeed * Time.deltaTime;
            if (MoveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveDir), RotationSpeed * Time.deltaTime);
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && PlayerSpeed != 0)
            {
                PlayerAnim.SetBool("IsWalking", true);
            }
            else
            {
                PlayerAnim.SetBool("IsWalking", false);
            }
        }
    }


    void LeftClickAction()
    {

        //TO DO: Move this to the AXE Scriptable object's left click

        if (Input.GetMouseButtonDown(0) && AttackTimer >= 1.0f)
        {

            //LookAtPointOfInterest();
            //PlayerSpeed = 0f;
            AttackTimer = 0f;
            AttackTimer += Time.deltaTime;
            //Debug.Log(AttackTimer);
            Debug.Log("Attack Pressed");
            PlayerAnim.SetTrigger("Attack");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Interactable") && InteractableRangeCheck(hitObject))
                {
                    transform.LookAt(hitObject.transform);
                    hitObject.GetComponent<Interactables>().GenerateLoot();
                }
                else if (hitObject.CompareTag("Ground"))
                {
                    transform.LookAt(hit.point);
                }
            }
            ///!!!! REWORK ENEMY SCRIPTS TO HAVE A UNIVERSAL TAKE DAMAGE METHOD
            foreach (GameObject enemy in EnemiesInFront(enemiesInRange))
            {
                
                //Placeholder mexri na katharisei ta scripts twn enemies o thodoris
                if (enemy.GetComponent<EnemyScript>()!= null)
                {
                    Debug.Log("I dealt damage");                    
                    enemy.GetComponent<EnemyScript>().TakeDamage();
                    //enemy.GetComponent<EnemyMeleeAI>().TakeDamage();
                }
                
            }

        }
        else
        {
            PlayerAnim.SetBool("IsAttacking", false);
            if (AttackTimer >= 1.0f)
            {
                PlayerSpeed = 5f;
            }
        }
    }

    //Delay the destruction of Interactable to line up with the animation WIP
    IEnumerator WaitAndDestroy(GameObject obj, float time)
    {
        // suspend execution for 'time' seconds
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    //public void StatusSystem(float hunger, float thirst,float hp)
    //{
    //    PlayerHunger = hunger;
    //    PlayerThirst = thirst;
    //    PlayerLife = hp;//untested
    //    //Debug.Log(PlayerHunger);
    //    //Calculate player status such as Life , Hunger , Water , Sanity etc.
    //}

    //void GatherResources()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        //transform.LookAt(hitObject.transform);
    //        PlayerAnim.SetBool("IsGathering", true);
    //        Debug.Log("+20");
    //        PlayerHunger += 20.0f;
    //    }
    //    //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    //RaycastHit hit;
    //    /*if (Physics.Raycast(ray, out hit, 100))
    //    {
    //        GameObject hitObject = hit.transform.gameObject;
    //        if (hitObject.CompareTag("Consumable") && ConsumableRangeCheck(hitObject))
    //        {
    //            transform.LookAt(hitObject.transform);
    //            PlayerAnim.SetBool("IsGathering", true);
    //            PlayerHunger += 20.0f;
    //            //TODO : specific interactions with specific items, food interface needs 2 be done

    //        }
    //    }*/
    //    else
    //    {
    //        PlayerAnim.SetBool("IsGathering", false);
    //    }

    //}

    public void OnTriggerEnter(Collider other)
    {
        //Add Interactables and enemies in range (vacuum sphere) into lists
        //!!! TO DO: on enemy death, remove it from the list!!!
        if (other.CompareTag("Interactable"))
        {
            interactablesInRange.Add(other.gameObject);
        }
        if (other.CompareTag("MeleeEnemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
        if (other.CompareTag("RangedEnemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
        if (other.CompareTag("Consumable"))
        {
            consumablesInRange.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Interactable"))
        {
            interactablesInRange.Remove(other.gameObject);
        }
        if (other.CompareTag("MeleeEnemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
        if (other.CompareTag("RangedEnemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
        if (other.CompareTag("Consumable"))
        {
            consumablesInRange.Remove(other.gameObject);
        }
    }


    public void RangedTakeDamage()
    {
       PlayerLife -= RangedEnemy.damage;
    }
    public void MeleeTakeDamage()
    {
       PlayerLife -= MeleeEnemy.damage;
    }
    private void OnApplicationQuit()
    {
        playerInventory.InventoryContainer.Clear();
    }


    //Returns all the enemies in front of the player (180 degrees)
    public List<GameObject> EnemiesInFront(List<GameObject> enemiesInRange)
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            if ((transform.InverseTransformPoint(enemy.transform.position).z) > 0.0f)
            {
                enemiesInFront.Add(enemy);
            }
        }
        return enemiesInFront;
    }

    public bool InteractableRangeCheck(GameObject interactable)
    {
        if (interactablesInRange.Contains(interactable))
        {
            return true;
        }
        return false;
    }
    public bool ConsumableRangeCheck(GameObject consumable)
    {
        if (consumablesInRange.Contains(consumable))
        {
            return true;
        }
        return false;
    }
    void PlayerDiedEndGame()
    {
        if (endGame != null)
        {
            endGame();
        }
        manager.PlayerDiedEndGame();
        Destroy(gameObject);

    }

    public void TakeDamage()
    {

    }

    public void DealDamage()
    {

    }
}
