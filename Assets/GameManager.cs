
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public GameObject ghostCamera;

    // Start is called before the first frame update
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

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
