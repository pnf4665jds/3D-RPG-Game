using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopRobot : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
       
        anim = this.GetComponent<Animator>();
        anim.SetBool("Open_Anim", true);

    }
    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer() {
        Collider[] enterZone = Physics.OverlapBox(transform.position , transform.localScale/2 , transform.rotation, LayerMask.GetMask("Player"));

        if (enterZone.Length > 0) {
            var playerEntity = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(playerEntity.transform);
            if (playerEntity.GetComponent<Player>()) {
                anim.SetBool("Walk_Anim", true);
            }
            else {
                anim.SetBool("Walk_Anim", false);
            }
            

            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Walk_Anim", false);
            StartCoroutine(animFlow());
        }
    }

    private IEnumerator animFlow() {
        anim.SetBool("Open_Anim", true);
        GameSystem.instance.changeGameState();
        //yield return shopping finish 
        yield return new WaitForSecondsRealtime(5);
        
        anim.SetBool("Open_Anim", false);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("anim_close"));
        GameSystem.instance.changeGameState();
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
