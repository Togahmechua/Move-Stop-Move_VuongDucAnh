using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;

    [SerializeField] private List<Transform> spawnList;
    [SerializeField] private int count = 0; 
    [SerializeField] private BotCtrl botPrefab;

    private bool isCheck;

    private void Awake()
    {
        LevelManager.ins = this;
        Spawn(0);
    }

    // private void Update()
    // {
    //     if (count % 5 != 0 && isCheck)
    //     {
    //         Spawn();
    //         isCheck = true;
    //     }
    // }

    public void Spawn(int num)
    {
        switch (num)
        {
            case (int)ESpawn.Spawn1 :
                for (int i = 0 ; i < 5; i++)
                {
                    BotCtrl bot1 = SimplePool.Spawn<BotCtrl>(botPrefab, spawnList[i].position, spawnList[i].rotation);
                    count++;
                }
            break;

            case (int)ESpawn.Spawn2 :
                BotCtrl bot = SimplePool.Spawn<BotCtrl>(botPrefab, spawnList[Random.Range(0,spawnList.Count)].position, spawnList[Random.Range(0,spawnList.Count)].rotation);
                count++;
            break;

        }
        
    }    
}

public enum ESpawn
{
    Spawn1 = 0,
    Spawn2 = 1
}
