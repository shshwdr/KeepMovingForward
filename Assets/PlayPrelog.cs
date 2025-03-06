using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayPrelog : Singleton<PlayPrelog>
{
    public GameObject dog;
    public GameObject player;
    public GameObject[] prelogList;
    public GameObject[] epilogList;

    private bool mouseClick = false;
    //public float waitTime = 5;
    private int index;
    public CameraTrigger trigger;
    public bool caughtBall = false;

    public bool isPlayingPrelog;

    public GameObject tutorial1;
    public GameObject tutorial2;

    public GameObject thanks;

    private void Start()
    {
        thanks.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.Restart();
        });
    }

    public void dropBall()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
    }
    // Start is called before the first frame update
    public void ShowPrelog()
    {
        isPlayingPrelog = false;
        if (SceneManager.Instance.currentDay == 0)
        {
            //trigger.forceTrigger();
            tutorial1.GetComponentInChildren<TMP_Text>(true).text = CSVLoader.Instance.DialogueInfoMap["tutorial_1"][0].text;
            tutorial2.GetComponentInChildren<TMP_Text>(true).text = CSVLoader.Instance.DialogueInfoMap["tutorial_2"][0].text;
            isPlayingPrelog = true;
            index = 0;
            dog.SetActive(false);
            player.SetActive(false);
            
            foreach (var ob in prelogList)
            {
                ob.SetActive(false);
            }
            
            StartCoroutine(ShowOne(prelogList));
        }
    }

    public void ShowEpilog()
    {

        if (SceneManager.Instance.currentDay == 4)
        {
            
        isPlayingPrelog = true;
        index = 0;
        dog.SetActive(false);
        player.SetActive(false);
            
        foreach (var ob in epilogList)
        {
            ob.SetActive(false);
        }
            
        StartCoroutine(ShowOne(epilogList));
        }
    }

    IEnumerator ShowOne(GameObject[] prelogList)
    {
        if (index < prelogList.Length)
        {
            
            mouseClick = false;
            prelogList[index].SetActive(true);
            bool hasNextLine = true;

            while (hasNextLine)
            {
                hasNextLine = false;
                foreach (var interactable in prelogList[index].GetComponentsInChildren<Interactable>())
                {
                    if (CSVLoader.Instance.DialogueInfoMap.ContainsKey(interactable.commentName) && CSVLoader.Instance.dialogueIndex.GetValueOrDefault(interactable.commentName,0) < CSVLoader.Instance.DialogueInfoMap[interactable.commentName].Count)
                    {
                        var dialogue = interactable.GetComponentInChildren<DialogueBubble>();
                        dialogue.Show(interactable.commentName, false);
                        if (CSVLoader.Instance.dialogueIndex[interactable.commentName] <
                            CSVLoader.Instance.DialogueInfoMap[interactable.commentName].Count)
                        {
                            hasNextLine = true;
                            //CSVLoader.Instance.dialogueIndex [interactable.commentName]++;
                        }
                    }
                }
            
                yield return new WaitUntil( () => mouseClick);
                mouseClick = false;
            }


            if (index == 4 && prelogList == this.prelogList)
            {
                isPlayingPrelog = false;
                
                tutorial1.SetActive(true);
                yield return new WaitUntil( () => caughtBall);
                isPlayingPrelog = true;
                tutorial2.SetActive(false);
            }
            
            
            prelogList[index].SetActive(false);
            index++;
            StartCoroutine(ShowOne(prelogList));
        }
        else
        {
            if (prelogList == this.prelogList)
            {
                
                player.SetActive(true);
                dog.SetActive(true);
                SceneManager.Instance.NextDay();
            }
            else
            {
                prelogList[index-1].SetActive(true);
                prelogList[index-1].GetComponentInChildren<PlayerController>().GetComponentInChildren<SpriteRenderer>()
                    .DOFade(0, 10f);
                StartCoroutine(test());
                thanks.SetActive(true);
            }
        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(10);
        
        epilogList[index-1].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClick = true;
        }
    }
}
