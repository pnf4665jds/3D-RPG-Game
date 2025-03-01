﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public string NPCname;
    public TextAsset TxtFile;
    protected Animator anim;
    public GameObject UICanvas; //專屬腳色的UI面板
    private List<string> contentToTalk = new List<string>();
    

    private void Start()
    {

        anim = this.GetComponent<Animator>();

        if (TxtFile != null)
        {
            string[] subString = TxtFile.text.Split('\n');
            foreach (string s in subString)
            {
                addDialog(s);
            }
        }

    }

    public void changeName(string name) {
        this.NPCname = name;
    }
    public void addDialog(string dialog) {

        contentToTalk.Add(dialog);

    }
    
    

    public string getName() {
        return NPCname;
    }
    public List<string> getDialog()
    {
        return contentToTalk;
    }
    public GameObject getNPCCanvas() {
        return UICanvas;
    }
}
