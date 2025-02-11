using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private float damage = 1;
    [SerializeField] private float iframesDelay = 1;
    private CapsuleCollider2D _collision;
    private bool _isAttackReady;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isAttackReady = false;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collision = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Powershard"))
        {
            if (power < 3)
            {
                Destroy(other.gameObject);
                power++;
                if (power == 3)
                {
                    _isAttackReady = true;
                }
            }
        }
        
        if (other.gameObject.CompareTag("Death") || other.gameObject.CompareTag("BossAttacks"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            Time.timeScale = 0;
        }
        
        if (other.gameObject.CompareTag("Boss"))
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (_isAttackReady)
            {
                boss.Hit(damage);
                power = 0;
                _collision.enabled = false;
                StartCoroutine("Iframes");
                _isAttackReady = false;
            }
            else
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
                Time.timeScale = 0;
            }
        }
        
        switch (power)
        {
            case 0:
                _spriteRenderer.color = Color.white;
                break;
            case 1: 
                _spriteRenderer.color = Color.yellow;
                break;
            case 2: 
                _spriteRenderer.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
                break;
            case 3:
                _spriteRenderer.color = Color.red;
                break;
        }
    }
    
    private IEnumerator Iframes()
    {
        yield return new WaitForSeconds(iframesDelay);
        _collision.enabled = true;
    }
}
