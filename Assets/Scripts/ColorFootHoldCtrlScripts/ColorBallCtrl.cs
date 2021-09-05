using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBallCtrl : MonoBehaviour
{
    bool isPressed;         //터치 중인지 확인
    float releaseDelay;     //언제부터 충돌을 시작할 것인지
    float maxDragDis = 2f;  //드래그 되는 최대 범위

    Transform center;
    PlayerCtrl player;

    //볼의 콜라이더
    [Header("-=Rigid, Collider=-")]
    public CircleCollider2D circle;    
    Rigidbody2D rigid;      
    Rigidbody2D centerRG;

    [Header("-=Boll Pos, Sprite Component=-")]
    //볼 위치, 색
    public Transform tr;
    public SpriteRenderer sr;
    //색 정보
    public bool isBlue;
    
    SpringJoint2D sj;

    //주황, 파랑 색
    [Header("-=PaintBoomObject=-")]
    public GameObject boom_blue;
    public GameObject boom_orange;

    public GameObject shootButton;
    
    //터치 감지
    [Header("-=Touch UI=-")]
    public GameObject touchPad;
    public GameObject bollSizeSlider;
    public GameObject chageColor;

    [Header("-=values=-")]
    [SerializeField] bool isShoot = false;   //많이 생성되는 오류
    [SerializeField] bool ready = true;      //엘리베이터에서 발사시 오류
    [SerializeField] int paintOrder = 0;
    [SerializeField] Vector2 saveSize;

    // Start is called before the first frame update
    void Start()
    {
        //컴포넌트 불러옴
        rigid = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sj = GetComponent<SpringJoint2D>();
        sr = GetComponent<SpriteRenderer>();

        //플레이어의 transform 받아와 저장
        center = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        circle = GetComponent<CircleCollider2D>();
        
        //발사체의 중심축 설정
        centerRG = sj.connectedBody;

        //충돌 처리 딜레이 - [1 / (SpringJoint2D.frequency * 4)]
        releaseDelay = 1 / (sj.frequency * 4);

        if (isBlue)
            SetBlue();

        else
            SetOrange();

        //공의 크기 저장
        saveSize = new Vector2(1, 1);
        boom_blue.GetComponent<Transform>().localScale = saveSize;
        boom_orange.GetComponent<Transform>().localScale = saveSize;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 눌러졌 을 때 드래그 볼
        if (isPressed)
        {
            DragBall();
        }
        
        else if(ready)
        { 
            tr.position = center.position;
        }
    }

    void DragBall()
    {
        //터치 포지션 받아와서 볼에 적용
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //거리를 확인하여 최대 거리 이상이면 최대 거리 적용
        float dis = Vector2.Distance(touchPos, centerRG.position);

        if(dis > maxDragDis)
        {
            Vector2 dir = (touchPos - centerRG.position).normalized;
            rigid.position = centerRG.position + dir * maxDragDis;
        }
        else
        {
            rigid.position = touchPos;
        }
        
    }

    //이벤트 시스템에서 처리
    public void OnTouchDown()
    {
        isPressed = true;
        rigid.isKinematic = true;
        circle.enabled = false;
    }

    public void OnTouchUp()
    {
        //오류 해결을 위함
        isShoot = false;
        ready = false;

        //DragBall() 비활성화
        isPressed = false;

        //물리적인 연산이 가능하도록
        rigid.isKinematic = false;

        StartCoroutine(Release());

        //터치 입력 제어
        shootButton.SetActive(false);
        touchPad.SetActive(false);

        //플레이어 이동 제어
        player.isShoot = false;

        //UI Animation
        bollSizeSlider.GetComponent<UIAnimation>().IsNotNeed();
        chageColor.GetComponent<UIAnimation>().IsNotNeed();

    }

    //색 변경 함수
    public void SetOrange()
    {
        isBlue = false;
        sr.color = new ColorInfo().orange;
    }

    public void SetBlue()
    {
        isBlue = true;
        sr.color = new ColorInfo().blue;
    }

    
    IEnumerator Release()
    {
        //터치을 종료하였을 때 바로 충돌을 막음
        yield return new WaitForSeconds(releaseDelay);
        sj.enabled = false;
        circle.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FootHold")) && !isShoot)
        {
            isShoot = true;
            rigid.velocity = new Vector2(0.0f, 0.0f);   //속도 초기화
            circle.enabled = false;                     //충돌이 초기화
            gameObject.SetActive(false);                //숨겨서 다시 재사용

            float size = bollSizeSlider.GetComponentInChildren<Slider>().value;   //사이즈 지정을 위함

            if (isBlue)
            {
                boom_blue.GetComponent<Transform>().localScale = saveSize * size;
                Instantiate(boom_blue, tr.position, tr.rotation);
            }
            else
            {
                boom_orange.GetComponent<Transform>().localScale = saveSize * size;
                Instantiate(boom_orange, tr.position, tr.rotation);
            }
                

            sj.enabled = true;                          //다시 연결해줌
            shootButton.SetActive(true);
        }
    }

    //발사 초기화, 버튼에서 실행
    public void ShootSet()
    {
        tr.position = center.position;
        touchPad.SetActive(true);
        gameObject.SetActive(true);

        //플레이어 이동 제어
        player.isShoot = true;
        rigid.isKinematic = true;
        ready = true;
    }
}
