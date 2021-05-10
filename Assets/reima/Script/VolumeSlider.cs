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

    private float BGMTextVal = 100;
    private float SETextVal = 100;

    private float PadWaitTime = 0;
    private float SetPadWaittime = 1;

    public GameObject Icon;
    public GameObject Icon2;


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
        SettingNumSelect = 1;
    }

    // Update is called once per frame
    void Update()
    {

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (PadWaitTime > 0)
        {
            PadWaitTime -= Time.deltaTime;
        }

        if (SettingCanvas.activeSelf == true)
        {
            Debug.Log(SettingNumSelect);
            NowBGMVol = BGMVolSlider.value;
            NowSEVol = SEVolSlider.value;
            BGMVolumeTxt.text = "" + BGMTextVal;
            SEVolumeTxt.text = "" + SETextVal;
            switch (SettingNumSelect)
            {

                case 1:
                    Icon.gameObject.SetActive(true);
                    Icon2.gameObject.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.LeftArrow)&&BGMTextVal>0 || hori < 0 && BGMTextVal > 0&&PadWaitTime<=0)
                    {
                        NowBGMVol -= ScroolSpeed;
                        BGMTextVal -= ScroolSpeed;
                        PadWaitTime += SetPadWaittime;
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && BGMTextVal < 100 || hori > 0 && BGMTextVal < 100 && PadWaitTime <= 0)
                    {
                        NowBGMVol += ScroolSpeed;
                        BGMTextVal += ScroolSpeed;
                        PadWaitTime += SetPadWaittime;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow) && SettingNumSelect == 1||vert<0 && SettingNumSelect == 1)
                    {
                        SettingNumSelect = 2;
                    }

                    break;
                case 2:
                    Icon.gameObject.SetActive(false);
                    Icon2.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && SETextVal > 0 || hori < 0 && SETextVal > 0 && PadWaitTime <= 0)
                    {
                        NowSEVol -= ScroolSpeed;
                        SETextVal -= ScroolSpeed;
                        PadWaitTime += SetPadWaittime;
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && SETextVal < 100 || hori > 0 && SETextVal < 100 && PadWaitTime <= 0)
                    {
                        NowSEVol += ScroolSpeed;
                        SETextVal += ScroolSpeed;
                        PadWaitTime += SetPadWaittime;
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow) && SettingNumSelect == 2 || vert > 0 && SettingNumSelect == 2)
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
