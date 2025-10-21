using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState: byte {Menu, Levels, Game, Pause, GameOver}
public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private Vector2 savePoint;
    public static GameManager ST  {get; private set;}
    public int levelNum;
    public GameState currentState { get;  private set;}

    private TextMeshProUGUI livesNum;
    private void Awake()
    {
        ST = this;
        if (SceneManager.GetActiveScene().name.Contains('-'))
            int.TryParse(SceneManager.GetActiveScene().name.Split('-')[1], out levelNum);
        
    }
    private void Start()
    {
        livesNum = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        livesNum.text = lives.ToString();
        savePoint = Player.ST.transform.position;

        if (levelNum == 1)
        {
            Menu.ST.OpenMenu();
            ChangeState(GameState.Menu);
            Audio.ST.PlayMusic(Music.win);
        }
        else
        {
            ChangeState(GameState.Game);
            Audio.ST.PlayMusic(Music.game);
        }
        
    }
    private void ChangeLives(bool flag = false)
    {
        if (flag)
            lives++;
        else if(lives > 0)
            lives--;
        livesNum.text = lives.ToString();
    }
    private void DoPause(bool flag = true)
    {
        if (flag)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        Audio.ST.PauseMusic();
    }

    public void ChangeState (GameState gameState)
    {
        currentState = gameState;
        
        DoPause(currentState == GameState.Pause || currentState == GameState.Menu);
    }

    public void PlayerDead ()
    {
        if(lives == 1)
            GameOver();
        else
            StartCoroutine(MoveToSavePoint());
    }
    public void GameOver ()
    {
        ChangeLives();
        Menu.ST.OpenGameOver();
    }
    private IEnumerator MoveToSavePoint()
    {
        yield return new WaitForSeconds(1.0f);
        ChangeLives();
        Player.ST.transform.position = savePoint;
        //Player.ST.state = Player.State.Idle;
        //Player.ST.Restart();
        yield return null;
    }
    public void NewCheckPoint(Vector2 pos)
    {
        savePoint = pos;
    }
}
