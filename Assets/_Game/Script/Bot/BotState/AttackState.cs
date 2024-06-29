using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<BotCtrl>
{
    private bool isCheck;
    public void OnEnter(BotCtrl bot)
    {
        isCheck = false;
        if (bot.attackRange.characterList.Count != 0)
        {
            // Debug.Log("Shoot");
            bot.Wait(() => bot.Shoot());
        }
    }

    public void OnExecute(BotCtrl bot)
    {
        if (bot.attackRange.characterList.Count <= 0 && isCheck == false)
        {
            // Debug.Log("E");
            bot.Wait(() => bot.TransitionToState(bot.moveState));
            isCheck = true;
        }
        else if (bot.attackRange.characterList.Count != 0 && isCheck == false && bot.canShoot)
        {
            // Debug.Log("F");
            bot.Wait(() => bot.TransitionToState(bot.attackState));
            isCheck = true;
        }
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
