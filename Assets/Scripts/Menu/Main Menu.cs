using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    private static bool _hasInitialized = false; 

    void Start()
    {
        if (!_hasInitialized)
        {
            ApplyDefaultSettings();
            _hasInitialized = true;
        }
    }
    
    private void ApplyDefaultSettings()
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = true;
        QualitySettings.SetQualityLevel(2);
        AudioListener.volume = 1.0f;
    }
    
    public void StartGame(int bossIndex)
    {
        BossManager.SelectedBossIndex = bossIndex;
        SceneManager.LoadScene(2);
    }
    
    public void CloseGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    public void OpenSettings()
    {
        SceneManager.LoadScene(1);
    }
}
