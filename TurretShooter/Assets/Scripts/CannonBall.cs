using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");

    [SerializeField] private Animator animator;

    private Rigidbody ballRigidbody;

    public void Setup(Vector3 fireForce)
    {
        ballRigidbody.AddForce(fireForce, ForceMode.Impulse);
        ballRigidbody.angularVelocity = new Vector3(
            Random.Range(-10, 10),
            Random.Range(-10, 10),
            Random.Range(-10, 10));
    }
    public void OnFinishedExplosionAnimation()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.rotation =
            Quaternion.FromToRotation(transform.up, collision.GetContact(0).normal)
            * transform.rotation;

        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.isKinematic = true;

        animator.SetTrigger(Exploded);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }
}
