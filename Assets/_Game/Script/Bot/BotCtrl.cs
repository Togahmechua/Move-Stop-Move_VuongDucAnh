using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class BotCtrl : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangeToMove;
    [SerializeField] private Vector3 _destinationPosition;


    public IState<BotCtrl> currentState;
    public StartState startState;
    public IdleState idleState;
    public AttackState attackState;
    public MoveState moveState;
    public DieState dieState;
    public Vector3 GetDestinationPosition() => _destinationPosition;

    private void Start()
    {
        weaponModel = GameData.Ins.RandomModelWeapon(wpPos);
        wp = GameData.Ins.RandomWeapon();
        GameData.Ins.RandomSkinForBots(body, pants, hatPos);
        // Initialize states

        startState = new StartState();
        idleState = new IdleState();
        attackState = new AttackState();
        moveState = new MoveState();
        dieState = new DieState();
        //Start in Idle state
        TransitionToState(startState);
        LevelManager.Ins.starButton.onClick.AddListener(TransToIdleState);
    }

    private void TransToIdleState()
    {
        TransitionToState(idleState);
    }

    private void Update()
    {
        currentState?.OnExecute(this);

        if (isded == true)
        {
            TransitionToState(dieState);
        }
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
        agent.speed = 0;
        ChangeAnim(Constants.ANIM_Dead);
        canShoot = false;
       
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
            timeToShoot += Time.deltaTime;
            if (timeToShoot >= cooldown)
            {
                TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
                base.Shoot();
                timeToShoot = 0f;
            }
        }
    }

    public void Wait(UnityAction callBack, float time)
    {
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