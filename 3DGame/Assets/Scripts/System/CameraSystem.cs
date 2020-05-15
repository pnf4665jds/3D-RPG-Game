using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem :Singleton<CameraSystem>
{
    
    public enum CameraMode {
        normal,
        talking,
        backpack 
    }
    public GameObject DialogCanvas;
    public GameObject PlayerDetailCanvas;
    private CameraMode cameraM;
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

        cameraM = CameraMode.normal;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (cameraM == CameraMode.normal)
        {
            DialogCanvas.SetActive(false);
            PlayerDetailCanvas.SetActive(true);
            FollowPlayer();
            
        }
        else if (cameraM == CameraMode.talking)
        {
            DialogCanvas.SetActive(true);
            PlayerDetailCanvas.SetActive(false);
            showDialog();
        }
        else if (cameraM == CameraMode.backpack)
        {

        }
        
    }
    public void changeCameraModeTalking() {

        cameraM = CameraMode.talking;
    }
    public void changeCameraModeBackPack()
    {

        cameraM = CameraMode.backpack;
    }
    public void changeCameraModeFpllowPlayer()
    {

        cameraM = CameraMode.normal;
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
}
