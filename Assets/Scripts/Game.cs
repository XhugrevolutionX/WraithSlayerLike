using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    private Camera _mainCamera;
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
            endGameCanvas.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void AddScore(int score)
    {
        _score += score;
        scoreText.text = "Score : " + _score.ToString();
    }
}
