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

    [SerializeField] private Animator anim;
    [SerializeField] protected float shootDelay;
    [SerializeField] private GameObject weapon; 
    [SerializeField] protected float distance;
    [SerializeField] private Transform model;

    protected bool isChecked;
    protected bool isAttacking; // Biến trạng thái cho animation tấn công

    private string animName;
    
    
    private void Awake()
    {
        ChangeAnim(Constants.ANIM_IDLE);
    }

    public virtual void Die()
    {
        // Kích hoạt sự kiện OnCharacterDeath
        if (OnCharacterDeath != null)
        {
            OnCharacterDeath(this);
        }
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
        model.localScale += new Vector3(0.25f,0.25f,0.25f);
    }

    public virtual void Shoot()
    {
        StartCoroutine(ShootBulletDelay());
    }
    
    public IEnumerator ShootBulletDelay()
    {
        canShoot = false;
        isAttacking = true;
        // Tạo vũ khí mới sau khi đã hoàn thành animation tấn công
        Weapon newWeapon = SimplePool.Spawn<Weapon>(PoolType.Weapon, TF.position, TF.rotation);
        newWeapon.Owner = this;
        ChangeAnim(Constants.ANIM_ATTACK);
        weapon.SetActive(false);
        yield return new WaitForSeconds(shootDelay);
        ChangeAnim(Constants.ANIM_IDLE);
        isAttacking = false; // Đặt lại trạng thái tấn công
        weapon.SetActive(true);
        isChecked = false; //Check điều kiện để bắn
    }
}
