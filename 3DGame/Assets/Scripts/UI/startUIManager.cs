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
            Create
        });
    }

    private void Create()
    {
        int chooseIndex = count % 3;
        GameObject character = new GameObject();
        switch (chooseIndex)
        {
            case 0:
                character = Instantiate(Avelyn);
                character.name = "Avelyn";
                break;
            case 1:
                character = Instantiate(DogKnight);
                character.name = "DogPBR";
                break;
            case 2:
                character = Instantiate(FootMan);
                character.name = "Footman";
                break;
        }
        
        DontDestroyOnLoad(character);
    }
  
}
