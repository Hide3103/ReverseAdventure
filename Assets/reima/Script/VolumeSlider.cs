using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSlider : MonoBehaviour
{

    public GameObject SettingCanvas;
    public Slider BGMVolSlider;
    public Slider SEVolSlider;
    public GameObject Obj_BGMVolSlider;
    public GameObject Obj_SEVolSlider;
    public static float NowBGMVol;
    public static float NowSEVol;
    bool KeyInput = true;
    float ScroolSpeed = 5;

    public Text BGMVolumeTxt;
    public Text SEVolumeTxt;

    int NumSelect = 1;
    // Start is called before the first frame update
    void Start()
    {
        BGMVolSlider = GetComponent<Slider>();
        BGMVolSlider.value = AudioListener.volume;
        BGMVolSlider.maxValue = 100;
        BGMVolSlider.minValue = 0;
        BGMVolSlider.value = 100;

        SEVolSlider = GetComponent<Slider>();
        SEVolSlider.value = AudioListener.volume;
        SEVolSlider.maxValue = 100;
        SEVolSlider.minValue = 0;
        SEVolSlider.value = 100;
        float v = BGMVolSlider.value;
        float v2 = SEVolSlider.value;
    }

    private void OnEnable()
    {
       BGMVolSlider.value = AudioListener.volume;
       BGMVolSlider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);

        SEVolSlider.value = AudioListener.volume;
        SEVolSlider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        SEVolSlider.onValueChanged.RemoveAllListeners();
        BGMVolSlider.onValueChanged.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(NumSelect);
        NowBGMVol = BGMVolSlider.value;
        NowSEVol = SEVolSlider.value;
        BGMVolumeTxt.text = "" + NowBGMVol;
        SEVolumeTxt.text = "" + NowSEVol;
        if (KeyInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                NowBGMVol -= ScroolSpeed;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NowBGMVol += ScroolSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                NowSEVol -= ScroolSpeed;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NowSEVol += ScroolSpeed;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && NumSelect < 3)
        {
            NumSelect++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NumSelect > 1)
        {
            NumSelect--;
        }

        if (SettingCanvas.activeSelf==true)
        {
            switch(NumSelect)
            {
                case 1:
                    Obj_BGMVolSlider.SetActive(true);
                    Obj_SEVolSlider.SetActive(false);
                    break;
                case 2:
                    Obj_BGMVolSlider.SetActive(false);
                    Obj_SEVolSlider.SetActive(true);
                    break;
            }
        }
        NowBGMVol = Mathf.Clamp(NowBGMVol, 0, 100);
        BGMVolSlider.value = NowBGMVol;
        SEVolSlider.value = NowSEVol;
    }
}
