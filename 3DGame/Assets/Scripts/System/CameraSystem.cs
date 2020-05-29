using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem :Singleton<CameraSystem>
{
    
    
    
    private GameObject player;
    private bool canBuy;
    private Vector3 cameraOffset;
    

    public bool lookAtPlayer = false;
    

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        this.transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        transform.Rotate(30, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {

        if (GameSystem.instance.isPlayerNormal())
        {
            
            FollowPlayer();
            detectWall();
            //print("Normal");

        }
        else if (GameSystem.instance.isPlayerTalking())
        {
            this.GetComponent<Camera>().fieldOfView = 40;
            print("Talking");
        }
        else if (GameSystem.instance.isPlayerOpenBackPack())
        {

           
            print("BackPack");
        }
        else if (GameSystem.instance.isPlayerShopping()) {


        }
        
    }
    
    private void FollowPlayer() {
        this.GetComponent<Camera>().fieldOfView = 60;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
        //transform.rotation = player.transform.rotation;
        //transform.Rotate(30, 0, 0);
        transform.position = player.transform.position - player.transform.TransformDirection(Vector3.forward) * 10;
        transform.position += new Vector3(0, 10, 0);

        if (lookAtPlayer) {
            transform.LookAt(player.transform);

        }
        if (Input.GetMouseButton(1)) {

            this.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        }
    }
    private void detectWall() {
        RaycastHit hit;
        if (Physics.Linecast(player.transform.position+Vector3.up*2 , transform.position, out hit, LayerMask.GetMask("Terrain") ))
        {
            if (hit.collider.gameObject.tag != "MainCamera") {
               
                float currentDistance = Vector3.Distance(hit.point, player.transform.position);
                float cameraDistance = Vector3.Distance(transform.position, player.transform.position);
                //print(currentDistance);
                if (cameraDistance > currentDistance)
                {
                    if (currentDistance < 5)
                    {
                        transform.position = hit.point + transform.forward*4 + transform.up*2;
                        //transform.Rotate(-30, 0, 0);
                    }
                    else {
                        transform.position = hit.point;
                    }
                    Debug.DrawLine(player.transform.position + Vector3.up, hit.point, Color.red);
                    
                }

            }
        }
    }
    
    
    



}
