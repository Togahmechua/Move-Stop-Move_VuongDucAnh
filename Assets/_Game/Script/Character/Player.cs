using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private CustomizeSkin customizeSkin;
    private GameObject targetBot; 
    public bool isDancing;
    public int currentNum;


    private void Start()
    {
        currentNum = PlayerPrefs.GetInt("PWeapon", 0);
        weaponModel = customizeSkin.SetModelWeapon(currentNum);
        wp = customizeSkin.SetWeaponForPlayer(currentNum);
    }

    private void Update()
    {
        if (isded == true) return;
        if (isDancing == true) return;
        // Kiểm tra xem nhân vật có di chuyển không
        bool isMoving = Mathf.Abs(joyStick.Vertical) > 0.001f || Mathf.Abs(joyStick.Horizontal) > 0.001f;

        if (isMoving)
        {
            this.Move();
            ChangeAnim(Constants.ANIM_RUNNING);
            isChecked = false;
        }
        else
        {
            if (attackRange.characterList.Count > 0)
            {
                TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
            }

            if (canShoot && attackRange.characterList.Count > 0 && !isChecked)
            {
                TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
                Shoot();
                isChecked = true;
            }
            else
            {
                if (!isAttacking) // Kiểm tra nếu không trong trạng thái tấn công
                {
                    ChangeAnim(Constants.ANIM_IDLE);
                }
                rb.velocity = Vector3.zero;
            }
        }

        ActiveTarget();
    }

    private void ActiveTarget()
    {
        if (attackRange.isInRange && attackRange.characterList.Count > 0)
        {
            targetBot = attackRange.characterList[0].target;
            if (targetBot != null)
            {
                targetBot.SetActive(true);
            }
        }
        else
        {
            if (targetBot != null)
            {
                targetBot.SetActive(false);
            }
        }
    }

    protected override void Move()
    {
        base.Move();
        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical) * moveSpeed;
        rb.velocity = movement;
        if (movement != Vector3.zero)
        {
            TF.rotation = Quaternion.LookRotation(movement);
        }
    }

    public override void Die()
    {
        base.Die();
        ChangeAnim(Constants.ANIM_Dead);
        isded = true;
    }

    public void ChangeWeapon(int num)
    {
        currentNum = num;
        PlayerPrefs.SetInt("PWeapon", currentNum);
        weaponModel = customizeSkin.SetModelWeapon(currentNum);
        wp = customizeSkin.SetWeaponForPlayer(currentNum);
    }
}
