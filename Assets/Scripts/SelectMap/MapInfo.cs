using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfo : MonoBehaviour
{
    public string mapName;
    public string sceneName;
    public Sprite MapImage;
    public bool isClear;

    public Text mapNameText;
    public Image mapImageSp;

    // Start is called before the first frame update
    void Start()
    {
        mapNameText = GameObject.Find("MapName").GetComponent<Text>();
        mapImageSp = GameObject.Find("MapImage").GetComponent<Image>();
    }

    public void SetMapInfo()
    {
        mapNameText.text = mapName;
        mapImageSp.sprite = MapImage;
        SceneInfo.sceneName = sceneName;
    }
}
