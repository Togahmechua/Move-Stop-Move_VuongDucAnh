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
    private GameObject targetBot;
    public bool isDancing;
    public int currentNum;

    private void Start()
    {
        currentNum = PlayerPrefs.GetInt("PWeapon", 0);
        weaponModel = GameData.Ins.SetModelWeapon(currentNum, wpPos);
        wp = GameData.Ins.SetWeaponForPlayer(currentNum);
    }

    private void Update()
    {
        if (isded == true)
        {
            Die();
            this.gameObject.SetActive(false);
        }

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

            timeToShoot += Time.deltaTime; // Tăng thời gian chờ

            if (canShoot && attackRange.characterList.Count > 0 && !isChecked && timeToShoot >= cooldown)
            {
                TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
                Debug.Log("Shoot");
                Shoot();
                rb.velocity = Vector3.zero;
                isChecked = true;
                timeToShoot = 0f; // Đặt lại thời gian chờ
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
    }

    public void ChangeWeapon(int num)
    {
        currentNum = num;
        PlayerPrefs.SetInt("PWeapon", currentNum);
        weaponModel = GameData.Ins.SetModelWeapon(currentNum, wpPos);
        wp = GameData.Ins.SetWeaponForPlayer(currentNum);
    }
}
