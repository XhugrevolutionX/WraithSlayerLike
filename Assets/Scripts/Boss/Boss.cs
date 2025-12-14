using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private static readonly int IsMovingId = Animator.StringToHash("isMoving");
    private static readonly int FireId = Animator.StringToHash("Fire");
    private static readonly int DamagedId = Animator.StringToHash("Damaged");

    [Header("Attack Phases")] 
    [SerializeField] private List<BossAttack> phase1Attacks;
    [SerializeField] private float phase1AttackDelay;

    [SerializeField] private List<BossAttack> phase2Attacks;
    [SerializeField] private float phase2AttackDelay;

    [SerializeField] private List<BossAttack> phase3Attacks;
    [SerializeField] private float phase3AttackDelay;

    [Header("Stats")] 
    [SerializeField] private float health = 3;
    [SerializeField] private float deathDelay;

    [Header("Movement")] 
    [SerializeField] private float moveRangeX = 10;
    [SerializeField] private float speedX = 5;
    [SerializeField] private float moveRangeY = 1;
    [SerializeField] private float speedY = 2;

    private Game game;
    private Animator animator;

    private List<BossAttack> currentAttackPool;
    private bool canAttack;
    private float currentAttackDelay;
    private int state;
    private Vector2 startPosition;
    private GameObject target;

    private Coroutine attackDelayCoroutine;
    private Coroutine deathCoroutine;
    private Coroutine stunCoroutine;

    public bool beenHit;
    private float hitStun = 0.5f;
    
    public Action deathAction;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        game = GetComponentInParent<Game>();

        UpdateAttackParameters();

        canAttack = false;
        attackDelayCoroutine = StartCoroutine(AttackDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!beenHit)
        {
            Move();
            animator.SetBool(IsMovingId, true);
        }
        else
        {
            animator.SetBool(IsMovingId, false);
        }

        if (canAttack)
        {
            FireAttack();
            animator.SetTrigger(FireId);
        }
    }

    private void UpdateAttackParameters()
    {
        switch (state)
        {
            case 0:
                currentAttackPool = phase1Attacks;
                currentAttackDelay = phase1AttackDelay;
                break;
            case 1:
                currentAttackPool = phase2Attacks;
                currentAttackDelay = phase2AttackDelay;
                break;
            case 2:
                currentAttackPool = phase3Attacks;
                currentAttackDelay = phase3AttackDelay;
                break;
            default:
                // Fallback to phase 1 if state goes too high
                currentAttackPool = phase1Attacks;
                currentAttackDelay = phase1AttackDelay;
                break;
        }
    }

    private void FireAttack()
    {
        if (currentAttackPool == null || currentAttackPool.Count == 0) return;

        int index = Random.Range(0, currentAttackPool.Count);
        BossAttack attackPrefab = currentAttackPool[index];

        if (attackPrefab != null)
        {
            BossAttack newAttack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
            newAttack.Fire(transform, target.transform);
        }

        canAttack = false;
        if (attackDelayCoroutine != null) StopCoroutine(attackDelayCoroutine);
        attackDelayCoroutine = StartCoroutine(AttackDelay());
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (game != null) game.AddScore(1);
        beenHit = true;
        animator.SetTrigger(DamagedId);

        if (health <= 0)
        {
            if (deathCoroutine != null) StopCoroutine(deathCoroutine);
            deathCoroutine = StartCoroutine(DeathDelay());
        }
        else
        {
            state++;
            UpdateAttackParameters();

            if (stunCoroutine != null) StopCoroutine(stunCoroutine);
            stunCoroutine = StartCoroutine(HitStun());
        }
    }

    private void Move()
    {
        if (transform.position.x >= startPosition.x + moveRangeX ||
            transform.position.x <= startPosition.x - moveRangeX)
        {
            speedX = -speedX;
        }

        if (transform.position.y >= startPosition.y + moveRangeY ||
            transform.position.y <= startPosition.y - moveRangeY)
        {
            speedY = -speedY;
        }

        transform.Translate(Vector2.right * (speedX * Time.deltaTime));
        transform.Translate(Vector2.up * (speedY * Time.deltaTime));
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        deathAction?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(currentAttackDelay);
        canAttack = true;
    }

    private IEnumerator HitStun()
    {
        yield return new WaitForSeconds(hitStun);
        beenHit = false;
    }
}