using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemy : MonoBehaviour
{
    public float AttackRadius = 2f;
    public float damage = 20f;
    [SerializeField]
    MonsterLoot Monsterloot;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    Player player;
    Animator EnemyAnim;
    public NavMeshAgent EnemyAgent;

    public Transform PlayerCharacter;

    public LayerMask WhatsGround, WhatsPlayer;

    public float EnemyHp;

    //Patrol Variables
    public Vector3 RandomPatrolPoint;
    bool PatrolPointSet;
    public float PatrolPointRange;

    //Attack Variables
    public float AttackTimer;
    bool HaveAttacked;
    public GameObject projectile;

    //Different States
    public float DetectionRange, AttackDistance;
    public bool PlayerInRange, PlayerInAttRange;

    private void Awake()
    {

        EnemyAgent = GetComponent<NavMeshAgent>();
        PlayerCharacter = GameObject.Find("Character").transform;
    }
    private void Start()
    {
        EnemyHp = 20f;
        EnemyAnim = GetComponent<Animator>();
    }

    public void Update()
    {
        float AttackCalc = Vector3.Distance(transform.position, Player.transform.position);
        if (AttackCalc < 3)
        {
            AttackDistance = 3;
        }
        else
        {
            AttackDistance = 0;
        }
        //Check Player seen distance and attack distance
        PlayerInRange = Physics.CheckSphere(transform.position, DetectionRange, WhatsPlayer);
        PlayerInAttRange = Physics.CheckSphere(transform.position, AttackDistance, WhatsPlayer);

        if (!PlayerInRange && !PlayerInAttRange) Patroling();
        //if (PlayerInRange && !PlayerInAttRange) ChasePlayer();
        //if (PlayerInAttRange && PlayerInRange) AttackPlayer();
    }
    public void Patroling()
    {
        //EnemyAnim.SetBool("Walk Forward", true);
        if (!PatrolPointSet) SearchWalkPoint();

        if (PatrolPointSet)
            EnemyAgent.SetDestination(RandomPatrolPoint);

        Vector3 CalcDistancetoPatrolPoint = transform.position - RandomPatrolPoint;

        //WHen patrol point is reached
        if (CalcDistancetoPatrolPoint.magnitude < 1f)
            PatrolPointSet = false;
    }
    public void SearchWalkPoint()
    {
        //Make enemy find random patrol points around him
        float ZAxisMove = Random.Range(-PatrolPointRange, PatrolPointRange);
        float XAxisMove = Random.Range(-PatrolPointRange, PatrolPointRange);

        RandomPatrolPoint = new Vector3(transform.position.x + XAxisMove, transform.position.y, transform.position.z + ZAxisMove);

        if (Physics.Raycast(RandomPatrolPoint, -transform.up, 2f, WhatsGround))
            PatrolPointSet = true;
    }

    private void ChasePlayer()
    {
        EnemyAnim.SetBool("Walk Forward", true);
        EnemyAgent.SetDestination(PlayerCharacter.position);
    }

    private void AttackPlayer()
    {
        #region OldAttack 
        //if (AttackDistance<=3f)
        //{

        //EnemyAnim.SetBool("Walk Forward", false);
        //EnemyAnim.SetBool("Stab Attack", true);
        ////Make enemy stanionary to attack
        //EnemyAgent.SetDestination(EnemyAgent.transform.position);

        //transform.LookAt(PlayerCharacter);

        //if (!HaveAttacked)
        //{

        //    player.MeleeTakeDamage();


        //    //Ranged Attack 
        //    //Rigidbody EnemyRB = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //    //EnemyRB.AddForce(transform.forward * 32f, ForceMode.Impulse);
        //    //EnemyRB.AddForce(transform.up * 8f, ForceMode.Impulse);


        //    HaveAttacked = true;
        //    Invoke(nameof(ResetAttack), AttackTimer);
        //}
        //}
        #endregion
        //EnemyAnim.SetBool("Walk Forward", false);

        //Make enemy stanionary to attack
        //EnemyAgent.SetDestination(EnemyAgent.transform.position);
        transform.LookAt(PlayerCharacter);
        EnemyAnim.SetBool("Stab Attack", true);

        Collider[] HitCollisions = Physics.OverlapSphere(transform.position, AttackRadius);
        foreach (var player in HitCollisions)
        {

            if (HaveAttacked == false)
            {
                Debug.Log("Mamouni Striked!");
                player.GetComponent<Player>().PlayerLife -= damage;
                HaveAttacked = true;
                Invoke(nameof(ResetAttack), AttackTimer);
            }

        }
    }
    //private void StopThisEnemy()

    //{
    //    EnemyAgent.isStopped = true;
    //}
    //private void ContinueChase()
    //{
    //    EnemyAgent.SetDestination(PlayerCharacter.transform.position);
    //}
    private void ResetAttack()
    {
        HaveAttacked = false;
        EnemyAnim.SetBool("Stab Attack", false);
    }

    public void TakeDamage()
    {
        EnemyHp -= player.PlayerDamage;

        if (EnemyHp <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Monsterloot.GenerateLoot();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
