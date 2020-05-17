using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem :Singleton<CameraSystem>
{
    
    
    public GameObject DialogCanvas;
    public GameObject PlayerDetailCanvas;
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
        cameraOffset = transform.position - player.transform.position;
        canBuy = false;

        GameSystem.instance.changeModeFollowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.instance.isPlayerNormal())
        {
            
            DialogCanvas.SetActive(false);
            PlayerDetailCanvas.SetActive(true);
            PlayerDetailCanvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            FollowPlayer();
            
        }
        else if (GameSystem.instance.isPlayerTalking())
        {

            
        }
        else if (GameSystem.instance.isPlayerOpenBackPack())
        {

            PlayerDetailCanvas.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
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
    public void showDialog(NPC character) {

        DialogCanvas.SetActive(true);
        PlayerDetailCanvas.SetActive(false);
        this.GetComponent<Camera>().fieldOfView = 40;
        setNPCName(character.getName());
        setDialog(character.getDialog());
        StartCoroutine(DialogCanvas.GetComponent<Dialog>().showNPCMessage());
        

    }
    private void setNPCName(string name) {
        DialogCanvas.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = name;

    }
    private void setDialog(List<string> temp)
    {
        DialogCanvas.GetComponent<Dialog>().setUIDialog(temp);
    }
    public void canBuying(bool canBuy) {
        this.canBuy = canBuy;
    }


}
