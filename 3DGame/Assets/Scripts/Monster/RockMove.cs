using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    private GameObject player;
    public float speed ;
    private float distanceToPlayer;
    private bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        StartCoroutine(shoot());

    }

    public IEnumerator shoot() {
        Vector3 playerPos = player.transform.position;
        
        while (move) {
            this.transform.LookAt(playerPos);
            float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, playerPos) / distanceToPlayer) * 45;
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
            float currentDist = Vector3.Distance(this.transform.position, player.transform.position);
            if (currentDist < 0.5f || this.transform.position.y  < player.transform.position.y)
            {
                move = false;
                Destroy(this.gameObject);
            }
            this.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
            yield return null;
        }
        
    }
}
