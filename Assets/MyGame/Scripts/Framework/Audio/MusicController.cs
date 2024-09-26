using UnityEngine;

public class MusicController : MonoSingleton<MusicController>
{
    private AudioSource music;

    public float Music_Volume
    {
        get { return music.volume; }
        set { music.volume = value; }
    }
    protected override void Awake()
    {
        base.Awake();

        if (GetComponent<AudioSource>() == null)
        {
            music = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            music = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Play an auidio
    /// </summary>
    /// <param name="enumName">Enums/FooEnum Type(The actual corresponding location of the file)</param>
    /// <param name="isLoop">is audio loop</param>
    public void PlayMusicByName(object enumName, bool isLoop = false)
    {
        var clip = ResourcesLoadTool.Instance.ResourceLoadObject<AudioClip>(enumName);
        music.clip = clip;
        music.loop = isLoop;
        music.Play();
    }
}
