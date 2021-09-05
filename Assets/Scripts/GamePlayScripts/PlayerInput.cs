using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerCtrl player;
    //[SerializeField] Image buttonImage;

    //bool isFade;

    //플레이어 이동 스크립트 불러옴
    void Awake()
    {
        player = FindObjectOfType<PlayerCtrl>().GetComponent<PlayerCtrl>();
    }

    public void LeftArrowDown()
    {
        player.playerDir = -1f;
    }

    public void RightArrowDown()
    {
        player.playerDir = 1f;
    }

    public void UpArrowDown()
    {
        StartCoroutine(Jump());
    }

    public void AnyArrowUp()
    {
        player.playerDir = 0f;
    }

    IEnumerator Jump()
    {
        player.isJump = true;
        yield return new WaitForSeconds(0.3f);
        player.isJump = false;
    }
}
