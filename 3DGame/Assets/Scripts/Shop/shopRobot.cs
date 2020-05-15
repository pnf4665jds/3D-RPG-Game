using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopRobot : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public Vector3 DetectPlayerSize = new Vector3(1.0f, 1.0f, 1.0f);
    void Start()
    {
       
        anim = this.GetComponent<Animator>();
        

    }
    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer() {
        Collider[] enterZone = Physics.OverlapBox(transform.position , DetectPlayerSize , transform.rotation, LayerMask.GetMask("Player"));

        if (enterZone.Length > 0)
        {
            anim.SetBool("Open_Anim", true);
            var playerEntity = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(playerEntity.transform);
            if (playerEntity.GetComponent<Player>().GetSpeed() != 0)
            {
                anim.SetBool("Walk_Anim", true);
            }
            else
            {
                anim.SetBool("Walk_Anim", false);
            }
        }
        else {
            anim.SetBool("Open_Anim", false);
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
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CameraSystem.instance.changeCameraModeFpllowPlayer();
        }
    }

    private IEnumerator animFlow() {
        
        GameSystem.instance.changeGameState();
        CameraSystem.instance.changeCameraModeTalking();
        yield return new WaitForSecondsRealtime(5);

        GameSystem.instance.changeGameState();

        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, DetectPlayerSize);
    }
}
