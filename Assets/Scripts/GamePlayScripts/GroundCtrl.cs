using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtrl : MonoBehaviour
{
    public PlayerCtrl player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            if (coll.gameObject.GetComponent<FootHoldCollCtrl>() == null)
            {
                Debug.Log("보통 바닥과 충돌");
                player.isOnGround = true;
            }
                

            else
            {
                if (coll.gameObject.GetComponent<BoxCollider2D>().isTrigger == true)
                {
                    Debug.Log("");
                    player.isOnGround = false;
                }
                    

                else {
                    player.isOnGround = true;
                }
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            if(coll.gameObject.GetComponent<FootHoldCollCtrl>() == null)
            {
                Debug.Log("보통 바닥 위에 있는중!");
                player.isOnGround = true;
            }
                

            else
            {
                if (coll.gameObject.GetComponent<BoxCollider2D>().isTrigger == true)
                {
                    Debug.Log("발판이 ");
                    player.isOnGround = false;
                }
                    

                else
                {
                    player.isOnGround = true;
                }
                    
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            player.isOnGround = false;
        }
    }
}
