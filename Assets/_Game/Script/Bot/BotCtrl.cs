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
    public bool isded;

    private void Start()
    {
        weapon = customizeSkin.RandomWeapon();
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
        ChangeAnim(Constants.ANIM_Dead);
        canShoot = false;
        isded = true;
        Invoke(nameof(DespawnBots),0.7f);
    }

    private void DespawnBots()
    {     
        SimplePool.Despawn(this);
    }


    public override void Shoot()
    {
        if (attackRange.isInRange && canShoot)
        {
            TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
            base.Shoot();
        }
    }

    public void Wait(UnityAction callBack)
    {
        StartCoroutine(IEWait(callBack));
    }

    private IEnumerator IEWait(UnityAction callBack)
    {
        yield return new WaitForSeconds(Random.Range(0f, 2.5f));
        callBack?.Invoke();
    }

    public Vector3 RandomPoint()
    {
        Vector3 randomPosition = Random.insideUnitSphere * rangeToMove;
        Vector3 randomPos = new Vector3(randomPosition.x, 0, randomPosition.z);
        return TF.position + randomPos; // Adjusted to be relative to bot's current position
    }

    public void MoveToNewPos()
    {
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