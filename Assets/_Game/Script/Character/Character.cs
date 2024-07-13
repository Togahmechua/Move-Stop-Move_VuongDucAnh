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
    public Renderer pants;
    public Transform hatPos;

    [SerializeField] private Animator anim;
    [SerializeField] protected float shootDelay;
    [SerializeField] private Transform shootPos;
    [SerializeField] protected GameObject weaponModel;
    [SerializeField] protected Weapon wp;
    [SerializeField] protected float distance;
    [SerializeField] private Transform model;
    [SerializeField] protected Transform wpPos;
    [SerializeField] protected Transform shieldPos;
    [SerializeField] protected Transform tailPos;
    [SerializeField] protected Transform wingPos;
    [SerializeField] protected Renderer body;

    protected bool isChecked;
    protected bool isAttacking; // Biến trạng thái cho animation tấn công
    protected float cooldown = 0.4f;
    protected float timeToShoot; 

    private string animName;
    

    public virtual void Die()
    {
        OnCharacterDeath?.Invoke(this);
        // LevelManager.Ins.count--;
    }

    protected virtual void Move()
    {
        //For override
    }

    public virtual void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
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
        isAttacking = false; // Đặt lại trạng thái tấn công
        weaponModel.SetActive(true);
        isChecked = false; //Check điều kiện để bắn
    }
}
