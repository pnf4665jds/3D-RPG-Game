using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryContoller : MonoBehaviour
{
    private GameObject player;
    public List<storyTypeBase> loadingFinishStory = new List<storyTypeBase>();
    public List<storyTypeBase> enterBossFieldStory = new List<storyTypeBase>();
    public List<storyTypeBase> bossDeadStory = new List<storyTypeBase>();
    public List<storyTriggerType> emergencyStory = new List<storyTriggerType>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StoryFlowControl());
    }

    // Update is called once per frame
    private IEnumerator StoryFlowControl() {

        yield return new WaitUntil(() => GameSceneManager.instance.IsLoadingFinish);

        foreach (storyTypeBase story in loadingFinishStory) {
            StartCoroutine(story.Play());
        }

        yield return new WaitUntil(() => player.GetComponent<Player>().GetisInBoss()); //進入boss場景

        foreach (storyTypeBase story in enterBossFieldStory)
        {
            StartCoroutine(story.Play());
        }
        yield return new WaitUntil(() => MonsterSystem.instance.IsBossDead); //進入boss場景

        foreach (storyTypeBase story in bossDeadStory)
        {
            StartCoroutine(story.Play());
        }

    }
}
