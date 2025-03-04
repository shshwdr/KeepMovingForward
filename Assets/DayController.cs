using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DayController : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    public TMP_Text dayLabel;
    // Start is called before the first frame update
    public void StartDay()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        dayLabel.text ="Day "+ SceneManager.Instance.currentDay;
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(2);
        
        _canvasGroup.DOFade(0, 1);
        _canvasGroup.blocksRaycasts = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}