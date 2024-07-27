using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    }


    private static Dictionary<Collider, Weapon> weapons = new Dictionary<Collider, Weapon>();

    public static Weapon GetWeapon(Collider collider)
    {
        if (!weapons.ContainsKey(collider))
        {
            weapons.Add(collider, collider.GetComponent<Weapon>());
        }

        return weapons[collider];
    }
    

    private static Dictionary<Collider, Obstacle> obstacle = new Dictionary<Collider, Obstacle>();

    public static Obstacle GetObstacle(Collider collider)
    {
        if (!obstacle.ContainsKey(collider))
        {
            obstacle.Add(collider, collider.GetComponent<Obstacle>());
        }

        return obstacle[collider];
    }

    private static Dictionary<Collider, BotCtrl> botctrl = new Dictionary<Collider, BotCtrl>();

    public static BotCtrl GetBotCtrl(Collider collider)
    {
        if (!botctrl.ContainsKey(collider))
        {
            botctrl.Add(collider, collider.GetComponent<BotCtrl>());
        }

        return botctrl[collider];
    }
}
