using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState: byte {Game, Pause}
public class GameManager : MonoBehaviour
{
    public static GameManager ST  {get; private set;}
    public int lives;
    public int levelNum;
    public int coins { get; private set; }
    public GameState currentState { get;  private set;}
    public event Action livesChanged;
    public event Action coinsChanged;
    [HideInInspector] public CheckPoint checkPoint;
    private Vector3 startPosition;
    
    public Dictionary<GameObject, Health> healthContainer = new Dictionary<GameObject, Health>();
    
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Contains('-'))
            int.TryParse(SceneManager.GetActiveScene().name.Split('-')[1], out levelNum);
        
        if (ST)
            Destroy(gameObject);
        else
            ST = this;
    }
    
    private void Start()
    {
        if (levelNum == 0)
        {
            ChangeState(GameState.Pause);
            Menu.ST.OpenMenu(false);
            Audio.ST.PlayMusic(Music.win);
        }
        else
        {
            ChangeState(GameState.Game);
            livesChanged?.Invoke();
            Audio.ST.PlayMusic(Music.game);
            startPosition = Player.ST.transform.position;
        }
    }

    public void CoinsChanged(bool reCount)
    {
        if(!reCount)
            coins++;
        coinsChanged?.Invoke();
    }

    private void ChangeLives(bool flag = false)
    {
        if (flag)
            lives++;
        else if(lives > 0)
            lives--;
        
        livesChanged?.Invoke();
    }
    private void DoPause(bool flag = true)
    {
        Time.timeScale = flag ? 0: 1;
    }

    public void ChangeState (GameState gameState)
    {
        if(currentState == gameState) return;
        currentState = gameState;
        DoPause(currentState == GameState.Pause);
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
        Player.ST.transform.position = checkPoint ? checkPoint.transform.position : startPosition;
        Player.ST.Restart();
        yield return null;
    }
    public void NewCheckPoint(CheckPoint _checkPoint)
    {
        checkPoint = _checkPoint;
    }
}
