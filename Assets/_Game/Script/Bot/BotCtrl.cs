using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Vector3 _destinationPosition;

    public Target targetScr;
    public IState<BotCtrl> currentState;
    public StartState startState;
    public IdleState idleState;
    public AttackState attackState;
    public MoveState moveState;
    public DieState dieState;
    public Vector3 GetDestinationPosition() => _destinationPosition;

    private void Start()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            GameData.Ins.RandomSkinForBots(fullSetItem.body, fullSetItem.PantRenderer, fullSetItem.hatPos);
        }
        else
        {
            GameData.Ins.SetSkin(this, Random.Range(1, System.Enum.GetValues(typeof(ESetSkin)).Length));
        }
        int num = Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length);
        weaponModel = GameData.Ins.RandomModelWeapon(num, fullSetItem.TfWeaponHolder);
        wp = GameData.Ins.RandomWeapon(num);

        // Initialize states
        startState = new StartState();
        idleState = new IdleState();
        attackState = new AttackState();
        moveState = new MoveState();
        dieState = new DieState();

        // Start in Start state
        if (!LevelManager.Ins.isStart)
        {
            TransitionToState(startState);
            LevelManager.Ins.starButton.onClick.AddListener(TransToIdleState);
        }
        else
        {
            TransitionToState(idleState);
        }
    }

    public void OnInit()
    {
        isded = false;
        canShoot = true;
        currentState = null;
        agent.enabled = true;
        agent.speed = moveSpeed;
        TransitionToState(idleState);
    }

    private void TransToIdleState()
    {
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
        agent.speed = 0;
        canShoot = false;
        Invoke(nameof(DespawnBots), 1f);
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
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(IEWait(callBack, time));
        }
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
        return TF.position + randomPos;
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
