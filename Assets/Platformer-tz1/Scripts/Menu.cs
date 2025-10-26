using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu ST  {get; private set;}
    
    [SerializeField] private GameObject windows;
    [SerializeField] private GameObject MenuWindow;
    [SerializeField] private GameObject PauseWindow;
    [SerializeField] private GameObject LevelsWindow;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject WinWindow;
    
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        if(ST)
            Destroy(gameObject);
        else
        {
            ST = this;
            CleanWindows(false);
        }

        cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        if (GameManager.ST.currentState != GameState.Pause)
            windows.SetActive(false);

        if (Player.ST)
        {
            cinemachineVirtualCamera.Follow = Player.ST.transform;
            cinemachineVirtualCamera.LookAt = Player.ST.transform;
        }
    }
    
    public void OpenMenu(bool button)
    {
        if(button)
            Audio.ST.PlaySound(Sound.button);
        
        GameManager.ST.ChangeState(GameState.Pause);
        CleanWindows(true);
        MenuWindow.SetActive(true);
    }
    public void OpenGameOver()
    {
        GameManager.ST.ChangeState(GameState.Pause);
        Audio.ST.PlayMusic(Music.lose);
        CleanWindows(true);
        GameOverWindow.SetActive(true);
    }
    public void OpenWin()
    {
        GameManager.ST.ChangeState(GameState.Pause);
        Audio.ST.PlayMusic(Music.win);
        CleanWindows(true);
        WinWindow.SetActive(true); 
    }
    public void OpenLevels()
    {
        GameManager.ST.ChangeState(GameState.Pause);
        Audio.ST.PlaySound(Sound.button);
        CleanWindows(true);
        LevelsWindow.SetActive(true);
    }
    public void OpenPause()
    {
        GameManager.ST.ChangeState(GameState.Pause);
        Audio.ST.PlaySound(Sound.button);
        CleanWindows(true);
        PauseWindow.SetActive(true);
    }
    public void StartGame()
    {
        GameManager.ST.ChangeState(GameState.Game);
        Audio.ST.PlaySound(Sound.button);
        CleanWindows(false);
    }
    public void ReStartGame()
    {
        GameManager.ST.ChangeState(GameState.Game);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchMusic()
    {
        Audio.ST.MusicOnOff(!Audio.ST.MusicOn);
        Audio.ST.PlaySound(Sound.button);
    }
    public void SwitchSound()
    {
        Audio.ST.SoundOnOff(!Audio.ST.SoundOn);
        Audio.ST.PlaySound(Sound.button);
    }
    public void ChangeLevel(int i)
    {
        Audio.ST.PlaySound(Sound.button);

        if (i > 0 && i < SceneManager.sceneCountInBuildSettings)
        {
            GameManager.ST.ChangeState(GameState.Game);
            SceneManager.LoadScene("Level-"+i);
        }
        else
            SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        int i = GameManager.ST.levelNum + 1;
        if(SceneManager.sceneCountInBuildSettings > i)
            ChangeLevel(i);
    }

    private void CleanWindows(bool isWindows)
    {
        windows.SetActive(isWindows);
        MenuWindow.SetActive(false);
        PauseWindow.SetActive(false);
        LevelsWindow.SetActive(false);
        GameOverWindow.SetActive(false);
        WinWindow.SetActive(false);
    }
}
