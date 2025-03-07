
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public GameObject ghostCamera;

    private FMOD.Studio.EventInstance gameplayMusic;
    // Start is called before the first frame update

    void Start()
    {
        gameplayMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/mus_gameplay_level_1");
        gameplayMusic.start();
    }
    void Awake()
    {
        CSVLoader.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (SceneManager.Instance.currentDay == 0 || SceneManager.Instance.currentDay > 3)
        {
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(ghostCamera.activeSelf)
                {
                    ghostCamera.SetActive(false);
                }

                else
                {
                    ghostCamera.SetActive(true);
                }
            }
        }
        
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        gameplayMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameplayMusic.release();
        SceneManager.Instance.FinalMusicStop();
    }

    public void StopMusic()
    {
        gameplayMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameplayMusic.release();
    }
}
