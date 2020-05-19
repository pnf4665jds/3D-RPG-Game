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
        this.transform.position = player.transform.position + new Vector3(-1.5f, 9.4f, -11);
        cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.instance.isPlayerNormal())
        {
            
            FollowPlayer();
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
        
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        if (lookAtPlayer) {
            transform.LookAt(player.transform);

        }
    }
    
    
    



}
