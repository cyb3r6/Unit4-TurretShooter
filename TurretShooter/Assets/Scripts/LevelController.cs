using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class LevelController : MonoBehaviour
{
    [SerializeField] protected UIGraphics uiGraphics;

    private static int levelCount;

    public Action levelEnded;
    protected int remainingTargets;
    private int currentLevel;

    public void RetryGame()
    {
        GoToLevel(1);
    }

    public void NextLevel()
    {
        GoToLevel(currentLevel + 1);
    }

    public void TargetDestroyed()
    {
        remainingTargets--;

        if (remainingTargets <= 0)
            EndLevel();

        uiGraphics.UpdateRemainingTargets(remainingTargets);
    }

    public virtual void RegisterTarget()
    {
        remainingTargets++;
        uiGraphics.UpdateRemainingTargets(remainingTargets);
    }

    private void EndLevel()
    {
        levelEnded?.Invoke();

        if (currentLevel == levelCount)
        {
            uiGraphics.EndGame();
            return;
        }

        uiGraphics.EndLevel(currentLevel);
    }

    private void GoToLevel(int levelIndex)
    {
        SceneManager.LoadScene($"Level{levelIndex}");
    }

    private bool GetLevelIndex(string sceneName, out int levelIndex)
    {
        Match find = Regex.Match(sceneName, "\\d+");

        if (find != Match.Empty)
        {
            levelIndex = Int32.Parse(find.Value);
            return true;
        }

        levelIndex = -1;
        return false;
    }

    protected virtual void Awake()
    {
        GameServices.RegisterService(this);

        InitializeLevelCount();
        SetCurrentLevel();
    }

    private void OnDestroy()
    {
        GameServices.DeregisterService(this);
    }

    private void SetCurrentLevel()
    {
        if (!GetLevelIndex(SceneManager.GetActiveScene().name, out currentLevel))
        {
            Debug.LogError("Level Controller on a non-level scene!");
        }
    }

    private void InitializeLevelCount()
    {
        if (levelCount != 0)
            return;

        int sceneCount = SceneManager.sceneCountInBuildSettings;

        int maxLevelFound = 0;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            if (GetLevelIndex(sceneName, out var levelIndex))
            {
                maxLevelFound = Mathf.Max(maxLevelFound, levelIndex);
                levelCount++;
            }
        }

        Debug.Assert(maxLevelFound == levelCount, "Max Scene Level differs from the total levels found");
    }
}