using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnimation : MonoBehaviour
{
    [Header("Blink Animation")]
    public Image target_Image;
    public Text target_Text;
    public float power = 5.0f;
    [SerializeField]
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!target_Image)
            if(GetComponent<Image>())
                target_Image = GetComponent<Image>();
            else
                Debug.Log("Image Is NULL / " + gameObject.name);

        if (!target_Text)
            if (GetComponent<Text>())
                target_Text = GetComponent<Text>();
            else
                Debug.Log("Text Is NULL / " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        timer += power * Time.deltaTime;

        if (target_Image)
            target_Image.color = new Vector4(target_Image.color.r,
                target_Image.color.g, target_Image.color.b, Mathf.Abs(Mathf.Cos(timer)));
        if (target_Text)
            target_Text.color = new Vector4(target_Text.color.r,
                target_Text.color.g, target_Text.color.b, Mathf.Abs(Mathf.Cos(timer)));

        if (timer * Mathf.Rad2Deg > 180)
            timer = 0.0f;
    }
}
