using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    float ScroolSpeed = 5;

    public Text BGMVolumeTxt;
    public Text SEVolumeTxt;

   public int SettingNumSelect = 1;
    public AudioMixer AudioMixer;

    // Start is called before the first frame update
    void Start()
    {
        BGMVolSlider.value = AudioListener.volume;
        BGMVolSlider.maxValue = 20;
        BGMVolSlider.minValue = -80;
        BGMVolSlider.value = 100;

        SEVolSlider.value = AudioListener.volume;
        SEVolSlider.maxValue = 20;
        SEVolSlider.minValue = -80;
        SEVolSlider.value = 100;
        float v = BGMVolSlider.value;
        float v2 = SEVolSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingCanvas.activeSelf == true)
        {
            Debug.Log(SettingNumSelect);
            NowBGMVol = BGMVolSlider.value;
            NowSEVol = SEVolSlider.value;
            BGMVolumeTxt.text = "" + NowBGMVol;
            SEVolumeTxt.text = "" + NowSEVol;
            switch (SettingNumSelect)
            {

                case 1:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        NowBGMVol -= ScroolSpeed;
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        NowBGMVol += ScroolSpeed;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow) && SettingNumSelect == 1)
                    {
                        SettingNumSelect = 2;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        NowSEVol -= ScroolSpeed;
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        NowSEVol += ScroolSpeed;
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow) && SettingNumSelect == 2)
                    {
                        SettingNumSelect = 1;
                    }
                    break;

            }

            //if (SettingCanvas.activeSelf==true)
            //{
            //    switch(NumSelect)
            //    {
            //        case 1:
            //            Obj_BGMVolSlider.SetActive(true);
            //            Obj_SEVolSlider.SetActive(false);
            //            break;
            //        case 2:
            //            Obj_BGMVolSlider.SetActive(false);
            //            Obj_SEVolSlider.SetActive(true);
            //            break;
            //    }
            //}
            NowBGMVol = Mathf.Clamp(NowBGMVol, -80, 20);
            NowSEVol = Mathf.Clamp(NowSEVol, -80, 20);
            BGMVolSlider.value = NowBGMVol;
            SEVolSlider.value = NowSEVol;
            
        }
    }
    public void SetBGM(float BGMVol)
    {
        AudioMixer.SetFloat("BGMVol", BGMVol);
    }
    public void SetSE(float SEVol)
    {
        AudioMixer.SetFloat("BGMVol", SEVol);
    }
}
