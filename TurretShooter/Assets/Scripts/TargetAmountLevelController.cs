using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAmountLevelController : LevelController
{
    [SerializeField] private int targetDestructionCount = 30;

    public override void RegisterTarget() { }

    protected override void Awake()
    {
        base.Awake();

        remainingTargets = targetDestructionCount;
        uiGraphics.UpdateRemainingTargets(remainingTargets);
    }
}
