using System.Collections;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public AudioSource GetSource()
    {
        return source;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume/2f, randomVolume/2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;
    [SerializeField]
    Sound[] monsterCries;
    [SerializeField]
    Sound[] musicTracks;

    Sound currentMusic;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 Sound Manager");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameObject soundsRoot = new GameObject("Sounds");
        GameObject musicRoot = new GameObject("Music");
        GameObject cryRoot = new GameObject("Cries");

        soundsRoot.transform.SetParent(this.transform);
        musicRoot.transform.SetParent(this.transform);
        cryRoot.transform.SetParent(this.transform);


        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(soundsRoot.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        for (int i = 0; i < musicTracks.Length; i++)
        {
            GameObject _go = new GameObject("music_" + i + "_" + musicTracks[i].name);
            _go.transform.SetParent(musicRoot.transform);
            musicTracks[i].SetSource(_go.AddComponent<AudioSource>());
        }

        for (int i = 0; i < monsterCries.Length; i++)
        {
            GameObject _go = new GameObject("cry_" + i + "_" + monsterCries[i].name);
            _go.transform.SetParent(cryRoot.transform);
            monsterCries[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("SoundManager: Sound not found with name " + _name);
    }

    public Sound FindSound(string _find)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _find)
            {
                return sounds[i];
            }
        }

        return null;
    }

    public void PlayCry(string _name)
    {
        for (int i = 0; i < monsterCries.Length; i++)
        {
            if (monsterCries[i].name == _name)
            {
                monsterCries[i].Play();
                return;
            }
        }

        Debug.LogWarning("SoundManager: Cry not found with name " + _name);
    }

    public void PlayFaintCry(string _name)
    {
        for (int i = 0; i < monsterCries.Length; i++)
        {
            if (monsterCries[i].name == _name)
            {
                float originalPitch = monsterCries[i].pitch;
                monsterCries[i].pitch = monsterCries[i].pitch * 0.75f;
                monsterCries[i].Play();
                StartCoroutine(resetPitch(monsterCries[i], originalPitch));
                return;
            }
        }

        Debug.LogWarning("SoundManager: Cry not found with name " + _name);
    }

    public void PlayMusic(string _name)
    {
        for (int i = 0; i < musicTracks.Length; i++)
        {
            if (musicTracks[i].name == _name)
            {
                if (currentMusic != null)
                {
                    currentMusic.Stop();
                }
                musicTracks[i].Play();
                currentMusic = musicTracks[i];
                return;
            }
        }

        Debug.LogWarning("SoundManager: Music track not found with name " + _name);
    }

    public void StopMusic()
    {
        currentMusic.Stop();
    }

    IEnumerator resetPitch(Sound sound, float originalPitch)
    {
        while (sound.GetSource().isPlaying)
        {
            yield return null;
        }

        sound.pitch = originalPitch;
    }
}
