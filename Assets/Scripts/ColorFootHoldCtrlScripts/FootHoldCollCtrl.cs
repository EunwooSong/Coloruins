using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHoldCollCtrl : MonoBehaviour
{
    BoxCollider2D boxColl;

    SpriteRenderer sr;
    Color paintColor;

    public enum Mycolor
    {
        blue, orange
    }
    public Mycolor color;
    public bool isColl;
    private bool isChanged;
    Color firstPaintColor;

    private void Awake()
    {
        if (!isColl)
            sr = GetComponent<SpriteRenderer>();

        SetColor(color); //기본 색은 파랑색

        boxColl = GetComponent<BoxCollider2D>();
        isChanged = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("ColorCircle"))
        {
            PaintCircleCtrl pccInfo = coll.gameObject.GetComponent<PaintCircleCtrl>();  //설정을 위해 스크립트 가져옴

            //같은 색이면 충돌을 꺼서 내려가도록 설정
            if (paintColor == pccInfo.sr.color)
            {
                Debug.Log("Tigger On!");
                boxColl.isTrigger = true;
            }
            //다른 색이면 충돌을 켜서 밟을수 있도록 설정
            if (paintColor != pccInfo.paintColor)
            {
                boxColl.isTrigger = false;
            }
                
        }
        
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("ColorCircle") && !isChanged)
        {
            PaintCircleCtrl pccInfo = coll.gameObject.GetComponent<PaintCircleCtrl>();  //설정을 위해 스크립트 가져옴
            //같은 색이면 충돌을 꺼서 내려가도록 설정
            //같은 색이면 충돌을 꺼서 내려가도록 설정
            if (paintColor == pccInfo.sr.color)
            {
                Debug.Log("Tigger On!");
                boxColl.isTrigger = true;
            }
            //다른 색이면 충돌을 켜서 밟을수 있도록 설정
            if (paintColor != pccInfo.paintColor)
            {
                boxColl.isTrigger = false;
            }

            isChanged = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ColorCircle"))
        {
            Debug.Log("Trigger false!");
            boxColl.isTrigger = false;
            isChanged = false;
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

        if(!isColl)
            sr.color = paintColor;  //색 설정 
    }
}
