using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<RemoveObject> remove;
    public Vector2 pressButton;
    public Vector2 moveValue;
    public float AnimSpeed;
    public bool isColl;

    Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
        isColl = false;
        pressButton = tr.position;
        pressButton -= moveValue;
    }

    private void Update()
    {
        if(isColl)
        {
            tr.position = Vector2.Lerp(tr.position, pressButton, AnimSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Monster")) && !isColl)
        {
            foreach(RemoveObject remove in remove)
            {
                Debug.Log("remove" + gameObject.name);
                remove.count++;
            }
            isColl = true;
        }
    }
}
