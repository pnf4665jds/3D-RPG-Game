using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem :Singleton<CameraSystem>
{
    
    
    public GameObject DialogCanvas;
    public GameObject PlayerDetailCanvas;
    private GameObject player;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool lookAtPlayer = false;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraOffset = transform.position - player.transform.position;

        GameSystem.instance.changeModeFollowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.instance.isPlayerNormal())
        {
            DialogCanvas.SetActive(false);
            PlayerDetailCanvas.SetActive(true);
            FollowPlayer();
            
        }
        else if (GameSystem.instance.isPlayerTalking())
        {
            DialogCanvas.SetActive(true);
            PlayerDetailCanvas.SetActive(false);
            showDialog();
        }
        else if (GameSystem.instance.isPlayerOpenBackPack())
        {

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
    private void showDialog() {
        this.GetComponent<Camera>().fieldOfView = 40;
    }
    public void setNPCNameInDialog(string name) {
        DialogCanvas.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = name;
    }

}
