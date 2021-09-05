using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //소리나는 위치 - 플레이어가 이동할 때(점프), 페인트가 터질 때, 게임 브금
    //모두 자동으로 불러옴
    [Header("AudioManager")]
    public List<AudioSource> audios;
    public Slider slider;

    public bool isMain;
    private float value;

    // Start is called before the first frame update
    void Start()
    {
        // - 효과음 추가
        //게임 브금
        if (!isMain)
        {
            audios.Add(GetComponent<AudioSource>());
            //이동 효과음
            audios.Add(GameObject.Find("Player").GetComponent<AudioSource>());
            //폭발 효과음
            audios.Add((Resources.Load("Paint_blue") as GameObject).GetComponent<AudioSource>());
            audios.Add((Resources.Load("Paint_orange Variant") as GameObject).GetComponent<AudioSource>());
        }
        // - 슬라이더 추가
        //slider = GameObject.Find("SoundSlider").GetComponent<Slider>();

        slider.value = AudioValue.value;
        StartCoroutine(valueCtrl());
    }

    // Update is called once per frame
    void Update()
    {
        if(value != slider.value)
        {
            StartCoroutine(valueCtrl());    
        }


    }

    IEnumerator valueCtrl()
    {
        foreach(AudioSource audio in audios)
        {
            yield return null;
            audio.volume = slider.value;
        }

        value = slider.value;
        AudioValue.value = value;
    }
}
