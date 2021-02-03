using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChaseBehavior : StateMachineBehaviour
{
    NewEnemy EnemyScript;
    NavMeshAgent Enemy;
    Rigidbody EnemyRB;
   public float AttackDistance;
    Transform PlayerCharacter;
    public float DetectionRange;
    bool PlayerInRange, PlayerInAttRange;
    LayerMask WhatsPlayer, WhatsGround;
    //Patrol Variables
    public Vector3 RandomPatrolPoint;
    bool PatrolPointSet;
    public float PatrolPointRange = 5f;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy = animator.GetComponent<NavMeshAgent>();
        EnemyRB = animator.GetComponent<Rigidbody>();
        PlayerCharacter = GameObject.Find("Character").transform;
        //animator.SetTrigger("Walking");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("Walk Forward", true);

        animator.transform.LookAt(PlayerCharacter);
        Enemy.SetDestination(PlayerCharacter.position);
        PlayerInRange = Physics.CheckSphere(animator.transform.position, DetectionRange, WhatsPlayer);
        if (Vector3.Distance(PlayerCharacter.position, EnemyRB.position) <=  AttackDistance)
        {
            animator.SetTrigger("Stab Attack");
        }
        //else if (Vector3.Distance(PlayerCharacter.position, EnemyRB.position) <= DetectionRange)
        //{
        //    return;
        //}
        //else
        //{
            
        //    //Enemy.SetDestination(animator.transform.position);
        //    animator.SetTrigger("PlayerLost");
        //}
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Enemy.SetDestination(animator.transform.position);
        //animator.ResetTrigger("Stab Attack");
        //animator.ResetTrigger("PlayerLost");
    }
}
