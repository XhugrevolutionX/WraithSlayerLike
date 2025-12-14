using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttack : BossAttack
{
    private GameObject _target;
    [SerializeField] private float speed = 5;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float spawnOffset = 6f; // Distance from player
    
    private float _dir;
    
    public override void Fire(Transform boss, Transform target)
    {
        _dir = (target.position.x > boss.position.x) ? -1 : 1;
        
        transform.position = new Vector2(target.position.x + (spawnOffset * _dir), target.position.y);
        
        StartCoroutine(DestroyRoutine());
    }

    void Update()
    {
        transform.Translate(Vector2.right * (-_dir * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death")) Destroy(gameObject);
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
