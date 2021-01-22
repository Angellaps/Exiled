using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartBehavior : StateMachineBehaviour
{
    EnemyMeleeAI EnemyScript;
    public float AttackDistance;
    public float DetectionRange;
    NavMeshAgent Enemy;
    Rigidbody EnemyRB;
    Transform PlayerCharacter;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyScript = animator.GetComponent<EnemyMeleeAI>();
        Enemy = animator.GetComponent<NavMeshAgent>();
        EnemyRB = animator.GetComponent<Rigidbody>();
        PlayerCharacter = GameObject.Find("Character").transform;

        if (Vector3.Distance(PlayerCharacter.position, animator.transform.position) <= AttackDistance)
        {
            Debug.Log("Sup");
            animator.SetTrigger("Stab Attack");
        }
        //EnemyScript.SearchWalkPoint();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(PlayerCharacter.position, EnemyRB.position) <= AttackDistance)
        {
            Debug.Log("Sup");
            animator.SetTrigger("Stab Attack");
        }
        else
        {
            return;
        }

        if (Vector3.Distance(PlayerCharacter.position, EnemyRB.position) >= DetectionRange && Vector3.Distance(PlayerCharacter.position, EnemyRB.position) >= AttackDistance)
        {
            EnemyScript.Patroling();
        }
        
        //if ((Vector3.Distance(PlayerCharacter.position, EnemyRB.position) <= DetectionRange))
        //{
        //    animator.SetTrigger("Walking");
        //}
        //else
        //{
        //    return;
        //}

      
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Walking");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
