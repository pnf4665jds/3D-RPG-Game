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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
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
}
