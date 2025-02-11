using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallsAttack : MonoBehaviour
{
    private GameObject _target;
    [SerializeField] private float speed = 5;
    [SerializeField] private float lifeTime = 3;
    private float _dir;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        if (_target.gameObject.transform.position.x > transform.position.x)
        {
            _dir = 1;
        }
        else
        {
            _dir = -1;
        }
        
        StartCoroutine("Destroy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (_dir * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
