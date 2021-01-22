using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehavior : StateMachineBehaviour
{
   public float damage = 10f;
   public float AttackRadius = 2f;
    bool HaveAttacked=false;
    public Transform Enemypos;
    EnemyMeleeAI EnemyScript;
    NavMeshAgent Enemy;
    Rigidbody EnemyRB;
    float AttackDistance;
    public Transform PlayerCharacter;
    float DetectionRange;
    bool PlayerInRange,PlayerInAttRange;
    LayerMask WhatsPlayer, WhatsGround;
    //Patrol Variables
    public Vector3 RandomPatrolPoint;
    bool PatrolPointSet;
    public float PatrolPointRange=5f;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = animator.transform.position;
        Enemypos = animator.transform;
        PlayerCharacter = GameObject.Find("Character").transform;
        //EnemyScript = animator.GetComponent<EnemyMeleeAI>();
    }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //EnemyScript.AttackPlayer();
        animator.transform.position = animator.transform.position;
        animator.transform.LookAt(PlayerCharacter);
        if (Vector3.Distance(PlayerCharacter.position, EnemyRB.position) > AttackDistance)
        {
            //animator.ResetTrigger("Stab Attack");
            animator.SetTrigger("PlayerLost");
        }

    }
   
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Walking");
        if (Vector3.Distance(PlayerCharacter.transform.position, EnemyRB.position) > AttackDistance)
        {
            animator.ResetTrigger("Stab Attack");
        }
        //if (Vector3.Distance(PlayerCharacter.transform.position, EnemyRB.position) <= DetectionRange)
        //{
        //    animator.SetTrigger("Walking");
        //}


    }
}
