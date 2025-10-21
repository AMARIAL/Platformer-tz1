using System.Collections.Generic;
using UnityEngine;
public enum Sound
{
    crowds,
    door,
    jump,
    hit,
    spring,
    teleport,
    checkpoint,
    gravity,
    take,
    button,
    hitpoint
}
public enum Music
{
    game = 0,
    win = 1,
    lose = 2
}
public class Audio : MonoBehaviour
{
    public static Audio ST  {get; private set;} // Audio.ST (Singltone)
    
    public bool MusicOn = true;
    public bool SoundOn = true;

    [SerializeField] private AudioClip[] musicList;
    
    private Dictionary<Sound, AudioSource> sound;

    private AudioSource musicAudio;
    
    private void Awake()
    {
        ST = this;
        sound = new Dictionary<Sound, AudioSource>();
        musicAudio = GetComponent<AudioSource>();

    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("MUSIC") && PlayerPrefs.GetString("MUSIC") == "OFF")
            MusicOn = false;
        
        if (PlayerPrefs.HasKey("SOUND") && PlayerPrefs.GetString("SOUND") == "OFF")
            SoundOn = false;
        
        if (!MusicOn)
        {
            musicAudio.Stop();
        }
    }
    
    public void SoundOnOff(bool isOn = true)
    {
        SoundOn = isOn;
        PlayerPrefs.SetString("SOUND", SoundOn ? "ON" : "OFF");
        PlayerPrefs.Save();
    }
    
    public void MusicOnOff(bool isOn = true)
    {
        MusicOn = isOn;
        
        PlayerPrefs.SetString("MUSIC", MusicOn ? "ON" : "OFF");
        PlayerPrefs.Save();
        
        if (MusicOn)
            musicAudio.Play();
        else
            musicAudio.Stop();
        
    }
    
    public void PlayMusic(Music newMusic)
    {
        musicAudio.clip = musicList[(int)newMusic];
        
        if(MusicOn)
            musicAudio.Play();
    }

    public void PauseMusic()
    {
        if(musicAudio.isPlaying)
            musicAudio.Pause();
        else
            musicAudio.Play();
    }
    public void PlaySound(Sound newSound)
    {
        if (!SoundOn) 
            return;
        
        sound[newSound].Play();
    }
}