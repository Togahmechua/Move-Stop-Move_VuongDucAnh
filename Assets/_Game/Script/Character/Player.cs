using System.Collections;
using System.Collections.Generic;
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
        // Load weapon data and set the current weapon model
        currentNum = PlayerPrefs.GetInt("PWeapon", 0);
        EquipWeapon(currentNum);

        // Load skin data and set the current skin
        int skinId = PlayerPrefs.GetInt("PlayerSkin", 0);
        EquipPreviouslyEquippedItem(skinId);
    }

    public void OnInit()
    {
        Debug.Log("OnInit called");
        if (LevelManager.Ins.currentLevelInstance.playerPos != null)
        {
            transform.position = LevelManager.Ins.currentLevelInstance.playerPos.position;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 185f, 0f));
            Debug.Log("Position set to initial position: " + LevelManager.Ins.currentLevelInstance.playerPos.position);
        }
        else
        {
            Debug.LogWarning("Initial position is not set");
        }
        fullSetItem.gameObject.transform.localScale = Vector3.one;
    }

    private void EquipWeapon(int weaponId)
    {
        weaponModel = GameData.Ins.SetModelWeapon(weaponId, fullSetItem.TfWeaponHolder);
        wp = GameData.Ins.SetWeaponForPlayer(weaponId);
    }

    public void PlayerSetSkin(int id)
    {
        GameData.Ins.SetSkin(this, id);
        EquipWeapon(currentNum);
    }

    public void PlayerSetHat(int id)
    {
        GameData.Ins.SetHatForPlayer(id, fullSetItem.hatPos);
        EquipWeapon(currentNum);
    }

    public void PlayerSetPant(int id)
    {
        GameData.Ins.SetPantForPlayer(id, fullSetItem.PantRenderer);
        EquipWeapon(currentNum);
    }

    public void PlayerSetShield(int id)
    {
        GameData.Ins.SetShieldForPlayer(id, fullSetItem.shieldPos);
        EquipWeapon(currentNum);
    }

    public void EquipPreviouslyEquippedItem(int id)
    {
        if (id > 0 && id < 10)
        {
            PlayerSetHat(id);
        }
        else if (id >= 10 && id < 20)
        {
            PlayerSetPant(id - 10);
        }
        else if (id >= 20 && id < 30)
        {
            PlayerSetShield(id - 20);
        }
        else if (id >= 30 && id < 40)
        {
            PlayerSetSkin(id - 30);
        }
    }

    private void Update()
    {
        if (isded)
        {
            Die();
            gameObject.SetActive(false);
            return;
        }

        if (isDancing) return;

        bool isMoving = Mathf.Abs(joyStick.Vertical) > 0.001f || Mathf.Abs(joyStick.Horizontal) > 0.001f;

        if (isMoving)
        {
            Move();
            ChangeAnim(Constants.ANIM_RUNNING);
            isChecked = false;
        }
        else
        {
            HandleShooting();
        }

        ActiveTarget();
    }

    private void HandleShooting()
    {
        if (attackRange.characterList.Count > 0)
        {
            TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
        }

        timeToShoot += Time.deltaTime;

        if (canShoot && attackRange.characterList.Count > 0 && !isChecked && timeToShoot >= cooldown)
        {
            TF.rotation = Quaternion.LookRotation(attackRange.characterList[0].TF.position - TF.position);
            Shoot();
            rb.velocity = Vector3.zero;
            isChecked = true;
            timeToShoot = 0f;
        }
        else if (!isAttacking)
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }

        rb.velocity = Vector3.zero;
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
        EquipWeapon(currentNum);
    }
}
