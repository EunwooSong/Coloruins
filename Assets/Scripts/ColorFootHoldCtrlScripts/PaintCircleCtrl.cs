using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCircleCtrl : MonoBehaviour
{

    [Header("-=Image=-")]
    public SpriteRenderer sr;              //색을 바꿔줄 대상
    public AudioSource sfx;
    public AudioClip boomSound;
    public Color paintColor;        //현재 페인트 색
    Color Blue;
    bool isOrdered;     //순서가 바뀌었는지 확인
    int renderOrder = 0;            //순서

    [Header("-=Remove=-")]
    public bool isRemove = true;
    public float removeDelay;


    public enum Mycolor
    {
        blue, orange
    }
    public Mycolor color;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetColor(color);
    }

    void Start()
    {
        sr.sortingOrder = ++SceneInfo.paintOrder;
        sfx = GetComponent<AudioSource>();
        

        if (isRemove)
        {
            sfx.PlayOneShot(boomSound);
            StartCoroutine(remove());
        }
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(removeDelay);
        Debug.Log("Remove");
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public void SetColor(Mycolor type)
    {
        color = type;

        switch (color)
        {
            case Mycolor.blue: paintColor = new ColorInfo().blue;  break;       //파랑색으로 변경
            case Mycolor.orange: paintColor = new ColorInfo().orange; break;      //주황색으로 변경
        }

        sr.color = paintColor;  //색 설정
    }
}
