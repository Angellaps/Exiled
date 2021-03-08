
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAI: MonoBehaviour 
{
    public float speed = 3f;
    public float AttackRadius = 3f;
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

    //Variables used for the patrol method
    public Vector3 RandomPatrolPoint;
    bool PatrolPointSet;
    public float PatrolPointRange;

    //Variables used to control Attack parameters
    public float AttackTimer;
    bool HaveAttacked;
    public GameObject projectile;

    //Checks for the different states 
    public float DetectionRange, AttackDistance;
    public bool PlayerInRange, PlayerInAttRange;

    private void Awake()
    {
        PlayerCharacter = GameObject.Find("Character").transform;
        EnemyAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        speed = GetComponent<NavMeshAgent>().speed;;
        EnemyHp = 20f;
        EnemyAnim = GetComponent<Animator>();
        EnemyAnim.SetFloat("AnimSpeed", speed);
    }

    private void Update()
    {
        //Checks for the player's distance and conditions that change the states based on range
        PlayerInRange = Physics.CheckSphere(transform.position, DetectionRange, WhatsPlayer);
        PlayerInAttRange = Physics.CheckSphere(transform.position, AttackDistance, WhatsPlayer);

        if (!PlayerInRange && !PlayerInAttRange) Patroling();
        if (PlayerInRange && !PlayerInAttRange) ChasePlayer();
        if (PlayerInAttRange && PlayerInRange) AttackPlayer();
    }

    public void Patroling()
    {
        EnemyAnim.SetBool("Walk Forward", true);
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
        //Make enemy find random patrol points around him then set a raycast to that point
        float ZAxisMove = Random.Range(-PatrolPointRange, PatrolPointRange);
        float XAxisMove = Random.Range(-PatrolPointRange, PatrolPointRange);

        RandomPatrolPoint = new Vector3(transform.position.x + XAxisMove, transform.position.y, transform.position.z + ZAxisMove);

        if (Physics.Raycast(RandomPatrolPoint, -transform.up, 2f, WhatsGround))
            PatrolPointSet = true;
    }
    //Simple state that overrides the destination of the enemy to the player's position
    private void ChasePlayer()
    {
        EnemyAnim.SetBool("Walk Forward", true);
        EnemyAgent.SetDestination(PlayerCharacter.position);
    }

    public void AttackPlayer()
    {
        //Attack behavior , checks for attack timer and makes the enemy look at the player it is attacking and stopping it in place during the attack
        EnemyAnim.SetBool("Walk Forward", false);
        EnemyAnim.SetBool("Stab Attack", true);
        transform.LookAt(PlayerCharacter);
        Collider[] HitCollisions = Physics.OverlapSphere(transform.position, AttackRadius);
        foreach (var player in HitCollisions)
        {
        
            if (player.CompareTag("Player"))
            {
                if (HaveAttacked == false)
                {          
                    player.GetComponent<Player>().PlayerLife-=damage;
                    player.GetComponent<VitalStats>().hpCurrentAmount -= damage;
                    HaveAttacked = true;
                    Invoke(nameof(ResetAttack), AttackTimer);
                }
            }
        }
    }
    private void ResetAttack()
    {
        HaveAttacked = false;
    }
    //Gizmos that help visuallize the detection and attack range of the enemy
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
