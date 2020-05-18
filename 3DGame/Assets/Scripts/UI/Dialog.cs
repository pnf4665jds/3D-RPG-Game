using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    
    
    public float textSpeed;    //文字的速度
    private GameObject UICanvas;

    
    private List<string> dialog = new List<string>();

    // Start is called before the first frame update

    public IEnumerator showNPCMessage() {

        int curPos = 0;  //記錄這一行到哪裡
        bool isActive = true;     //內部判定是否說完一段話
        int index = 0; //記錄到哪一行

        while (isActive) {
            this.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "";
            this.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = dialog[index].Substring(0, curPos);//刷新文本显示内容
            curPos++;
            if (curPos >= dialog[index].Length)
            {

                if (index + 1 == dialog.Count)
                {
                    this.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "按下Z鍵結束";
                    if (UICanvas)
                    {
                        var canvas = Instantiate(UICanvas);
                    }
                    else
                    {
                        yield return new WaitUntil(() => Input.GetKey(KeyCode.Z));
                        isActive = false;
                    }
                    

                }
                else {
                    this.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "按下Z鍵繼續";
                    yield return new WaitUntil(() => Input.GetKey(KeyCode.Z));
                    curPos = 0;
                    index += 1;
                }
            }
            yield return new WaitForSeconds(textSpeed);


        }

    }
    public void setUIDialog(List<string> temp) {
        dialog = temp;
    }
    public void setUICanvas(GameObject UICanvas)
    {
        this.UICanvas = UICanvas;
    }
   
    
}
