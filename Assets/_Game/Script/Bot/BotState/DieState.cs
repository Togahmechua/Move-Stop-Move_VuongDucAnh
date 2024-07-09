using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState<BotCtrl>
{
    private bool isded = false;
    public void OnEnter(BotCtrl bot)
    {
        if (isded == true) return;
        // Debug.Log("Trans to die state");
        bot.Die();
        isded = true;
    }

    public void OnExecute(BotCtrl bot)
    {
        
    }

    public void OnExit(BotCtrl bot)
    {
        
    }
}
