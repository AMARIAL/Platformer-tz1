using UnityEngine;
public enum Sound
{
    attack,
    death,
    hit,
    jump,
    landing,
    skeletonDeath,
    apple,
    coin,
    ouch,
    savepoint,
    wolfAttack,
    wolfDeath,
    button,
}
public enum Music
{
    game,
    win,
    lose
}
public class Audio : MonoBehaviour
{
    public static Audio ST  {get; private set;} // Audio.ST (Singltone)
    
    public bool MusicOn = true;
    public bool SoundOn = true;
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private Transform soundAudio;
    [SerializeField] private AudioClip[] musicList;
    private AudioSource[] soundList;
    private void Awake()
    {
        if(ST)
            Destroy(gameObject);
        else
            ST = this;
        
        soundList = soundAudio.GetComponentsInChildren<AudioSource>();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("MUSIC") && PlayerPrefs.GetString("MUSIC") == "OFF")
            MusicOn = false;
        
        if (PlayerPrefs.HasKey("SOUND") && PlayerPrefs.GetString("SOUND") == "OFF")
            SoundOn = false;
        
        if (!MusicOn)
            musicAudio.Stop();
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
        if (SoundOn) 
            soundList[(int)newSound].Play();
    }
}