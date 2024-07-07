using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPlayer : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected float speed;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 playPos;
    [SerializeField] private Vector3 shopPos;
    private bool isplay;
    private bool isMainMenu;
    private bool isActiveShop;

    private Vector3 offset;
    

    protected virtual void Awake()
    {
        this.LoadPlayer();
    }

    void Start()
    {
        // Tính offset giữa vị trí của camera và vị trí của nhân vật
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (this.player != null)
        {
            // Tính toán vị trí mới của camera dựa trên vị trí của nhân vật và offset
            Vector3 targetPosition = player.transform.position + offset;

            if (isplay)
            {
                // Di chuyển camera đến vị trí playPos
                transform.position = Vector3.MoveTowards(transform.position, playPos, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(50f, 0f, 0f));
                Wait(() => isplay = false);
            }
            else if (isMainMenu)
            {
                // Di chuyển camera đến vị trí startPos
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(10f, 0f, 0f));
            }
            else if (isActiveShop)
            {
                player.isDancing = true;
                Debug.Log("A");
                player.ChangeAnim(Constants.ANIM_Dance);
                // Di chuyển camera đến vị trí startPos
                transform.position = Vector3.MoveTowards(transform.position, shopPos, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(10f, 0f, 0f));
                Wait(() => isActiveShop = false);
            }
            else
            {
                // Di chuyển camera theo nhân vật
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindObjectOfType<Player>();
    }

    public void PlayGame()
    {
        isplay = true;
        isMainMenu = false;
        isActiveShop = false;
    }

    public void MainMenu()
    {
        isplay = false;
        isMainMenu = true;
        isActiveShop = false;
    }

    public void Shop()
    {
        isplay = false;
        isMainMenu = false;
        isActiveShop = true;
    }

    public void Wait(UnityAction callBack)
    {
        StartCoroutine(IEWait(callBack));
    }

    private IEnumerator IEWait(UnityAction callBack)
    {
        yield return new WaitForSeconds(0.3f);
        offset = transform.position - player.transform.position;
        callBack?.Invoke();
    }

}
