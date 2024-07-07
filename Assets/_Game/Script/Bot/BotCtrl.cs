using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class BotCtrl : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangeToMove;

    public IState<BotCtrl> currentState;
    public IdleState idleState;
    public AttackState attackState;
    public MoveState moveState;
    [SerializeField] private CustomizeSkin customizeSkin;

    [SerializeField] private Vector3 _destinationPosition;

    public Vector3 GetDestinationPosition() => _destinationPosition;

    private void Start()
    {
        weaponModel = customizeSkin.RandomModelWeapon();
        wp = customizeSkin.RandomWeapon();
        customizeSkin.RandomSkinForBots();
        // Initialize states
        idleState = new IdleState();
        attackState = new AttackState();
        moveState = new MoveState();

        //Start in Idle state
        TransitionToState(idleState);
    }

    private void Update()
    {
        if (isded == true) return;
        currentState?.OnExecute(this);
    }

    public void TransitionToState(IState<BotCtrl> newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }

    public void Move(int num)
    {
        if (isded) return;
        switch (num)
        {
            case (int)Chase.Chase0:
                Vector3 targetPos = RandomPoint();
                agent.SetDestination(targetPos);
                break;
                // case (int)Chase.Chase1:
                //     break;
                // case (int)Chase.Chase2:
                //     break;
        }
    }

    public override void Die()
    {
        base.Die();
        isded = true;
        
        agent.speed = 0;
        ChangeAnim(Constants.ANIM_Dead);
        canShoot = false;
       
        Invoke(nameof(DespawnBots),1f);
    }

    private void DespawnBots()
    {     
        SimplePool.Despawn(this);
    }


    public override void Shoot()
    {
        if (isded) return;

        if (attackRange.isInRange && canShoot)
        {
            TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
            base.Shoot();
        }
    }

    public void Wait(UnityAction callBack, float time)
    {
        if (isded) return;

        StartCoroutine(IEWait(callBack, time));
    }

    private IEnumerator IEWait(UnityAction callBack, float time)
    {
        yield return new WaitForSeconds(time);
        callBack?.Invoke();
    }

    public Vector3 RandomPoint()
    {
        if (isded) return Vector3.zero;

        Vector3 randomPosition = Random.insideUnitSphere * rangeToMove;
        Vector3 randomPos = new Vector3(randomPosition.x, 0, randomPosition.z);
        return TF.position + randomPos; // Adjusted to be relative to bot's current position
    }

    public void MoveToNewPos()
    {
        if (isded) return;

        _destinationPosition = RandomPoint();
        agent.SetDestination(_destinationPosition);
    }
}

public enum Chase
{
    Chase0 = 0,
    Chase1 = 1,
    Chase2 = 2
}