using UnityEngine;

public class Residue : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float forceX = 200;
    [SerializeField] private float forceY = 400;
 
    void Start()
    {
        rb.AddForce(Vector2.up * Random.Range(forceY/2, forceY));
        rb.AddForce(Vector2.right * Random.Range(forceX/2, forceX) * Random.Range(-1f, 1f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Death") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }   
    }
}
