using UnityEngine;

public class Game : MonoBehaviour
{
    private GameObject player_;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_.gameObject.activeInHierarchy)
        {
            Application.Quit();
        }
    }
}
