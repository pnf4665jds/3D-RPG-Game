using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionMessage : MonoBehaviour
{
    private bool isEntered , toMid;
    private CanvasGroup obj;
    private Vector3 startPos , endPos , midPos;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.GetComponent<CanvasGroup>();
        isEntered = false;
        toMid = false;
        Init();
        //showMessage("123");
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(transMessage());
    }
    private IEnumerator FadeMessage()
    {
        while (obj.alpha < 1)
        {
            obj.alpha += Time.deltaTime / 4;
            yield return null;
        }
        yield return new WaitUntil(() => toMid);
        while (obj.alpha > 0)
        {
            
            obj.alpha -= Time.deltaTime / 3;
            yield return null;
        }
    }
    private IEnumerator transMessage()
    {
        float count = 0;
        while (count < 200.0f)
        {
            this.transform.Translate(-0.5f, 0 , 0)  ;
            count += 0.5f;
            yield return null;
        }
        
        yield return new WaitForSeconds(3.0f);
        toMid = true;
        count = 0;
        while (count < 200.0f)
        {
            this.transform.Translate(-0.8f, 0, 0);
            count += 0.5f;
            yield return null;
        }
    }
}
