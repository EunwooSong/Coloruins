using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCtrl : MonoBehaviour
{
    Rigidbody2D _rigid;

    public Transform paintPivot;                //페인트 발사 위치
    public CircleCollider2D destructionRange;  //파괴 범위

    public GameObject paintTrail;

    public static FootHoldCtrl footHoldCtrl;    //파괴시킬 대상

    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BOOM!!!");

        if (collision.collider.CompareTag("FootHold"))
        {
            //if(collision.collider.GetComponent<SpriteRenderer>().color == color)
            //{
            
                footHoldCtrl.DestroyFootHold(destructionRange);
                //Destroy(gameObject);
            //}
        }
    }
}
