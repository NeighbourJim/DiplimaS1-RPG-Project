using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SerializedOptions
{
    float brightness;
    public float Brightness
    {
        get { return brightness; }
    }

    float musicVolume;
    public float MusicVolume
    {
        get { return musicVolume; }
    }

    float sfxVolume;
    public float SFXVolume
    {
        get { return sfxVolume; }
    }

    TextSpeed textSpeed;
    public TextSpeed TextSpeed
    {
        get { return textSpeed; }
    }

    Resolution res;
    public Resolution Res
    {
        get { return res; }
    }

    bool antiAliasing;
    public bool AntiAliasing
    {
        get { return antiAliasing; }
    }

    public SerializedOptions()
    {
        brightness = 0f;
        musicVolume = 1f;
        sfxVolume = 1f;
        textSpeed = TextSpeed.Normal;
        res = Resolution.FullHD;
        antiAliasing = false;
    }

    public SerializedOptions(float b, float mV, float sfxV, TextSpeed ts, Resolution r, bool aa)
    {
        brightness = b;
        musicVolume = mV;
        sfxVolume = sfxV;
        textSpeed = ts;
        res = r;
        antiAliasing = aa;
    }

    public SerializedOptions(DataSettings settings)
    {
        brightness = settings.Brightness;
        musicVolume = settings.MusicVolume;
        sfxVolume = settings.SFXVolume;
        textSpeed = settings.TextSpeed;
        res = settings.Resolution;
        antiAliasing = settings.AntiAliasing;
    }
}

public class SettingsFileManager : MonoBehaviour {

    public DataSettings settings;

    public Slider brightness, musicVolume, sfxVolume;
    public Toggle tsSlow, tsNorm, tsFast;
    public Toggle antiAliasing;
    public Dropdown resolution;

    private void Start()
    {
        StartCoroutine(StartOptions());
    }

    IEnumerator StartOptions()
    {
        yield return null;
        SerializedOptions loaded = Load();
        brightness.value = loaded.Brightness;
        musicVolume.value = loaded.MusicVolume;
        sfxVolume.value = loaded.SFXVolume;

        switch(loaded.TextSpeed)
        {
            case TextSpeed.Slow:
                tsSlow.isOn = true;
                tsNorm.isOn = false;
                tsFast.isOn = false;
                break;
            case TextSpeed.Normal:
                tsSlow.isOn = false;
                tsNorm.isOn = true;
                tsFast.isOn = false;
                break;
            case TextSpeed.Fast:
                tsSlow.isOn = false;
                tsNorm.isOn = false;
                tsFast.isOn = true;
                break;
        }

        antiAliasing.isOn = loaded.AntiAliasing;

        resolution.value = (int)loaded.Res;
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream f = File.Create(Application.persistentDataPath + "/Settings.dat");
        SerializedOptions current = new SerializedOptions(settings);

        bf.Serialize(f, current);
        f.Close();
    }

    SerializedOptions Load()
    {
        SerializedOptions loaded = new SerializedOptions();
        if (File.Exists(Application.persistentDataPath + "/Settings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = File.Open(Application.persistentDataPath + "/Settings.dat", FileMode.Open);

            loaded = bf.Deserialize(f) as SerializedOptions;
            f.Close();
        }

        return loaded;
    }
}
