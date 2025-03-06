using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    public CanvasGroup dialogue;
    private Sequence dialogueDotween;

    private float waitTime = 0.3f;
    private float waitTimer = 0;
    public void Show(string dialogueId,bool willHide = true)
    {
        if (waitTimer > 0)
        {
            return;
        }

        waitTimer = waitTime;
        
        if (dialogueId != "" &&
            CSVLoader.Instance.dialogueIndex.ContainsKey(dialogueId))
        {
            if (dialogueDotween != null && dialogueDotween.IsActive())
                dialogueDotween.Kill();

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_ghost_dialogue");
            dialogueDotween = DOTween.Sequence();
            dialogueDotween.Append(dialogue.DOFade(1f, 0.5f));
            dialogueDotween.AppendInterval(5);
            if (willHide)
            {
                dialogueDotween.Append(dialogue.DOFade(0f, 0.5f));
            }
            var texts = CSVLoader.Instance.DialogueInfoMap[dialogueId];
            var index = CSVLoader.Instance.dialogueIndex[dialogueId];

            dialogue.GetComponentInChildren<TMP_Text>().text = texts[index].text;
            index++;
            
            
            if (willHide)
            {
                if (index >= texts.Count)
                {
                    index = 0;
                }
            }

            CSVLoader.Instance.dialogueIndex[dialogueId] = index;
        }
    }

    private void Update()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
    }
}
