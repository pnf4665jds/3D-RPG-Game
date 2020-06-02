using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem :Singleton<CameraSystem>
{

    private enum CameraMode {
        outSide,
        normal
    }
    
    private GameObject player;
    private bool canBuy;
    private Vector3 cameraOffset;
    private float deltaBack;
    private float deltaUp;
    private float distance;
    private CameraMode cm;

    public bool lookAtPlayer = false;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        deltaBack = 10.0f;
        deltaUp = 10.0f;

        //this.transform.position = player.transform.position;
        transform.position = player.transform.position - player.transform.TransformDirection(Vector3.forward) * 10 + player.transform.TransformDirection(Vector3.up) * 10 ;
        transform.rotation = player.transform.rotation;
        transform.Rotate(30, 0, 0);
        distance = Vector3.Distance(transform.position , player.transform.position)  ;
        cm = CameraMode.normal;

    }

    // Update is called once per frame
    void Update()
    {
        //print(cm);
        if (GameSystem.instance.isPlayerNormal())
        {
            FollowPlayer();
            detectWall();

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

        if (cm == CameraMode.normal)
        {
            transform.position = player.transform.position - player.transform.TransformDirection(Vector3.forward) * deltaBack + player.transform.TransformDirection(Vector3.up) * deltaUp; ;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
            if (Input.GetMouseButton(1))
            {
                this.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
            }
        }
        else if (cm == CameraMode.outSide)
        {
           this.transform.LookAt(player.transform.position);
            var targetPos = player.transform.position - player.transform.TransformDirection(Vector3.forward) * 10 +
                    player.transform.TransformDirection(Vector3.up) * 10;
            if (Vector3.Distance(transform.position, player.transform.position) > distance - 1) {

                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime*10);
                if (transform.position == targetPos) {
                    cm = CameraMode.normal;
                }
            }
        }

        this.GetComponent<Camera>().fieldOfView = 60;

        
    }
    
    private void detectWall() {

        RaycastHit hit;
        Collider[] clipCollider = Physics.OverlapSphere(transform.position, 1.0f , LayerMask.GetMask("Terrain"));
        /*if (Physics.Linecast(player.transform.position + Vector3.up * 2, transform.position, out hit, LayerMask.GetMask("Terrain")))
        {

            if (hit.collider.gameObject.tag != "MainCamera")
            {
                cm = CameraMode.outSide;
                float currentDistance = Vector3.Distance(hit.point, player.transform.position);
                float cameraDistance = Vector3.Distance(transform.position, player.transform.position);
                if (cameraDistance > currentDistance)
                {
                    cm = CameraMode.normal;
                    if (currentDistance < 5)
                    {
                        transform.position = hit.point + transform.forward * 4 + transform.up * 2;

                    }
                    else
                    {
                        transform.position = hit.point + transform.forward * 2;
                    }
                    Debug.DrawLine(player.transform.position + Vector3.up, hit.point, Color.red);

                }
            }

        }*/
        if (Physics.Linecast(player.transform.position, transform.position + Vector3.left * 2, out hit, LayerMask.GetMask("Terrain")))
        {
            float currentDistance = Vector3.Distance(hit.point, player.transform.position);
            float LeftDistance = Vector3.Distance(transform.position + Vector3.left * 2 , player.transform.position);
            float cameraHitDistance = Vector3.Distance(transform.position + Vector3.left * 2  ,  hit.point);
            float cameraDistance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 camToPlayer = transform.position - player.transform.position;

            if (currentDistance < cameraDistance)
            {
                //cm = CameraMode.outSide;
                
                float ratio = cameraHitDistance / LeftDistance;
                transform.position -= camToPlayer * ratio;
                if (currentDistance < 5) {
                    transform.position += transform.up * 3 + transform.forward * 4;
                }
                //moveLerp(transform.position - camToPlayer * ratio, 1);

            }
        }
        else if (Physics.Linecast(player.transform.position, transform.position + Vector3.right * 2, out hit, LayerMask.GetMask("Terrain")))
        {
            float currentDistance = Vector3.Distance(hit.point, player.transform.position);
            float rightDistance = Vector3.Distance(transform.position + Vector3.right * 2, player.transform.position);
            float cameraHitDistance = Vector3.Distance(transform.position + Vector3.right * 2, hit.point);
            float cameraDistance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 camToPlayer = transform.position - player.transform.position;

            if (currentDistance < cameraDistance)
            {
                //cm = CameraMode.outSide;

                float ratio = cameraHitDistance / rightDistance;
                transform.position -= camToPlayer * ratio;
                //moveLerp(transform.position - camToPlayer * ratio, 1);
                if (currentDistance < 5)
                {
                    transform.position += transform.up * 3 + transform.forward * 4;
                }
            }
        }
        Debug.DrawLine(player.transform.position, transform.position + Vector3.left * 2, Color.green);
        Debug.DrawLine(player.transform.position, transform.position + Vector3.right * 2, Color.green);
        
    }


    private void moveLerp(Vector3 targetPos , float speed) {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
