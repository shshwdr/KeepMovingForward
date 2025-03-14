using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactDistance = 2f; // Max distance for interaction
    private Transform player; // Reference to the player
    public Collider2D collider;
    public string name;
    public string actionName;
    public string commentName;
    public SpriteRenderer sprite;
    public GameObject outline;
    public int sortLayer;
    public int sortOrder;
    private Animator animator;
    void Start()
    {
        player = PlayPrelog.Instance.player.transform;
        collider = GetComponentInChildren<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite)
        {
            
            sortLayer = sprite.sortingLayerID;
            sortOrder = sprite.sortingOrder;
        }
        animator = GetComponentInChildren<Animator>();
    }

    void OnMouseDown()
    {
         
        if (PlayPrelog.Instance.isPlayingPrelog)
        {
            return;
        }
        if (IsPlayerCloseEnough())
        {
            Interact();
        }
        else
        {
            Debug.Log("You are too far away!");
        }
    }

    private bool IsPlayerCloseEnough()
    {
        return Vector2.Distance(player.position, transform.position) <= interactDistance;
    }

    public virtual void Interact()
    {
        switch (actionName)
        {
            case "firstBall":
                PlayPrelog.Instance.dropBall();
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_ghost_interact");
                GetComponent<Rigidbody2D>().isKinematic = false;              
                break;
            case "ball":
            case "trophy":
            case "duster":
            case "dust":
            case "spray":

                if (GetComponent<Rigidbody2D>().isKinematic)
                {
                    
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_ghost_interact");

                    if (CSVLoader.Instance.dialogueIndex.ContainsKey(commentName + "_dropped"))
                    {
                        
                        commentName += "_dropped";
                    }
                }

                break;
            case "cupboardDoor":
                gameObject.SetActive(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_cupboard_door_open");
                break;
            case "tissueBox":
                FindObjectOfType<PlayerController>().tissue.SetActive(true);
                actionName = "";
                break;
            case "mirrorWater":
                if (FindObjectOfType<PlayerController>().tissue.activeSelf)
                {
                    FindObjectOfType<PlayerController>().tissue.SetActive(false);
                    GetComponent<Mirror>().Clean();
                }

                break;
        }
        Debug.Log("Interacted with: " + gameObject.name);

    }

    public void DogInteract(DogClickController dog)
    {
        switch (actionName)
        {
            case "door":


                if (SceneManager.Instance.currentDay != 0)
                {
                    foreach (var door in FindObjectsOfType<Door>())
                    {
                        if (door.gameObject != this.gameObject)
                        {
                            //dog.transform.position = door.transform.position;
                            dog.rb.position = door.transform.position;
                            dog.currentLayer = door.layer;
                            dog.interactionTime = 1f;
                        }
                    }
                }
                
                break;
            
            case "firstBall":
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_pick_item");
                PlayPrelog.Instance.caughtBall = true;
                dog.animator.SetTrigger("pick");
                    
                dog.interactionTime = 0.5f;
                if (dog.holdingItem)
                {
                    dog.holdingItem.DogDrop(dog);
                }

                StartCoroutine(pick(dog));
                break;
            case "ball":
                case "trophy":
                case "duster":
                case "spray":

                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_pick_item");
                dog.animator.SetTrigger("pick");
                    
                    dog.interactionTime = 0.5f;
                if (dog.holdingItem)
                {
                    dog.holdingItem.DogDrop(dog);
                }

                    StartCoroutine(pick(dog));
                
                break;
            case "dust":
                
                dog.interactionTime = 0.5f;
                if (dog.holdingItem && dog.holdingItem.name == "duster")
                {
                    gameObject.SetActive(false);
                    dog.holdingItem.animator.SetTrigger("Use");
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_clean_dust");
                    dog.holdingItem.GetComponentInChildren<HumanRequest>().deliverItem();
                }

                break;
            case "mirrorDirty":
                
                dog.interactionTime = 0.5f;
                if (dog.holdingItem && dog.holdingItem.name == "spary")
                {
                    //gameObject.SetActive(false);
                    dog.holdingItem.animator.SetTrigger("Use");
                    GetComponent<Mirror>().AddWater();
                }
                break;
            // case "human":
            //     if (GetComponent<HumanRequest>().isCorrectDelivery(dog.holdingItem.name))
            //     {
            //         
            //     }
            //     else
            //     {
            //         GetComponent<HumanRequest>().wrongDeliver();
            //     }
            //     break;
        }

        if (dog.holdingItem && GetComponent<HumanRequest>())
        {
            if (GetComponent<HumanRequest>().isCorrectDelivery(dog.holdingItem.name))
            {
                GetComponent<HumanRequest>().deliverItem();
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_put_in_box");
                dog.holdingItem.gameObject.SetActive(false);
                dog.holdingItem = null;
            }
            else
            {
                GetComponent<HumanRequest>().wrongDeliver();
            }
        }
        else
        {
            if (GetComponent<HumanRequest>())
            {
                
                GetComponent<HumanRequest>().wrongDeliver();
            }
        }

    }

    public void DogDrop(DogClickController dog)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponentInChildren<Collider2D>().enabled = true;
        sprite.sortingLayerID = sortLayer;
        sprite.sortingOrder = sortOrder;
        GetComponent<Rigidbody2D>().simulated = true;
        transform.parent = SceneManager.Instance.CurrentDay();
        dog.holdingItem = null;
    }
    
    IEnumerator pick(DogClickController dog)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_pick_item");
        yield return new WaitForSeconds(0.6f);
        
        transform.rotation = quaternion.identity;
        transform.parent = dog.transform;
        transform.position = dog.mouth.position;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponentInChildren<Collider2D>().enabled = false;
        sprite.sortingLayerName = "Dog";
        sprite.sortingOrder = -1;
        dog.holdingItem = this;
    }

}
