using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]Transform target;
    private Transform tr;
    [SerializeField] float camSmooth = 5.0f;
    [SerializeField] Vector2 targetOffset;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tr.position = Vector2.Lerp(tr.position, target.position, camSmooth * Time.deltaTime);    
    }
}