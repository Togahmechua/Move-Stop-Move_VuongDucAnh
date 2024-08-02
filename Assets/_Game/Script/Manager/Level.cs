using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int maxBotCount;  // Maximum number of bots to be spawned
    public int remainingBotCount;  // Remaining number of bots to be killed to win
    public int currentBotCount = 0;  // Number of bots currently spawned
    public Transform playerPos;

    public List<Transform> spawnList;
    public BotCtrl botPrefab;
    public float spawnDelay = 4f;
    public List<BotCtrl> spawnedBots = new List<BotCtrl>();

    private bool canSpawnMoreBots;
    public bool isSetPos;

    public Player player;

    private void Start()
    {
        remainingBotCount = maxBotCount;
        player = FindObjectOfType<Player>();
        SetPlayerPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, playerPos.position) >= 0.1f && isSetPos == false)
        {
            SetPlayerPosition();
            isSetPos = true;
        }

        if (spawnedBots.Count >= maxBotCount)
        {
            canSpawnMoreBots = false;
        }
    }

    public void Initialize(List<Transform> spawns, BotCtrl bot)
    {
        spawnList = spawns;
        botPrefab = bot;
        currentBotCount = 0;
        remainingBotCount = maxBotCount;
        canSpawnMoreBots = true;
        SetPlayerPosition();
        SpawnInitialBots(5);
    }

    private void SetPlayerPosition()
    {
        if (player != null && playerPos != null)
        {
            player.transform.position = playerPos.position;
            player.transform.rotation = Quaternion.Euler(new Vector3(0f, 185f, 0f));
            player.fullSetItem.transform.localScale = Vector3.one;
            Debug.Log("Player position set to: " + playerPos.position);
        }
    }

    private void SpawnInitialBots(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (currentBotCount < maxBotCount)
            {
                BotCtrl bot = SimplePool.Spawn<BotCtrl>(botPrefab, spawnList[i % spawnList.Count].position, spawnList[i % spawnList.Count].rotation);
                bot.OnInit();
                spawnedBots.Add(bot);
                currentBotCount++;
            }
        }
    }

    public void SpawnNewBot()
    {
        if (canSpawnMoreBots && currentBotCount < maxBotCount)
        {
            int randomIndex = Random.Range(0, spawnList.Count);
            BotCtrl bot = SimplePool.Spawn<BotCtrl>(botPrefab, spawnList[randomIndex].position, spawnList[randomIndex].rotation);
            bot.OnInit();
            spawnedBots.Add(bot);
            currentBotCount++;
        }
    }

    public void DecreaseBotCount()
    {
        if (currentBotCount > 0)
        {
            remainingBotCount--;
            currentBotCount--;
            Debug.Log("Remaining bot count: " + remainingBotCount + ", Current bot count: " + currentBotCount);

            // Check if all bots have been killed
            if (remainingBotCount <= 0)
            {
                if (!player.isded)
                {
                    LevelManager.Ins.winCanvas.SetActive(true);
                    LevelManager.Ins.gameplayCanvas.SetActive(false);
                    LevelManager.Ins.currentLevel++;
                    LevelManager.Ins.coinMNG.IncreaseMoney(100);
                    PlayerPrefs.SetInt("CurrentLevel", LevelManager.Ins.currentLevel);
                }
            }

            // Start coroutine to spawn a new bot after a delay
            if (currentBotCount < maxBotCount)
            {
                StartCoroutine(SpawnBotAfterDelay());
            }

            // Update spawning status
            if (currentBotCount >= maxBotCount)
            {
                canSpawnMoreBots = false;
            }
        }
    }

    private IEnumerator SpawnBotAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        if (canSpawnMoreBots && currentBotCount < maxBotCount)
        {
            SpawnNewBot();
        }
    }

    public int GetCurrentBotCount()
    {
        return currentBotCount;
    }

    public int GetRemainingBotCount()
    {
        return remainingBotCount;
    }

    public int GetMaxBotCount()
    {
        return maxBotCount;
    }
}
