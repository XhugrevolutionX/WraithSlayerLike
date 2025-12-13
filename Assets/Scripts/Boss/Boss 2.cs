using UnityEngine;

public class Boss2 : Boss
{
    protected override void Start()
    {
        StartPosition = transform.position;
        Animator = GetComponent<Animator>();
        
        Target = GameObject.FindGameObjectWithTag("Player");
        Game = GetComponentInParent<Game>();
        
        CanAttack = false;
        AttackCoroutine = StartCoroutine(AttackDelay());
    }

    protected override void Move()
    {
        if (transform.position.x >= StartPosition.x + moveRangeX || transform.position.x <= StartPosition.x - moveRangeX)
        {
            speedX = -speedX;
        }
        
        if (transform.position.y >= StartPosition.y + moveRangeY || transform.position.y <= StartPosition.y - moveRangeY)
        {
            speedY = -speedY;
        }
        
        transform.Translate(Vector2.right * (speedX * Time.deltaTime));
        transform.Translate(Vector2.up * (speedY * Time.deltaTime));
    }

    protected override int ChooseAttack()
    {
        switch (State)
        {
            case 0:
                return Random.Range(0, 2);
            case 1:
                return Random.Range(2, 5);
            case 2:
                return Random.Range(0, 5);
            default:
                return 0;
        }
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);

        if (health > 0)
        {
            switch (State)
            {
                case 0:
                    attackDelay = 3;
                    break;
                case 1:
                    attackDelay = 2.5f;
                    break;
                case 2:
                    attackDelay = 1.5f;
                    break;
            }

            if (StunCoroutine != null)
            {
                StopCoroutine(StunCoroutine);
            }

            StunCoroutine = StartCoroutine(HitStun());
        }
    }
}
