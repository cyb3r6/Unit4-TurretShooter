using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class CannonBallsPool
{
    [SerializeField] private List<CannonBall> possibleCannonBallPrefabs;

    private Dictionary<CannonBallType, List<CannonBall>> pools;

    public void Setup(int prewarmCount)
    {
        pools = new Dictionary<CannonBallType, List<CannonBall>>(possibleCannonBallPrefabs.Count);

        foreach (var ballPrefab in possibleCannonBallPrefabs)
        {
            var pool = new List<CannonBall>(prewarmCount);

            for (int j = 0; j < prewarmCount; j++)
                pool.Add(Instantiate(ballPrefab));

            pools[ballPrefab.BallType] = pool;
        }
    }

    public CannonBall GetCannonBall(CannonBallType ballType)
    {
        var pool = pools[ballType];
        CannonBall ball;

        if (pool.Count == 0)
            ball = Instantiate(possibleCannonBallPrefabs.Find(prefab => prefab.BallType == ballType));
        else
        {
            ball = pool[0];
            pool.RemoveAt(0);
        }

        ball.gameObject.SetActive(true);
        return ball;
    }

    public void ReleaseCannonBall(CannonBall ball, CannonBallType ballType)
    {
        pools[ballType].Add(ball);
        ball.gameObject.SetActive(false);
    }

    private CannonBall Instantiate(CannonBall prefab)
    {
        var newBall = Object.Instantiate(prefab);
        newBall.gameObject.SetActive(false);

        return newBall;
    }
}
