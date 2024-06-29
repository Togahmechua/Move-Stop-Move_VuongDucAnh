using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<BotCtrl>
{
    private bool isCheck;

    public void OnEnter(BotCtrl bot)
    {
        bot.ChangeAnim(Constants.ANIM_RUNNING);
        bot.MoveToNewPos();
        isCheck = false;
    }

    public void OnExecute(BotCtrl bot)
    {
        if (Vector3.Distance(bot.TF.position, bot.GetDestinationPosition()) < 0.1f && !isCheck)
        {
            bot.ChangeAnim(Constants.ANIM_IDLE);
            if (bot.attackRange.characterList.Count != 0)
            {
                bot.TransitionToState(bot.attackState);
            }
            else
            {
                bot.Wait(() => bot.TransitionToState(bot.moveState));
                isCheck = true;
            }
        }
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
