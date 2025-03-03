using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private Canvas endGameCanvas;
    private GameObject player_;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endGameCanvas.enabled = false;
        player_ = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_.gameObject.activeInHierarchy)
        {
            ExitGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
