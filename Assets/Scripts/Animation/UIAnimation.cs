using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    Transform tr;
    [Header("Smooth Move")]
    public float moveSpeed;
    public Transform firstOffset;
    [SerializeField]
    private Transform lastOffset;
    public bool isNeed = false;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        tr.position = firstOffset.position;     
    }

    // Update is called once per frame
    void Update()
    {
        if(isNeed)
        {
            tr.position = Vector2.Lerp(tr.position, lastOffset.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            tr.position = Vector2.Lerp(tr.position, firstOffset.position, moveSpeed * Time.deltaTime);
        }
    }

    public void PlayAnim()
    {
        //Debug.Log("Map Info ON!");
        isNeed = true;
    }

    public void IsNotNeed()
    {
        isNeed = false;
    }
}
