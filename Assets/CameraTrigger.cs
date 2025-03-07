using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject myCamera;
    public GameObject otherCamera;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Dog"))
        {
            otherCamera.SetActive(false);
            myCamera.SetActive(true);
        }
    }
}
