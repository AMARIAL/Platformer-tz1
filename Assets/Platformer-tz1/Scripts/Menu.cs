using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu ST  {get; private set;}
    
    [SerializeField] private GameObject windows;
    [SerializeField] private GameObject MenuWindow;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject WinWindow;

    private void Awake()
    {
        ST = this;
        windows.SetActive(false);
        MenuWindow.SetActive(false);
        GameOverWindow.SetActive(false);
        WinWindow.SetActive(false);
    }

    private void Start()
    {
        if (GameManager.ST.currentState != GameState.Menu)
        {
            windows.SetActive(false);
        }
    }

    public void MenuButton()
    {
        if(GameManager.ST.currentState != GameState.Game) return;
        
        GameManager.ST.ChangeState(GameState.Pause);
        windows.SetActive(true);
        MenuWindow.SetActive(true); 
    }
    
    public void OpenMenu()
    {
        GameManager.ST.ChangeState(GameState.Menu);
        windows.SetActive(true);
        MenuWindow.SetActive(true);
    }
    public void OpenGameOver()
    {
        GameManager.ST.ChangeState(GameState.Menu);
        Audio.ST.PlayMusic(Music.lose);
        windows.SetActive(true);
        GameOverWindow.SetActive(true);
    }
    public void OpenWin()
    {
        GameManager.ST.ChangeState(GameState.Menu);
        windows.SetActive(true);
        WinWindow.SetActive(true); 
    }
    public void StartGame()
    {
        GameManager.ST.ChangeState(GameState.Game);
        Audio.ST.PlayMusic(Music.game);
        windows.SetActive(false);
        MenuWindow.SetActive(false); 
    }
    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
