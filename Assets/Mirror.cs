using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject dirt;

    public GameObject water;

    public void Clean()
    {
        dirt.SetActive(false);
        water.SetActive(false);
        GetComponent<Interactable>().commentName = "mirrorClean";
        GetComponent<Interactable>().actionName = "";
        GetComponent<HumanRequest>().finishRequest();
    }

    public void AddWater()
    {
        water.SetActive(true);
        GetComponent<Interactable>().commentName = "mirrorWater";
        GetComponent<Interactable>().actionName = "mirrorWater";
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
