using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    public CanvasGroup dialogue;
    private Sequence dialogueDotween;
    
    public void Show(string dialogueId,bool willHide = true)
    {
        if (dialogueId != "" &&
            CSVLoader.Instance.dialogueIndex.ContainsKey(dialogueId))
        {
            if (dialogueDotween != null && dialogueDotween.IsActive())
                dialogueDotween.Kill();


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
}
