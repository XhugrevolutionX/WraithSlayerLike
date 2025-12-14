using UnityEngine;

public class ExplosiveAttack : BossAttack
{
    [SerializeField] private GameObject shrapnel;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force = 100;

    public override void Fire(Transform boss, Transform target)
    {
        transform.position = new Vector2(boss.position.x, boss.position.y - 2);
        
        float dir = (target.position.x > transform.position.x) ? 1 : -1;
        
        rb.AddForce(transform.right * (dir * force));
        rb.AddForce(transform.up * (force / 2));
    }
    
    private void Explode()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(shrapnel, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Death") || other.CompareTag("Ground"))
        {
            Explode();
        }
    }
}
