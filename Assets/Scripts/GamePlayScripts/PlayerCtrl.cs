using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Header("-=Value=-")]
    public float moveSpeed;     //플에이어의 이동 속도
    public float jumpSpeed;     //플레이어의 점프 속도
    public bool isJump;         //플레이어가 점프중인지
    public bool isOnGround;     //플레이어가 땅에 닿았는지
    public string groundName;   //땅에대한 tag 이름 정보
    public bool isShoot;
    [Header("-=Player=-")]
    public float playerDir;         //플레이어의 방향(양 -> 오, 음 -> 왼)
    private Transform tr;
    public Transform playerModel;
    public Animator anim;
    float playerSizeX;              //플레이어 X사이즈 (플레이어를 반전시키기 위함)
    [Header("-=Sound=-")]
    public AudioClip walkSource;
    public AudioClip jumpSource;
    public AudioSource sfx;

    Vector2 moveDir;    //이동시킬 벡터 정보

    Rigidbody2D rigid;  //물리 가져옴(이동, 점프 설정)

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sfx = GetComponent<AudioSource>();
        playerDir = 0f;

        playerSizeX = playerModel.localScale.x;   //플에이어 x축 저장
    }

    void FixedUpdate()
    {

        Move();

        if (isJump)
            Jump();
    }

    void Move()
    {
        moveDir = Vector2.zero;
        moveDir.y = rigid.velocity.y;

        if (playerDir < 0 || playerDir > 0)
            moveDir.x = playerDir * moveSpeed;
            

        else
            moveDir.x = 0;

        //좌우 반전
        if (playerDir != 0)
        {
            playerModel.localScale = new Vector2(playerDir * playerSizeX, playerModel.localScale.y);
            anim.SetBool("isPlayerWalk", true);
            if (!sfx.isPlaying)
                sfx.Play(44100/2/2);
        }
        else
        {
            anim.SetBool("isPlayerWalk", false);

            if(!isJump)
                sfx.Stop();
        }
            

        rigid.velocity = moveDir;

        //rigidbody의 y축 변화량을 측정하여 캐릭터가 땅에 있는지 확인
        if(rigid.velocity.y == 0.0f)
        {
            Debug.Log("Is Ground!");
            isOnGround = true;
        }
        else
        {
            Debug.Log("Is Air!");
            isOnGround = false;

            if(!isJump)
                sfx.Stop();
        }
    }
    
    void Jump()
    {
        if (!isOnGround)
            return;

        sfx.PlayOneShot(jumpSource);
        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isOnGround = false;
    }

    
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Elevator"))
        {
            if(!coll.gameObject.GetComponent<ElevatorCtrl>().wait && !coll.gameObject.GetComponent<ElevatorCtrl>().up)
            {
                float elVelocityY = coll.gameObject.GetComponent<Rigidbody2D>().velocity.y;
                Debug.Log("OnElevator!!" + elVelocityY);
                rigid.velocity = new Vector2(rigid.velocity.x, elVelocityY);
            }

            else if(!coll.gameObject.GetComponent<ElevatorCtrl>().wait)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
            } 
        }
    }
} //19.25 48.5 12.3

