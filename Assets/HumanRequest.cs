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
    
    private void Awake()
    {
        dialogue = GetComponentInChildren<DialogueBubble>();
        commentName = GetComponent<Interactable>().commentName;
    }

    public bool isCorrectDelivery(string n)
    {
         return n == requestItemName;
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
            
            GetComponent<Interactable>().commentName = GetComponent<Interactable>().commentName + "_finished";
            commentName = GetComponent<Interactable>().commentName;
            dialogue.Show(commentName+"_correct");
        }

        if (requestName == "day1Record")
        {
            //unlock upper level
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
