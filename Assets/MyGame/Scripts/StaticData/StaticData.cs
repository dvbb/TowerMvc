using System;
using UnityEngine;

public class StaticData : Singleton<StaticData>
{
    #region Game Setting

    public void SetResolution()
    {
        int index = PlayerPrefs.GetInt(Consts.Key_Resolution);
        SetResolution(index);
    }

    public void SetResolution(int index)
    {
        PlayerPrefs.SetInt(Consts.Key_Resolution, index);
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                Screen.SetResolution(1680, 960, false);
                break;
            case 2:
                Screen.SetResolution(1280, 960, false);
                break;
            case 3:
                Screen.SetResolution(1024, 768, false);
                break;
            case 4:
                Screen.SetResolution(800, 600, false);
                break;
            default:
                break;
        }
    }

    public void SaveBgmValue(float value) => PlayerPrefs.SetFloat(Consts.Key_BgmValue, value);
    public float LoadBgmValue() => PlayerPrefs.GetFloat(Consts.Key_BgmValue);
    public void SaveSeValue(float value) => PlayerPrefs.SetFloat(Consts.Key_SeValue, value);
    public float LoadSeValue() => PlayerPrefs.GetFloat(Consts.Key_SeValue);
    #endregion
}
