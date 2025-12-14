using UnityEngine;

public class Boulder : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 75 * Time.deltaTime));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Death") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
