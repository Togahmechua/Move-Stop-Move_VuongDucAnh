using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public delegate void CharacterDeathHandler(Character character);
    public event CharacterDeathHandler OnCharacterDeath;
    [SerializeField] private Animator anim;
    [SerializeField] protected float shootDelay;
    public bool canShoot = true;
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

    protected virtual void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    protected virtual void Shoot()
    {
        StartCoroutine(ShootBulletDelay());
    }
    
    public IEnumerator ShootBulletDelay()
    {
        canShoot = false;
        // Weapon newWeapon = Instantiate(weaponPrefab, shootPos.position, shootPos.rotation);
        Weapon newWeapon = SimplePool.Spawn<Weapon>(PoolType.Weapon, TF.position, TF.rotation);
        newWeapon.Owner = this;
        ChangeAnim(Constants.ANIM_ATTACK);
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
        ChangeAnim(Constants.ANIM_IDLE);
    }
}
