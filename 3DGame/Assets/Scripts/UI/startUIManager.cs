using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button right;
    public Button left;
    [SerializeField]
    private GameObject character;
    private int count;
    void Start()
    {
        character = GameObject.Find("character");
        count = 0 ;
        initCharacter();
        right.onClick.AddListener(Change);
        left.onClick.AddListener(Change);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void initCharacter() {
        for (int i = 1; i < character.transform.childCount; i++) {
            character.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void Change() {

        count++;

        character.transform.GetChild(count % 2).gameObject.SetActive(true);
        character.transform.GetChild(Mathf.Abs(count % 2-1)).gameObject.SetActive(false);

    }
  
}
