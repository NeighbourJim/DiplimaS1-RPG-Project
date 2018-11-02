using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextSpeed
{
    Slow,
    Normal,
    Fast
}

public enum Resolution
{
    FullHD,
    HD
}

[CreateAssetMenu()]
public class DataSettings : ScriptableObject {

    public Image BrightnessImage;

    [Range(0f,0.5f)]
    public float Brightness = 0f;
    [Range(0f, 1f)]
    public float MusicVolume = 1f;
    [Range(0f, 1f)]
    public float SFXVolume = 1f;

    public TextSpeed TextSpeed;

    public bool AntiAliasing;

    public Resolution Resolution;


    public void UpdateBrightness(float value)
    {
        Brightness = value;
        BrightnessImage.color = new Color(BrightnessImage.color.r, BrightnessImage.color.g, BrightnessImage.color.b, Brightness);
    }

    public void UpdateMusicVolume(float value)
    {
        MusicVolume = value;
    }

    public void UpdateSFXVolume(float value)
    {
        SFXVolume = value;
    }

    public void SetTextSpeedSlow()
    {
        TextSpeed = TextSpeed.Slow;
    }
    public void SetTextSpeedNorm()
    {
        TextSpeed = TextSpeed.Normal;
    }

    public void SetTextSpeedFast()
    {
        TextSpeed = TextSpeed.Fast;
    }

    public void ToggleAA(bool value)
    {
        AntiAliasing = value;
    }

    public void SetResolution(int value)
    {
        Resolution = (Resolution)value;
    }
}
