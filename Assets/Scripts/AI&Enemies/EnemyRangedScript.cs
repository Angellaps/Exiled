﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedScript : MonoBehaviour , IDamage
{
    public float speed = 0f;
    public float damage = 0f;
    [SerializeField]
    MonsterLoot Monsterloot;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    Player player;
    Animator EnemyAnimRanged;
    public NavMeshAgent EnemyAgent;

    public Transform PlayerCharacter;

    public LayerMask WhatsGround, WhatsPlayer;

    public float EnemyHp;
    public Transform AttPoint;
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
        PlayerCharacter = GameObject.Find("Character").transform;
        EnemyAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        speed = GetComponent<NavMeshAgent>().speed; ;
        EnemyHp = 20f;
        EnemyAnimRanged = GetComponent<Animator>();
        EnemyAnimRanged.SetFloat("AnimSpeed", speed);
    }

    private void Update()
    {
        //Check Player seen distance and attack distance
        PlayerInRange = Physics.CheckSphere(transform.position, DetectionRange, WhatsPlayer);
        PlayerInAttRange = Physics.CheckSphere(transform.position, AttackDistance, WhatsPlayer);

        if (!PlayerInRange && !PlayerInAttRange) Patroling();
        if (PlayerInRange && !PlayerInAttRange) ChasePlayer();
        if (PlayerInAttRange && PlayerInRange) AttackPlayer();
    }

    public void Patroling()
    {
        EnemyAnimRanged.SetBool("Walk Forward", true);
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
        EnemyAnimRanged.SetBool("Walk Forward", true);
        EnemyAgent.SetDestination(PlayerCharacter.position);
    }

    public void AttackPlayer()
    {
        EnemyAnimRanged.SetBool("Walk Forward", false);
        //EnemyAnimRanged.SetBool("Cast Spell", true);
        
        //Make enemy stanionary to attack
        EnemyAgent.SetDestination(EnemyAgent.transform.position);

        transform.LookAt(PlayerCharacter);

        if (!HaveAttacked)
        {
            EnemyAnimRanged.SetTrigger("Cast Spell");
            //Ranged Attack 
            Rigidbody EnemyRB = Instantiate(projectile, AttPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            EnemyRB.AddForce(transform.forward * 5f, ForceMode.Impulse);
            EnemyRB.AddForce(transform.up * 2f, ForceMode.Impulse);


            HaveAttacked = true;
            Invoke(nameof(ResetAttack), AttackTimer);
            //EnemyAnimRanged.SetBool("Cast Spell", false);
        }
    }
    private void ResetAttack()
    {
        HaveAttacked = false;
        //EnemyAnimRanged.SetBool("Cast Spell", false);
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
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }

    public void DealDamage()
    {
        throw new System.NotImplementedException();
    }
}
