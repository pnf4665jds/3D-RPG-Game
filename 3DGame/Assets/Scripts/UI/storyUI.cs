using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class storyUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setloadingFinshFile(int index)
    {
        string filePath = "Story/loadingFinish/0" + index ;
        TextAsset file = Resources.Load<TextAsset>(filePath);
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = file.text;
        

    }
    public void setbossDeadFile(int index)
    {
        string filePath = "Story/bossDead/0" + index ;
        TextAsset file = Resources.Load<TextAsset>(filePath);
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = file.text;

    }
    public void setSkip()
    {
        string skipText = "按 Z 跳過";
        this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = skipText;
    }
    public void setInit() {
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
        this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "";
    }


}
