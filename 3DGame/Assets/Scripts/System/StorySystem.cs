using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class StorySystem : Singleton<StorySystem>
{
    private bool StoryShow = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StoryFlowControl());
    }

    // Update is called once per frame
    public IEnumerator StoryFlowControl() {
        for (int i = 1; i <= SceneManager.sceneCount; i++) {
            yield return new WaitUntil(() => GameSceneManager.instance.IsLoadingFinish);
            //StoryShow = true; //告知玩家，讓玩家call GameSystem to change to the Story mode
            GameSystem.instance.changeModeStory();
            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setloadingFinshFile(i);
            yield return new WaitForSecondsRealtime(3);
            //StoryShow = false;
            GameSystem.instance.changeModeFollowPlayer();
            yield return new WaitUntil(() => MonsterSystem.instance.IsBossDead);
            // StoryShow = true; //告知玩家，讓玩家call GameSystem to change to the Story mode
            GameSystem.instance.changeModeStory();
            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setbossDeadFile(i);
            yield return new WaitForSecondsRealtime(3);
            //StoryShow = false;
            GameSystem.instance.changeModeFollowPlayer();
        }
    }
   
    public bool isTheStoryShow() {
        return StoryShow;
    }
}
