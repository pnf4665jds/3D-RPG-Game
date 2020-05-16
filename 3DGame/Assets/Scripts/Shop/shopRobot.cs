using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopRobot : NPC
{
    // Start is called before the first frame update
    
    public Vector3 DetectPlayerSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 openShopSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Sprite pressX;
    public Sprite shop;
    
    private void FixedUpdate()
    {
        LookAtPlayer();
        shopOpen();
        
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
            anim.SetBool("Walk_Anim", false);
        }
    }
    private void shopOpen()
    {
        Collider[] enterZone = Physics.OverlapBox(transform.position, openShopSize, transform.rotation, LayerMask.GetMask("Player"));
        
        if (enterZone.Length > 0 )
        {
            this.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = pressX;

            if (Input.GetKey(KeyCode.X)) {

                anim.SetBool("Walk_Anim", false);
                GameSystem.instance.changeModeTalking(this);
                //StartCoroutine(animFlow());

            }
            
        }
        else
        {
            GameSystem.instance.changeModeFollowPlayer();
            this.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = shop;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
        }
    }

    private IEnumerator animFlow() {
        
        GameSystem.instance.changeTheWorldTime(0);
        yield return new WaitForSecondsRealtime(5);
        GameSystem.instance.changeTheWorldTime(1);

        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, DetectPlayerSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, openShopSize);
    }
   

}
