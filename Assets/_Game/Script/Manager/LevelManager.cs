using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;

    public int count = 0;
    public Player player;
    public Button starButton;
    
    [SerializeField] private List<Transform> spawnList;
    [SerializeField] private BotCtrl botPrefab;
    

    private void Awake()
    {
        LevelManager.ins = this;
        Spawn(0);
    }


    private void Update()
    {
        if (count < 5)
        {
            Spawn(1);
        }

        if (count <= 0)
        {
            count = 0;
        }
    }

    public void Spawn(int num)
    {
        switch (num)
        {
            case (int)ESpawn.Spawn0 :
                for (int i = 0 ; i < 5; i++)
                {
                    BotCtrl bot1 = SimplePool.Spawn<BotCtrl>(botPrefab, spawnList[i].position, spawnList[i].rotation);
                    count++;
                }
            break;

            case (int)ESpawn.Spawn1 :
                BotCtrl bot = SimplePool.Spawn<BotCtrl>(botPrefab, spawnList[Random.Range(0,spawnList.Count)].position, spawnList[Random.Range(0,spawnList.Count)].rotation);
                count++;
            break;

        }
    }
}

public enum ESpawn
{
    Spawn0 = 0,
    Spawn1 = 1
}
