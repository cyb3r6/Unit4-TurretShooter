using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

namespace CannonApp
{
    public class LevelController : MonoBehaviour
    {
        private static readonly int LevelEndedHash = Animator.StringToHash("LevelEnded");
        private static readonly int GameOverHash = Animator.StringToHash("GameOver");

        [SerializeField] private CannonController cannonController;
        [SerializeField] private Animator animator;
        [SerializeField] private TMP_Text remainingTargetsText;
        [SerializeField] private TMP_Text levelFinishedText;

        private static int levelCount;

        private int remainingTargets;
        private int currentLevel;

        public void TargetDestroyed()
        {
            remainingTargets--;

            if (remainingTargets <= 0)
                EndLevel();

            UpdateRemainingTargets();
        }

        public void OnFinishedEndLevelAnimation()
        {
            GoToLevel(currentLevel + 1);
        }

        public void OnRetryClicked()
        {
            GoToLevel(1);
        }

        private void EndLevel()
        {
            cannonController.DisableFire();

            if (currentLevel == levelCount)
            {
                animator.SetTrigger(GameOverHash);
                return;
            }

            levelFinishedText.text = $"Level {currentLevel} Finished!";
            animator.SetTrigger(LevelEndedHash);
        }

        private void GoToLevel(int levelIndex)
        {
            SceneManager.LoadScene($"Level{levelIndex}");
        }

        private void Awake()
        {
            InitializeLevelCount();
            SetCurrentLevel();
            InitializeTargets();
        }

        private void SetCurrentLevel()
        {
            // if ()
            // {
            //     Debug.LogError("Level Controller on a non-level scene!");
            // }
        }

        private void InitializeTargets()
        {
        }

        private void InitializeLevelCount()
        {
            if (levelCount != 0)
                return;

            int sceneCount = SceneManager.sceneCountInBuildSettings;

            int maxLevelFound = 0;

            for( int i = 0; i < sceneCount; i++ )
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                // if ()
                // {
                //     maxLevelFound = Mathf.Max(maxLevelFound, levelIndex);
                //     levelCount++;
                // }
            }

            Debug.Assert(maxLevelFound == levelCount, "Max Scene Level differs from the total levels found");
        }

        private void UpdateRemainingTargets()
        {
            remainingTargetsText.text = $"Remaining Targets: {remainingTargets}!";
        }
    }
}