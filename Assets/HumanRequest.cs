using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRequest : MonoBehaviour
{
    private bool finished = false;

    DialogueBubble dialogue;
    private string commentName;
    
    private void Awake()
    {
        dialogue = GetComponentInChildren<DialogueBubble>();
        commentName = GetComponent<Interactable>().commentName;
    }
    public void finishRequest()
    {
        if (finished)
        {
            return;
        }

        finished = true;
        GetComponent<Interactable>().commentName = GetComponent<Interactable>().commentName + "_finished";
        commentName = GetComponent<Interactable>().commentName;
        dialogue.Show(commentName+"_correct");
    }

    public void wrongDeliver()
    {
        
        dialogue.Show(commentName+"_wrong");
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
