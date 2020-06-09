using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class feedBackUI : MonoBehaviour
{
    public Text EndStory;
    public Text Skip;
    public Button Exit;
    [SerializeField]
    private Text[] feedBacks ;
    public TextAsset EndStoryRes;
    [SerializeField]
    private string[] subString;

    private void Start()
    {
        feedBacks = this.transform.GetChild(0).GetComponentsInChildren<Text>();
        if (EndStoryRes != null)
        {
            subString = EndStoryRes.text.Split('\n');
        }
        closeTexts();
        StartCoroutine(showTheEndStory());

    }
    private IEnumerator showTheEndStory() {

        for (int i = 0; i < subString.Length; i ++) {
            EndStory.text += subString[i] + '\n';
            yield return new WaitForSeconds(2f);
            if (i == 6) {
                EndStory.text = "";
            }
        }
        Skip.text = "按Z繼續";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        closeTexts();
        EndStory.gameObject.SetActive(false);
        Skip.gameObject.SetActive(false);
        StartCoroutine(showMissionProcess());
    }
    private void closeTexts()
    {
        for (int i = 3; i < feedBacks.Length; i++) {
            feedBacks[i].gameObject.SetActive(false);
        }
    }
    private IEnumerator showMissionProcess() {
        for (int i = 3; i < feedBacks.Length; i++)
        {
            feedBacks[i].gameObject.SetActive(true);
            StartCoroutine(counting(feedBacks[i], 50));
            yield return new WaitForSeconds(5);

        }
    }
    private IEnumerator counting(Text temp , int goal) {
        int count = 0;
        string storeMessage = temp.text;
        while (count <= goal) {
            temp.text = storeMessage + count.ToString();
            count++;
            //temp.text = storeMessage.text;
            yield return new WaitForSeconds(0.01f);
        }
        

    }
}
