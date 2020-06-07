using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionMoveBetweenPoints : ActionBase
{
    // 這個Action用來讓機器人移動到隨機一個點附近

    public List<GameObject> Points;
    public DeciderArriveDestination deciderArriveDestination;

    private int targetIndex = -1;
    private int lastIndex = 0;
    private GameObject target;
    private Vector3 finalPos;
    private GameObject player;
    private static System.Random random = new System.Random();
    private NavMeshAgent agent;

    public override void Init()
    {
        player = GameObject.FindWithTag("Player");
        animator.SetBool("Walk", true);
        targetIndex = Random.Range(0, Points.Count);
        while (targetIndex == lastIndex)
            targetIndex = Random.Range(0, Points.Count);
        target = Points[targetIndex];

        finalPos = target.transform.position;
        finalPos.y = transform.position.y;

        deciderArriveDestination.SetDestination(finalPos);

        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.SetDestination(finalPos);
    }

    public override void Process()
    {

    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
        lastIndex = targetIndex;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void Move()
    {
        /*transform.position = Vector3.MoveTowards(transform.position, finalPos, monsterInfo.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, finalPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);*/
    }
}
