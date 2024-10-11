using UnityEngine;

public class MusicManager : MonoSingleton<MusicManager>
{
    private AudioSource audio_music;
    private AudioSource audio_effect;

    protected override void Awake()
    {
        base.Awake();
        // Init music audio
        audio_music = gameObject.AddComponent<AudioSource>();
        audio_music.loop = true;
        audio_music.playOnAwake = true;
        audio_music.volume = StaticData.Instance.LoadBgmValue();

        // Init effect audio
        audio_effect = gameObject.AddComponent<AudioSource>();
        audio_effect.loop = false;
        audio_effect.playOnAwake = false;
        audio_effect.volume = StaticData.Instance.LoadSeValue();
    }

    public void SetBgmValue(float value)
    {
        audio_music.volume = value;
        StaticData.Instance.SaveBgmValue(value);
    }

    public void SetSeValue(float value)
    {
        audio_effect.volume = value;
        StaticData.Instance.SaveSeValue(value);
    }

    /// <summary>
    /// Play an auidio
    /// </summary>
    /// <param name="enumName">Enums/FooEnum Type(The actual corresponding location of the file)</param>
    /// <param name="isLoop">is audio loop</param>
    private void PlayMusicByName(object enumName, bool isLoop = false)
    {
        var clip = ResourcesLoadTool.Instance.ResourceLoadObject<AudioClip>(enumName);
        audio_music.clip = clip;
        audio_music.loop = isLoop;
        audio_music.Play();
    }

    public void PlayMusic(MusicEnum.MusicType_Main music, bool isLoop = true) => PlayMusicByName(music, isLoop);
    public void PlayMusic(MusicEnum.MusicType_Bullet music, bool isLoop = true) => PlayMusicByName(music, isLoop);

    private void PlayEffectByName(object enumName, bool isEffect = true, float volume = 1f)
    {
        var clip = ResourcesLoadTool.Instance.ResourceLoadObject<AudioClip>(enumName);
        if (clip == null)
            return;

        if (isEffect)
            audio_effect.PlayOneShot(clip, volume);
        else
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void PlayEffect(MusicEnum.MusicType_Main effect, bool isEffect = true, float volume = 1f) => PlayEffectByName(effect, isEffect, volume);
    public void PlayEffect(MusicEnum.MusicType_Bullet effect, bool isEffect = true, float volume = 1f) => PlayEffectByName(effect, isEffect, volume);
}
