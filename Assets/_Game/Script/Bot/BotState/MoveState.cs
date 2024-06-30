using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<BotCtrl>
{
    private bool isCheck;
    private float time;
    private const float maxWaitTime = 5f; // thời gian chờ tối đa trước khi thử lại

    public void OnEnter(BotCtrl bot)
    {
        bot.ChangeAnim(Constants.ANIM_RUNNING);
        bot.MoveToNewPos();
        isCheck = false;
        time = 0f;
    }

    public void OnExecute(BotCtrl bot)
    {
        time += Time.deltaTime;

        if (Vector3.Distance(bot.TF.position, bot.GetDestinationPosition()) < 0.3f && !isCheck)
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
        else if (time >= maxWaitTime)
        {
            // Nếu bot không đến được vị trí đích trong thời gian giới hạn, đặt lại vị trí đích
            bot.MoveToNewPos();
            time = 0f; // Reset thời gian
        }
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
