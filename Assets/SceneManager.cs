using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : Singleton<SceneManager>
{
    public Door[] doors;

    public int currentDay = 1;
    public int startDay = 1;
    public GameObject[] days;

    private FMOD.Studio.EventInstance finalMusic;

    public DayController dayController;
    // Start is called before the first frame update
    void Awake()
    {
        currentDay = startDay;
    }

    private void Start()
    {
        LoadDay();
        
        Nextday.GetComponentInChildren<Button>().onClick.AddListener(() => { NextDay();});     
    }

    public Transform CurrentDay()
    {
        return days[currentDay].transform;
    }
    void LoadDay()
    {
        
        foreach (var day in days)
        {
            day.SetActive(false);
        }
        days[currentDay].SetActive(true);

        PlayPrelog.Instance.player.GetComponent<PlayerController>().StartLevel(days[currentDay].transform);
        PlayPrelog.Instance.dog.GetComponent<DogClickController>().StartLevel(days[currentDay].transform);

        PlayPrelog.Instance.ShowPrelog();
        PlayPrelog.Instance.ShowEpilog();

        if (currentDay > 3)
        {
            Invoke("FinalMusic", 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentDay > 0)
            {
                currentDay--;
                LoadDay();
            }
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
             NextDay();
        }

        if (currentDay > 3)
            {
            GameManager.Instance.StopMusic();          
        }
    }

    public GameObject Nextday;
    public void CheckFinish()
    {
        bool allFinished = true;
        foreach (var request in FindObjectsOfType<HumanRequest>())
        {
            if (!request.finished)
            {
                 allFinished = false;
                 break;
            }
        }

        if (allFinished)
        {
            Nextday.SetActive(true);
        }
    }
    
    public void NextDay()
    {
        
        Nextday.SetActive(false);
        if (currentDay < days.Length - 1)
        {
            currentDay++;
            dayController.StartDay();
            LoadDay();
            
        }
    }

    private void FinalMusic()
    {
        finalMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/mus_gameplay_level_4");
        finalMusic.start();
    }

    public void FinalMusicStop()
    {
        finalMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        finalMusic.release();
    }
}
