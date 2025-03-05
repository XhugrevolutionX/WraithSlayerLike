using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private float damage = 1;
    [SerializeField] private float iframesDelay = 1;
    [SerializeField] private AudioSource powerShardsSoundEffect;
    [SerializeField] private AudioSource bossSoundEffect;
    [SerializeField] private Canvas endGameCanvas;
    private PlayerMovement _playerMovement;
    private bool _canBeHit;
    private bool _isAttackReady;
    private SpriteRenderer _spriteRenderer;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canBeHit = true;
        _isAttackReady = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
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
                powerShardsSoundEffect.Play();
                power++;
                if (power == 3)
                {
                    _isAttackReady = true;
                }
            }
        }

        if (_canBeHit)
        {
            if (other.gameObject.CompareTag("Death") || other.gameObject.CompareTag("BossAttacks"))
            {
                gameObject.SetActive(false);
                Time.timeScale = 0;
                endGameCanvas.enabled = true;
            }
        
            if (other.gameObject.CompareTag("Boss"))
            {
                Boss boss = other.gameObject.GetComponent<Boss>();
                if (_isAttackReady)
                {
                    boss.Hit(damage);
                    power = 0;
                    bossSoundEffect.Play();
                    _canBeHit= false;
                    StartCoroutine("Iframes");
                    _isAttackReady = false;
                    _playerMovement.Impulse();
                }
                else
                {
                    gameObject.SetActive(false);
                    Time.timeScale = 0;
                    endGameCanvas.enabled = true;
                }
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
        _canBeHit = true;
    }
}
