using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceX = 10f;
    [SerializeField] private float forceY = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private Animator animator;
    
    private Rigidbody2D _rigidbody;
    private Coroutine _dashAnimationCoroutine;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMoveX(InputValue value)
    {
        if (MathF.Abs(_rigidbody.linearVelocity.x) <= maxSpeed)
        {
            _rigidbody.AddForce(Vector2.left * forceX * value.Get<float>(), ForceMode2D.Impulse);
            _rigidbody.gravityScale = 0;
            
            animator.SetFloat("isMovingX", value.Get<float>());

            if (_dashAnimationCoroutine != null)
            {
                StopCoroutine(_dashAnimationCoroutine);
            }

            _dashAnimationCoroutine = StartCoroutine(DashAnimationCooldown());
        }
    }

    public void Impulse()
    {
        Vector2 direction = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y);
        
        _rigidbody.AddForce(Vector2.right * forceX * 2 * direction.normalized.x, ForceMode2D.Impulse);
        _rigidbody.AddForce(Vector2.up * forceY * 2 * direction.normalized.y, ForceMode2D.Impulse);
    }
    
    void OnMoveY(InputValue value)
    {
        if (MathF.Abs(_rigidbody.linearVelocity.y) <= maxSpeed)
        {
            _rigidbody.AddForce(Vector2.up * forceY * value.Get<float>(), ForceMode2D.Impulse);
            _rigidbody.gravityScale = 0;
            
            animator.SetFloat("isMovingY", value.Get<float>());

            if (_dashAnimationCoroutine != null)
            {
                StopCoroutine(_dashAnimationCoroutine);
            }

            _dashAnimationCoroutine = StartCoroutine(DashAnimationCooldown());
        }
    }

    IEnumerator DashAnimationCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetFloat("isMovingY", 0);
        animator.SetFloat("isMovingX", 0);
        _rigidbody.gravityScale = 1.5f;
    }
}
