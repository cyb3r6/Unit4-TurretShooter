using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCannonBall : CannonBall
{
    private static readonly int SpecialAvailableHash = Animator.StringToHash("SpecialAvailable");
    private static readonly int SpecialUsedHash = Animator.StringToHash("SpecialUsed");

    public float splitTime = 0.7f;
    public float splitAngle = 20.0f;

    public override CannonBallType BallType => CannonBallType.Split;

    private float remainingSplitTime;

    public override void Setup(Vector3 fireForce, CannonBallsPool objectPool)
    {
        base.Setup(fireForce, objectPool);

        animator.SetTrigger(SpecialAvailableHash);
        remainingSplitTime = splitTime;
        enabled = true;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        enabled = false;
    }

    private void SpawnSplitCannonBalls()
    {
        var position = transform.position;
        var forward = ballRigidbody.velocity;

        var ball1Forward =
            Quaternion.AngleAxis(-splitAngle, Vector3.up) * forward;
        var ball1 = pool.GetCannonBall(CannonBallType.Normal);
        ball1.transform.position = position;
        ball1.Setup(ball1Forward, pool);

        var ball2Forward =
            Quaternion.AngleAxis(splitAngle, Vector3.up) * forward;
        var ball2 = pool.GetCannonBall(CannonBallType.Normal);
        ball2.transform.position = position;
        ball2.Setup(ball2Forward, pool);

        animator.SetTrigger(SpecialUsedHash);
        enabled = false;
    }

    private void Update()
    {
        remainingSplitTime -= Time.deltaTime;

        if (remainingSplitTime <= 0)
            SpawnSplitCannonBalls();
    }
}
