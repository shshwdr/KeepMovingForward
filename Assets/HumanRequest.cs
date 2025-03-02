using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRequest : MonoBehaviour
{
    private bool finished = false;

    public void finishRequest()
    {
        if (finished)
        {
            return;
        }

        finished = true;
        GetComponent<Interactable>().commentName = GetComponent<Interactable>().commentName + "_finished";
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
