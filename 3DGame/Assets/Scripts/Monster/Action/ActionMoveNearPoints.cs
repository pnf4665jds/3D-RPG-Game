using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveNearPoints : ActionBase
{
    // 這個Action用來讓機器人移動到隨機一個能量點附近

    public List<GameObject> Points;

    private int targetIndex = -1;
    private int lastIndex = 0;
    private GameObject target;
    private Vector3 finalPos;
    private GameObject player;

    public override void Init()
    {
        player = GameObject.FindWithTag("Player");
        animator.SetBool("Walk", true);
        System.Random random = new System.Random();
        targetIndex = random.Next(0, Points.Count - 1);
        if (targetIndex == lastIndex)
            targetIndex = targetIndex > 0 ? targetIndex - 1 : targetIndex + 1;
        target = Points[targetIndex];

        finalPos = transform.position + (target.transform.position - transform.position) * 0.9f;
        finalPos.y = transform.position.y;

        StartCoroutine(LookAtPlayer());
    }

    public override void Process()
    {
        if (transform.position != finalPos)
            Move();
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
        lastIndex = targetIndex;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, monsterInfo.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, finalPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private IEnumerator LookAtPlayer()
    {
        yield return new WaitUntil(() => transform.position == finalPos);
        Vector3 playerPos = player.transform.position;
        while (true)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            if (transform.rotation == Quaternion.LookRotation(newDirection))
                break;
            else
                transform.rotation = Quaternion.LookRotation(newDirection);

            yield return null;
        }
        animator.SetBool("Walk", false);
    }
}
