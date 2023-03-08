using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGraphics : MonoBehaviour
{
    private static readonly int LevelEndedHash = Animator.StringToHash("LevelEnded");
    private static readonly int GameOverHash = Animator.StringToHash("GameOver");

    public Animator animator;
    public TMP_Text remainingTargetsText;
    public TMP_Text levelFinishedText;

    public void EndLevel(int currentLevel)
    {
        levelFinishedText.text = $"Level {currentLevel} Finished!";
        animator.SetTrigger(LevelEndedHash);
    }

    public void EndGame()
    {
        animator.SetTrigger(GameOverHash);
    }

    public void OnFinishedEndLevelAnimation()
    {
        GameServices.GetService<LevelController>().NextLevel();
    }

    public void OnRetryClicked()
    {
        GameServices.GetService<LevelController>().RetryGame();
    }

    public void UpdateRemainingTargets(int remainingTargets)
    {
        remainingTargetsText.text = $"Remaining Targets: {remainingTargets}!";
    }
}
