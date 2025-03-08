using UnityEngine;

public class BossExplodingAttack : MonoBehaviour
{
       
    private GameObject _target;
    [SerializeField] private GameObject shrapnel;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force = 100;
    private float _dir;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        
        if (_target.transform.position.x > transform.position.x)
        {
            _dir = 1;
        }
        else
        {
            _dir = -1;
        }
        
        rb.AddForce(transform.right * (_dir * force));
        rb.AddForce(transform.up * (force / 2));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Death") || other.CompareTag("Ground"))
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(shrapnel, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
