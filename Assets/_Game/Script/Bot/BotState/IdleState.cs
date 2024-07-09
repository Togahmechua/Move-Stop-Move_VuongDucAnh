using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState<BotCtrl>
{
    public void OnEnter(BotCtrl bot)
    {
        bot.Wait(() => bot.TransitionToState(bot.moveState), Random.Range(0f,2f));
    }

    public void OnExecute(BotCtrl bot)
    {
        if (bot.isded == true)
        {
            bot.TransitionToState(bot.dieState);
        }
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
