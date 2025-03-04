using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRequest : MonoBehaviour
{
    private bool finished = false;

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
            nextRequestToFinish.finishRequest();
        }
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
