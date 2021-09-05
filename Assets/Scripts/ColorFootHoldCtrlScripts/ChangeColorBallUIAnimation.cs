using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorBallUIAnimation : MonoBehaviour
{
    public Vector2 downPos;
    public Vector2 defaultPos;
    public float smoothSpeed;
    public bool isPlayerNeed;

    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        defaultPos = tr.position;
        downPos = new Vector2(defaultPos.x, defaultPos.y - 600f);
    }

    // Update is called once per frame
    void Update()
    {
        //필요하면 위로 올라옴
        if(isPlayerNeed == true)
            tr.position = Vector3.Lerp(tr.position, defaultPos, smoothSpeed * Time.deltaTime);

        //필요가 없으면 밑으로 내려감
        else
            tr.position = Vector3.Lerp(tr.position, downPos, smoothSpeed * Time.deltaTime);

    }

    public void IsNeed()
    {
        isPlayerNeed = true;
    }

    public void IsNotNeed()
    {
        Debug.Log("IsNotNeed");
        isPlayerNeed = false;
    }
}
