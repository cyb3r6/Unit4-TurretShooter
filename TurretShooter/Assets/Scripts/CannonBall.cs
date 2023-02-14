using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody ballRigidbody;

    public void Setup(Vector3 fireForce)
    {
        ballRigidbody.AddForce(fireForce, ForceMode.Impulse);
        ballRigidbody.angularVelocity = new Vector3(
            Random.Range(-10, 10),
            Random.Range(-10, 10),
            Random.Range(-10, 10));
    }

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }
}
