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
    private TextAsset EndStoryRes;
    [SerializeField]
    private string[] subString;
    

    private void Start()
    {
        if (GameSceneManager.instance.PassSceneNum == 4)
        {
            EndStoryRes = Resources.Load<TextAsset>("Story/EndStory");
        }
        else {
            EndStoryRes = Resources.Load<TextAsset>("Story/Escape");
        }
        
        
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

        feedBacks[3].gameObject.SetActive(true);
        yield return counting(feedBacks[3], MonsterSystem.instance.DeadMonsterNum);
        feedBacks[4].gameObject.SetActive(true);
        yield return counting(feedBacks[4], MonsterSystem.instance.DeadBossNum);
        feedBacks[5].gameObject.SetActive(true);
        yield return counting(feedBacks[5], GameSceneManager.instance.PassSceneNum);
        feedBacks[6].gameObject.SetActive(true);
        yield return counting(feedBacks[6], calculateMissionScore());



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
    private int calculateMissionScore() {
        float sum = MonsterSystem.instance.DeadMonsterNum * 0.2f / MonsterSystem.instance.MonsterNum
            + MonsterSystem.instance.DeadBossNum * 0.4f / 3
            + GameSceneManager.instance.PassSceneNum * 0.4f / 4;

        return (int)sum;

    }
}
