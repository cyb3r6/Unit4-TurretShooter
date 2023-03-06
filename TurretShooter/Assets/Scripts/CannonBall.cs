using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");

    [SerializeField] private float explosionForce = 12.0f;
    [SerializeField] private float explosionRadius = 9.0f;
    [SerializeField] private float explosionUpwardsModifier = 1.0f;
    [SerializeField] protected Animator animator;

    public virtual CannonBallType BallType => CannonBallType.Normal;

    protected Rigidbody ballRigidbody;
    protected CannonBallsPool pool;

    public virtual void Setup(Vector3 fireForce, CannonBallsPool objectPool)
    {
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.isKinematic = false;
        pool = objectPool;

        ballRigidbody.AddForce(fireForce, ForceMode.Impulse);
        ballRigidbody.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    }

    public void OnFinishedExplosionAnimation()
    {
        pool.ReleaseCannonBall(this, BallType);
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
        pool.ReleaseCannonBall(this, BallType);
    }

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }
}
