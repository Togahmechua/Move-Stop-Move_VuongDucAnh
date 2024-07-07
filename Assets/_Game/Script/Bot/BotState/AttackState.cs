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
            bot.Wait(() => bot.Shoot(), Random.Range(0f,1f));
        }
    }

    public void OnExecute(BotCtrl bot)
    {
        if (bot.isded == true)
        {
            bot.Die();
        }
        if (bot.attackRange.characterList.Count <= 0 && isCheck == false)
        {
            // Debug.Log("E");
            bot.Wait(() => bot.TransitionToState(bot.moveState), Random.Range(0f,2f));
            isCheck = true;
        }
        else if (bot.attackRange.characterList.Count != 0 && isCheck == false && bot.canShoot)
        {
            // Debug.Log("F");
            bot.Wait(() => bot.TransitionToState(bot.attackState), Random.Range(0f,1f));
            isCheck = true;
        }
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
