using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public delegate void CharacterDeathHandler(Character character);
    public event CharacterDeathHandler OnCharacterDeath;
    public bool canShoot;
    public GameObject target;
    public AttackRange attackRange;
    public bool isded;
    public FullSetItem fullSetItem;

    [SerializeField] protected float shootDelay;
    [SerializeField] private Transform shootPos;
    [SerializeField] protected GameObject weaponModel;
    [SerializeField] protected Weapon wp;
    [SerializeField] protected float distance;
    public Transform model;

    protected bool isChecked;
    protected bool isAttacking;
    protected float cooldown = 0.4f;
    protected float timeToShoot;

    private string animName;
    public int countToScale;

    public virtual void Die()
    {
        if (isded) return; // Ensure this method is only called once
        isded = true;

        Debug.Log("Character died, invoking death handler and decreasing bot count.");
        OnCharacterDeath?.Invoke(this);
        LevelManager.Ins.DecreaseBotCount();
    }

    protected virtual void Move()
    {
        // For override
    }

    public virtual void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            fullSetItem.anim.ResetTrigger(this.animName);
            this.animName = animName;
            fullSetItem.anim.SetTrigger(this.animName);
        }
    }

    public void BuffScale()
    {
        if (isded) return;
        model.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        weaponModel.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
    }

    public virtual void Shoot()
    {
        StartCoroutine(ShootBulletDelay());
    }

    private IEnumerator ShootBulletDelay()
    {
        canShoot = false;
        isAttacking = true;
        ChangeAnim(Constants.ANIM_ATTACK);
        yield return new WaitForSeconds(0.2f);
        weaponModel.SetActive(false);
        wp.SetOwner(this);
        wp.OnFire(shootPos, this);
        yield return new WaitForSeconds(shootDelay);
        ChangeAnim(Constants.ANIM_IDLE);
        isAttacking = false; // Reset attacking state
        weaponModel.SetActive(true);
        isChecked = false; // Check conditions to shoot again
    }
}
