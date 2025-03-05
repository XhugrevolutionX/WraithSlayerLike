using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameObject _player;
    private int _score;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _score = 0;
        endGameCanvas.enabled = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.gameObject.activeInHierarchy)
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


    public void AddScore(int score)
    {
        _score += score;
        scoreText.text = "Score : " + _score.ToString();
    }
}
