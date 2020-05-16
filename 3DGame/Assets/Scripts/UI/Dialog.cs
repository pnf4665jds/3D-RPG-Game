using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private float timer;  //紀錄時間
    private int curPos;  //記錄這一行到哪裡
    private int curSentence; //記錄到哪一行
    public float textSpeed;    //文字的速度
    private bool isTalking;    //外部呼叫可以開始說話
    private bool isActive ;     //內部判定是否說完一段話
    private List<string> dialog = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        isActive = true;
        curSentence = 0;
        timer = textSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            showMessagePerWord(curSentence);
            
            if (Input.GetKey(KeyCode.Z) && curSentence + 1 < dialog.Count && !isActive)
            {
                curPos = 0;
                curSentence++;
                timer = textSpeed;
                isActive = true;

            }
            
        }
       
            

    }
    public void setTalking(bool isTalking) {

        this.isTalking = isTalking;
    }
    private void showMessagePerWord(int index)
    {
        if (isActive) {
            
            timer += Time.deltaTime;
            if (timer >= textSpeed)
            {//判断计时器时间是否到达             
                this.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = dialog[index].Substring(0, curPos);//刷新文本显示内容

                if (curPos >= dialog[index].Length)
                {
                    isActive = false;
                    if (curSentence + 1 == dialog.Count)
                    {
                        CameraSystem.instance.canBuying(true);
                    }
                    
                    
                }

                curPos++;
                timer = 0;
            }
        }

    }
    public void setUIDialog(List<string> temp) {
        dialog = temp;
    }
    public void initDialogCanvas() {
        curPos = 0;
        curSentence = 0;
        isActive = true;
        this.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "";
        timer = textSpeed;
    }
    public bool finishTalking() {
        return !isTalking;
    }
    
}
