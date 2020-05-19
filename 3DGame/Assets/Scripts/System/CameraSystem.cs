using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem :Singleton<CameraSystem>
{
    
    
    
    private GameObject player;
    private bool canBuy;
    private Vector3 cameraOffset;
    
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool lookAtPlayer = false;
    

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        this.transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (GameSystem.instance.isPlayerNormal())
        {
            
            FollowPlayer();
            detectWall();
            print("Normal");

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
        transform.rotation = player.transform.rotation;
        transform.Rotate(30, 0, 0);
        transform.position = player.transform.position - player.transform.TransformDirection(Vector3.forward) * 10;
        transform.position += new Vector3(0, 10, 0);

        if (lookAtPlayer) {
            transform.LookAt(player.transform);

        }
    }
    private void detectWall() {
        RaycastHit hit;
        if (Physics.Linecast(player.transform.position+Vector3.up , transform.position, out hit, LayerMask.GetMask("Terrain") ))
        {
            if (hit.collider.gameObject.tag != "MainCamera") {
                float currentDistance = Vector3.Distance(hit.point, player.transform.position);
                float cameraDistance = Vector3.Distance(transform.position, player.transform.position);
                if (cameraDistance > currentDistance)
                {
                    transform.position = hit.point + new Vector3(0 , 3 , 0);
                }

            }
        }
    }
    
    
    



}
