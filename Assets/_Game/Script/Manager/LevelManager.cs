using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;

    public TextMeshProUGUI text;
    public Player player;
    public Button starButton;
    public bool isStart;
    public int currentLevel;

    [SerializeField] private List<Level> levelList = new List<Level>();
    public Level currentLevelInstance;

    private void Awake()
    {
        LevelManager.ins = this;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    private void Start()
    {
        StartLevel(currentLevel);
    }

    public void StartGame()
    {
        isStart = true;
    }

    public void StartLevel(int levelIndex)
    {
        // Clear and despawn previous level's bots if any
        if (currentLevelInstance != null)
        {
            foreach (var bot in currentLevelInstance.spawnedBots)
            {
                SimplePool.Despawn(bot);  // Despawn bots using the pool system
            }
            currentLevelInstance.spawnedBots.Clear();  // Clear the list
            Destroy(currentLevelInstance.gameObject); // Destroy the level object
        }

        // Instantiate new level
        currentLevelInstance = Instantiate(levelList[levelIndex], transform);
        currentLevelInstance.Initialize(currentLevelInstance.spawnList, currentLevelInstance.botPrefab);
        UpdateText();
    }

    public void UpdateText()
    {
        if (currentLevelInstance != null)
        {
            text.text = "Alive: " + currentLevelInstance.GetRemainingBotCount();
        }
    }

    public void DecreaseBotCount()
    {
        if (currentLevelInstance != null)
        {
            currentLevelInstance.DecreaseBotCount();
            UpdateText();
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        StartLevel(currentLevel);
        isStart = false;
    }
}
