using System;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    
    private GameObject _target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce((_target.transform.position - transform.position).normalized * force);
        transform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Death") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
