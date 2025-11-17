using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerAI : MonoBehaviour
{
    public Transform player;                        //유저 위치
    public float chaseRange = 50.0f;
    public float attackRange = 20.0f;

    private NavMeshAgent agent;                     //길 찾기 알고리즘을 지원해주는 ai agent
    private float distanceToPlayer;                 //플레이어와의 거리
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }

        if(distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);          //플레이어 위치로 목적지를 설정한다
    }

    void StopChasing()
    {
        agent.isStopped = true;
    }

    void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        Debug.Log("Attacking player");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
