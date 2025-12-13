using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private static readonly int IsMovingId = Animator.StringToHash("isMoving");
    private static readonly int FireId = Animator.StringToHash("Fire");
    private static readonly int DamagedId = Animator.StringToHash("Damaged");

    [SerializeField] protected GameObject attack1;
    [SerializeField] protected GameObject attack2;
    [SerializeField] protected GameObject attack3;
    [SerializeField] protected GameObject attack4;
    [SerializeField] protected GameObject attack5;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float deathDelay;
    [SerializeField] protected float health = 3;
    [SerializeField] protected float moveRangeX = 10;
    [SerializeField] protected float speedX = 5;
    [SerializeField] protected float moveRangeY = 1;
    [SerializeField] protected float speedY = 2;
    
    protected Game Game;
    protected Animator Animator;
    protected bool CanAttack;
    protected int State;
    protected Vector2 StartPosition;
    protected GameObject Target;
    
    protected Coroutine AttackDelayCoroutine;
    protected Coroutine AttackCoroutine;    
    protected Coroutine StunCoroutine;
    protected Coroutine DeathCoroutine;
    
    public bool beenHit;
    private float _hitStun = 0.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!beenHit)
        {
            Move();
            Animator.SetBool(IsMovingId, true);
        }
        else
        {
            Animator.SetBool(IsMovingId, false);
        }
        
        if (CanAttack)
        {
            Fire(ChooseAttack());
            Animator.SetTrigger(FireId);
        }
    }

    protected virtual int ChooseAttack()
    {
        return 0;
    }

    protected virtual void Move()
    {
        
    }

    private void Fire(int attackIdx)
    {
        switch (attackIdx)
        {
           case 0:
               //Homing Shot
               Instantiate(attack1, new Vector2(transform.position.x, transform.position.y - 2), Quaternion.identity);
               break; 
           case 1:
               //Laser Wall
               float dir;
               if (Target.transform.position.x > transform.position.x)
               {
                   dir = -1;
               }
               else
               {
                   dir = 1;
               }
               
               Instantiate(attack2, new Vector2(Target.transform.position.x + (6 * dir) , Target.transform.position.y), Quaternion.identity);
               break;
           case 2:
               //Exploding attack
               Instantiate(attack3, new Vector2(transform.position.x, transform.position.y - 2), Quaternion.identity);
               break;
           case 3:
               //Boulder
               if (AttackCoroutine != null)
               {
                   StopCoroutine(AttackCoroutine);
               }
               AttackCoroutine = StartCoroutine(Boulders());
               break;
           case 4:
               //Fountain
               if (AttackCoroutine != null)
               {
                   StopCoroutine(AttackCoroutine);
               }
               AttackCoroutine = StartCoroutine(Fountain());
               break;
        }
        CanAttack = false;

        if (AttackDelayCoroutine != null)
        {
            StopCoroutine(AttackDelayCoroutine);
        }
        AttackDelayCoroutine = StartCoroutine(AttackDelay());
    }


    public virtual void Hit(float damage)
    {
        health -= damage;
        Game.AddScore(1);
        beenHit = true;
        Animator.SetTrigger(DamagedId);
        
        if (health <= 0)
        {
            if (DeathCoroutine != null)
            {
                StopCoroutine(DeathCoroutine);
            }
            DeathCoroutine = StartCoroutine(DeathDelay());
        }
        else
        {
            State++;
        }
    }
    
    protected IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
    
    protected IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        CanAttack = true;
    }
    
    protected IEnumerator HitStun()
    {
        yield return new WaitForSeconds(_hitStun);
        beenHit = false;
    }
    
    private IEnumerator Boulders()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(attack4, new Vector2(Target.transform.position.x + Random.Range(-2, 3), 7), Quaternion.identity);
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