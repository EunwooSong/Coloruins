using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCtrl : MonoBehaviour
{
    public Vector2 topPos;
    public Vector2 botPos;
    public float moveSpeed;
    public float waitSec;
    Transform tr;
    Rigidbody2D rigid;

    public bool up;
    public bool wait; 

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();

        //현재위치, 최대 위, 최대 아래 포지션 구함
        topPos += new Vector2(tr.position.x, tr.position.y);
        botPos += new Vector2(tr.position.x, tr.position.y);

        up = true;

        StartCoroutine(ElevatorMoveCtrl());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!wait)
        {
            if (up)
                rigid.velocity = Vector2.up * moveSpeed;
            else
                rigid.velocity = Vector2.down * moveSpeed;
        }

        else
            rigid.velocity = Vector2.zero;
    }

    IEnumerator ElevatorMoveCtrl()
    {
        while(true)
        {
            yield return null;

            if (up)
            {
                if (Vector2.Distance(tr.position, topPos) <= 0.5f)
                {
                    up = false;
                    wait = true;
                    yield return new WaitForSeconds(waitSec);
                    wait = false;
                }
            }

            else
            {
                if (Vector2.Distance(tr.position, botPos) <= 0.5f)
                {
                    up = true;
                    wait = true;
                    yield return new WaitForSeconds(waitSec);
                    wait = false;
                }
            }
        }
    }
}
