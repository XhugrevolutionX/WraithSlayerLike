using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    
    [SerializeField] private GameObject attack1;
    [SerializeField] private GameObject attack2;
    [SerializeField] private GameObject attack3;
    [SerializeField] private GameObject attack4;
    [SerializeField] private GameObject attack5;
    [SerializeField] private float attackDelay;
    [SerializeField] private float deathDelay;
    [SerializeField] private Game game;
    private Animator _animator;
    private bool _canAttack;
    private Coroutine _attackDelayCoroutine;
    private Coroutine _attackCoroutine;    
    private Coroutine _stunCoroutine;
    private Coroutine _deathCoroutine;

    private int _state = 0;
    
    [SerializeField] private float health = 3;
    public bool beenHit = false;
    private float _hitStun = 0.5f;
    
    [SerializeField] private float moveRangeX = 10;
    [SerializeField] private float speedX = 5;
    [SerializeField] private float moveRangeY = 1;
    [SerializeField] private float speedY = 2;
    private Vector2 _startPosition;
    
    private GameObject _target;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canAttack = false;
        _attackCoroutine = StartCoroutine(AttackDelay());
        
        _startPosition = transform.position;
        _animator = GetComponent<Animator>();
        
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!beenHit)
        {
            Move();
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
        
        if (_canAttack)
        {
            int rnd = 0;
            switch (_state)
            {
                case 0:
                    rnd = Random.Range(0, 2); 
                    break;
                case 1:
                    rnd = Random.Range(2, 5);
                    break;
                case 2:
                    rnd = Random.Range(0, 5);
                    break;
                    
            }
            Fire(rnd);
            _animator.SetTrigger("Fire");
        }
    }

    private void Move()
    {
        if (transform.position.x >= _startPosition.x + moveRangeX || transform.position.x <= _startPosition.x - moveRangeX)
        {
            speedX = -speedX;
        }
        
        if (transform.position.y >= _startPosition.y + moveRangeY || transform.position.y <= _startPosition.y - moveRangeY)
        {
            speedY = -speedY;
        }
        
        transform.Translate(Vector2.right * (speedX * Time.deltaTime));
        transform.Translate(Vector2.up * (speedY * Time.deltaTime));
    }

    private void Fire(int attackIdx)
    {
        switch (attackIdx)
        {
           case 0:
               Instantiate(attack1, new Vector2(transform.position.x, transform.position.y - 2), Quaternion.identity);
               break; 
           case 1:
               float dir;
               if (_target.transform.position.x > transform.position.x)
               {
                   dir = -1;
               }
               else
               {
                   dir = 1;
               }
               
               Instantiate(attack2, new Vector2(_target.transform.position.x + (6 * dir) , _target.transform.position.y), Quaternion.identity);
               break;
           case 2:
               Instantiate(attack3, new Vector2(transform.position.x, transform.position.y - 2), Quaternion.identity);
               break;
           case 3:
               if (_attackCoroutine != null)
               {
                   StopCoroutine(_attackCoroutine);
               }
               _attackCoroutine = StartCoroutine(Boulders());
               break;
           case 4:
               if (_attackCoroutine != null)
               {
                   StopCoroutine(_attackCoroutine);
               }
               _attackCoroutine = StartCoroutine(Fountain());
               break;
        }
        _canAttack = false;

        if (_attackDelayCoroutine != null)
        {
            StopCoroutine(_attackDelayCoroutine);
        }
        _attackDelayCoroutine = StartCoroutine(AttackDelay());
    }


    public void Hit(float damage)
    {
        health -= damage;
        game.AddScore(1);
        beenHit = true;
        _animator.SetTrigger("Damaged");
        
        if (health <= 0)
        {
            if (_deathCoroutine != null)
            {
                StopCoroutine(_deathCoroutine);
            }
            _deathCoroutine = StartCoroutine(DeathDelay());
        }
        else
        {
            _state++;
            switch (_state)
            {
                case 0:
                    attackDelay = 3;
                    break;
                case 1:
                    attackDelay = 2.5f;
                    break;
                case 2:
                    attackDelay = 1.5f;
                    break;
            }
            if (_stunCoroutine != null)
            {
                StopCoroutine(_stunCoroutine);
            }
            _stunCoroutine = StartCoroutine(HitStun());
        }
    }
    
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
    
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }
    
    private IEnumerator HitStun()
    {
        yield return new WaitForSeconds(_hitStun);
        beenHit = false;
    }
    
    private IEnumerator Boulders()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(attack4, new Vector2(_target.transform.position.x + Random.Range(-2, 3), 7), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }   
    
    private IEnumerator Fountain()
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(attack5, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}