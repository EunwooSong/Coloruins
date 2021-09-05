using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCtrl : MonoBehaviour
{
    PolygonCollider2D boxColl;
    SpriteRenderer sr;
    Color paintColor;
    bool isCanHit;
    public bool isStatic;
    public GameObject gameOver;

    public enum Mycolor
    {
        blue, orange
    }
    public Mycolor color;
    public bool isColl;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if(!isStatic)
            SetColor(color); //기본 색은 파랑색

        boxColl = GetComponent<PolygonCollider2D>();
        isCanHit = true;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("ColorCircle"))
        {

            PaintCircleCtrl circleColor = coll.gameObject.GetComponent<PaintCircleCtrl>();  //설정을 위해 스크립트 가져옴

            //같은 색이면 충돌을 꺼서 내려가도록 설정
            if (paintColor == circleColor.sr.color && !isStatic)
            {
                Debug.Log("test");
                boxColl.isTrigger = true;
                isCanHit = false;
            }


            if (paintColor != circleColor.paintColor && !isStatic)   //다른 색이면 충돌을 켜서 밟을수 있도록 설정
            {
                boxColl.isTrigger = false;
                isCanHit = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColorCircle"))
        {
            Debug.Log("Trigger false!");
            boxColl.isTrigger = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if (isCanHit == true)
                gameOver.SetActive(true);
        }
    }

    public void SetColor(Mycolor type)
    {
        color = type;

        switch (color)
        {
            case Mycolor.blue: paintColor = new ColorInfo().blue; break;       //파랑색으로 변경
            case Mycolor.orange: paintColor = new ColorInfo().orange; break;      //주황색으로 변경
        }

        if (!isColl)
            sr.color = paintColor;  //색 설정
    }
}
