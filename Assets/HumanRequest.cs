using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HumanRequest : MonoBehaviour
{
    public bool finished = false;

    public string requestItemName;
    
    DialogueBubble dialogue;
    private string commentName;

    public string requestName;
    public HumanRequest nextRequestToFinish;
    public int requireItemCount = 1;
    private int getItemCount = 0;
    public GameObject finishedItem;
    public GameObject unfinishedItem;
    private void Awake()
    {
        dialogue = GetComponentInChildren<DialogueBubble>();
        commentName = GetComponent<Interactable>().commentName;
    }

    public bool isCorrectDelivery(string n)
    {
         return n == requestItemName;
    }

    public void deliverItem()
    {
        getItemCount++;
        if (getItemCount >= requireItemCount)
        {
            finishRequest();
        }
        
        
        
        switch (requestName)
        {
            case "box":
                transform.DOPunchScale(transform.localScale, 0.2f, 10, 1f);
                break;
        }
    }
    
    public void finishRequest()
    {
        if (finished)
        {
            return;
        }

        finished = true;

        if (dialogue)
        {
            
            dialogue.Show(commentName+"_correct");
            
            
            GetComponent<Interactable>().commentName = GetComponent<Interactable>().commentName + "_finished";
            commentName = GetComponent<Interactable>().commentName;
        }

        if (requestName == "day1Record")
        {
            //unlock upper level
        }

        switch (requestName)
        {
            case "day1Record":
                break;
        }

        
        if (unfinishedItem)
        {
            unfinishedItem.SetActive(false);
        }
        if (finishedItem)
        {
            finishedItem.SetActive(true);
        }

        if (nextRequestToFinish)
        {
            nextRequestToFinish.deliverItem();
        }

       SceneManager.Instance. CheckFinish();
    }

    public void wrongDeliver()
    {

        if (dialogue)
        {
            dialogue.Show(commentName + "_wrong");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
