using System;
using UnityEngine;

public class HommingAttack : BossAttack
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force = 5;
    [SerializeField] private float rotateSpeed = 100;
    
    private Transform _target;
    
    public override void Fire(Transform boss, Transform target)
    {
        _target = target;
        transform.position = new Vector2(boss.position.x, boss.position.y - 2);
    }

    void Update()
    {
        if (_target == null) return;
        
        Vector2 direction = (_target.position - transform.position).normalized;
        rb.AddForce(direction * force);
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Death") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
