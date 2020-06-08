using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class startUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button right;
    public Button left;
    public Button Enter;
    public GameObject DogKnight;
    public GameObject Avelyn;
    public GameObject FootMan;
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private int count;
    private GameObject createdCharacter;
    void Start()
    {
        character = GameObject.Find("character");
        count = 0 ;
        ActiveCharacter(0);
        right.onClick.AddListener(Change);
        left.onClick.AddListener(Change);
        Enter.onClick.AddListener(enterClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ActiveCharacter(int index) {
        for (int i = 0; i < character.transform.childCount; i++) {
            if (i == index)
            {
                character.transform.GetChild(i).gameObject.SetActive(true);
            }
            else {
                character.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }
    private void Change() {

        count++;

        ActiveCharacter(count % 3);

    }
    private void enterClick() {

        GameSceneManager.instance.LoadScene("GamePlayScene", new List<Action>()
        {
            Create,
            // 切換場景時將主角移到指定座標
            () => createdCharacter.transform.position = new Vector3(415, 5, 30),
            // 更改成指定的Y軸旋轉角度
            () => createdCharacter.transform.rotation = Quaternion.Euler(0, 340, 0),
            // 重置狀態
            () => createdCharacter.GetComponent<Player>().ResetAnything()
        });
    }

    private void Create()
    {
        int chooseIndex = count % 3;
        switch (chooseIndex)
        {
            case 0:
                createdCharacter = Instantiate(Avelyn);
                createdCharacter.name = "Avelyn";
                break;
            case 1:
                createdCharacter = Instantiate(DogKnight);
                createdCharacter.name = "DogPBR";
                break;
            case 2:
                createdCharacter = Instantiate(FootMan);
                createdCharacter.name = "Footman";
                break;
        }
        
        DontDestroyOnLoad(createdCharacter);
    }
  
}
