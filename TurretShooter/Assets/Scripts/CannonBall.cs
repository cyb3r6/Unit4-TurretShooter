using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour, IPoolObject
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");

    [SerializeField] private float explosionForce = 12.0f;
    [SerializeField] private float explosionRadius = 9.0f;
    [SerializeField] private float explosionUpwardsModifier = 1.0f;
    [SerializeField] protected Animator animator;

    public virtual PoolObjectId PoolId => PoolObjectId.DefaultCannonBall;

    protected Rigidbody ballRigidbody;
    protected ObjectsPool pool;

    public virtual void Setup(Vector3 fireForce, ObjectsPool objectPool)
    {
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.isKinematic = false;
        pool = objectPool;

        ballRigidbody.AddForce(fireForce, ForceMode.Impulse);
        ballRigidbody.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void OnFinishedExplosionAnimation()
    {
        pool.ReleaseObject(this);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        transform.rotation =
            Quaternion.FromToRotation(transform.up, collision.GetContact(0).normal)
            * transform.rotation;

        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.isKinematic = true;

        animator.SetTrigger(Exploded);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius, LayerMask.GetMask("Targets"));

        foreach (Collider hit in colliders)
        {
            Rigidbody collidedRigidbody = hit.GetComponent<Rigidbody>();

            if (collidedRigidbody != null)
            {
                collidedRigidbody.AddExplosionForce(
                    explosionForce,
                    explosionPos,
                    explosionRadius,
                    explosionUpwardsModifier,
                    ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pool.ReleaseObject(this);
    }

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }
}
