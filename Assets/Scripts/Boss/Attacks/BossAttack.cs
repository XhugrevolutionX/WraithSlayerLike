using UnityEngine;
public abstract class BossAttack : MonoBehaviour
{
    public abstract void Fire(Transform boss, Transform target);
}