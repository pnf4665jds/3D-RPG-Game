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
            //print("Talking");
        }
        else if (GameSystem.instance.isPlayerOpenBackPack())
        {

           
            //print("BackPack");
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

        Vector3 cameraLeftDir = transform.TransformDirection(Vector3.left) * 2;
        Vector3 cameraRightDir = transform.TransformDirection(Vector3.right) * 2;
        Vector3 hitPointL = Vector3.zero;
        Vector3 hitPointR = Vector3.zero;

        float currentDistanceL = Vector3.Distance(transform.position + cameraLeftDir, player.transform.position);
        float currentDistanceR  = Vector3.Distance(transform.position + cameraRightDir, player.transform.position);
        float offsetDistanceL = Vector3.Distance(transform.position + cameraLeftDir, player.transform.position);
        float offsetDistanceR = Vector3.Distance(transform.position + cameraRightDir, player.transform.position);

        if (Physics.Linecast(player.transform.position, transform.position + cameraLeftDir, out hit, LayerMask.GetMask("Terrain")))
        {
            currentDistanceL = Vector3.Distance(hit.point, player.transform.position);
            hitPointL = hit.point;
            
        }
        if (Physics.Linecast(player.transform.position, transform.position + cameraRightDir, out hit, LayerMask.GetMask("Terrain")))
        {
            currentDistanceR = Vector3.Distance(hit.point, player.transform.position);
            hitPointR = hit.point;
        }

        if (currentDistanceR < offsetDistanceR && currentDistanceL < offsetDistanceL) //左右邊界都觸及到，判定兩方長短
        {
            if (currentDistanceL > currentDistanceR)//右方較短
            {
                calculateTheCameraPos(Vector3.Distance(transform.position + cameraRightDir, player.transform.position),
                    Vector3.Distance(transform.position + cameraRightDir, hitPointR),
                    Vector3.Distance(transform.position, player.transform.position), currentDistanceR);
            }
            else //左方較短或一樣
            {
                calculateTheCameraPos(Vector3.Distance(transform.position + cameraLeftDir, player.transform.position),
                    Vector3.Distance(transform.position + cameraLeftDir, hitPointL),
                    Vector3.Distance(transform.position, player.transform.position), currentDistanceL);
            }

        }
        else if (currentDistanceR < offsetDistanceR) //只有右邊觸及到
        {
            calculateTheCameraPos(Vector3.Distance(transform.position + cameraRightDir, player.transform.position),
                    Vector3.Distance(transform.position + cameraRightDir, hitPointR),
                    Vector3.Distance(transform.position, player.transform.position), currentDistanceR);
        }
        else if (currentDistanceL < offsetDistanceL)//只有左邊觸及到
        {
            calculateTheCameraPos(Vector3.Distance(transform.position + cameraLeftDir, player.transform.position),
                    Vector3.Distance(transform.position + cameraLeftDir, hitPointL),
                    Vector3.Distance(transform.position, player.transform.position), currentDistanceL);
        }


        Debug.DrawLine(player.transform.position, transform.position + cameraLeftDir, Color.green);
        Debug.DrawLine(player.transform.position, transform.position + cameraRightDir, Color.green);
        Debug.DrawLine(transform.position, transform.position + cameraLeftDir, Color.red);
        Debug.DrawLine(transform.position, transform.position + cameraRightDir, Color.red);
    }

    private void calculateTheCameraPos(float offsetToPlayer , float offsetToHit , float cameraToPlayer , float currentDistance)
    {
        Vector3 camToPlayer = transform.position - player.transform.position;
        float ratio = offsetToHit / offsetToPlayer;
        transform.position -= camToPlayer * ratio;
        if (currentDistance < 5)
        {
            transform.position += transform.up * 3 + transform.forward * 4;
        }
    }
    private void moveLerp(Vector3 targetPos , float speed) {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
