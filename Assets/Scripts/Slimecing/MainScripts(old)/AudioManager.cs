using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Sounds
    public Sound[] sounds;

    //Music
    public Sound[] musics;
    int num_music = 0;
    public Sound music;


    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        Screen.SetResolution(1280, 600, false);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    //Return the Sound object with the name given. Used by other objets to access the sounds they want to play.
    public Sound Find(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound:" + name + " not found!");
        }
        return s;
    }

    //Mute all SFX sounds
    public void MuteAllSounds()
    {
        foreach (Sound s in sounds)
            s.source.mute = true;
    }

    public void PauseAll()
    {
        foreach (Sound s in sounds)
            s.source.Pause();
        music.source.Pause();
    }
    //Resume all sounds.
    public void ResumeAll()
    {
        foreach (Sound s in sounds)
            s.source.UnPause();
        music.source.UnPause();
    }

    //Change the music for the next one in the array musics[]
    public void nextMusic()
    {
        if (musics.Length - 1 > num_music)
            num_music++;
        else
            num_music = 0;

        music.source.Stop(); 
        
        music.source.clip = musics[num_music].clip;
        music.source.volume = musics[num_music].volume;
        music.source.pitch = musics[num_music].pitch;
        music.source.Play();
    }

    public static AudioManager getInstance()
    {
        return instance;
    }
}
