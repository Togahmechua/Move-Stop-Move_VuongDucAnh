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
            bot.Wait(() => bot.Shoot(), Random.Range(0f,0.2f));
        }
    }

    public void OnExecute(BotCtrl bot)
    {
        if (bot.isded == true)
        {
            bot.TransitionToState(bot.dieState);
        }

        if (bot.attackRange.characterList.Count <= 0 && isCheck == false)
        {
            // Debug.Log("E");
            bot.Wait(() => bot.TransitionToState(bot.moveState), Random.Range(0f,2f));
            isCheck = true;
        }
        else if (bot.attackRange.characterList.Count != 0 && bot.canShoot)
        {
            bot.TF.rotation = Quaternion.LookRotation(bot.attackRange.characterList[0].TF.position - bot.TF.position);
            // Debug.Log("F");
            // bot.Wait(() => bot.TransitionToState(bot.attackState), Random.Range(0f,0.2f));
            bot.TransitionToState(bot.attackState);
        }
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
