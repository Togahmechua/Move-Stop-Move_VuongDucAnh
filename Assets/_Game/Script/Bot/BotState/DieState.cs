using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState<BotCtrl>
{
    public void OnEnter(BotCtrl bot)
    {
        bot.ChangeAnim(Constants.ANIM_Dead);
        bot.Die();
    }

    public void OnExecute(BotCtrl bot)
    {
        
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
