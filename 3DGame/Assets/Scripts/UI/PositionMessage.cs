using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionMessage : MonoBehaviour
{
    private bool isEntered , toMid;
    private CanvasGroup obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = this.GetComponent<CanvasGroup>();
        isEntered = false;
        toMid = false;
        Init();
    }

    public void Init()
    {
        obj.alpha = 0;

    }
    public void setIsGetPosition(bool isEntered) {
        this.isEntered = isEntered;
    }
    public bool getIsGetPosition() {
        return this.isEntered;
    }
    
    public void showMessage(string title) {

        this.transform.GetChild(0).GetComponent<Text>().text = title;
        StartCoroutine(FadeMessage());

    }
    private IEnumerator FadeMessage()
    {
        while (obj.alpha < 1)
        {
            obj.alpha += Time.deltaTime / 4;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (obj.alpha > 0)
        {
            
            obj.alpha -= Time.deltaTime / 3;
            yield return null;
        }
    }
    
}
