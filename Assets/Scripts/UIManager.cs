using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Home,
    GamePlay,
    Pause,
    GameOver
}

public class UIManager : MonoBehaviour
{
    private static UIManager m_Instance;
    public static UIManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<UIManager>();
            return m_Instance;
        }
    }

    public Action<int> onScoreChanged;

    [SerializeField] private HomePanel m_HomePanel;
    [SerializeField] private GamePlayPanel m_GamePlayPanel;
    [SerializeField] private PausePanel m_PausePanel;
    [SerializeField] private GameOverPanel m_GameOverPanel;
    [SerializeField] private WaveData[] m_Waves;

    //private AudioManager m_AudioManager;
    //private SpawnManager m_SpawnManager;
    private GameState m_GameState;
    private bool m_Win;
    private int m_Score;
    private int m_CurrentWaveIndex;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        //m_AudioManager = FindObjectOfType<AudioManager>();
        //m_SpawnManager = FindObjectOfType<SpawnManager>();
        m_HomePanel.gameObject.SetActive(false);
        m_GamePlayPanel.gameObject.SetActive(false);
        m_PausePanel.gameObject.SetActive(false);
        m_GameOverPanel.gameObject.SetActive(false);
        SetState(GameState.Home);
    }

    private void SetState(GameState state)
    {
        m_GameState = state;
        m_HomePanel.gameObject.SetActive(m_GameState == GameState.Home);
        m_GamePlayPanel.gameObject.SetActive(m_GameState == GameState.GamePlay);
        m_PausePanel.gameObject.SetActive(m_GameState == GameState.Pause);
        m_GameOverPanel.gameObject.SetActive(m_GameState == GameState.GameOver);

        if (m_GameState == GameState.Pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (m_GameState == GameState.Home)
            AudioManager.Instance.PlayHomeMusic();
        else
            AudioManager.Instance.PlayBattleMusic();
    }
    public bool  IsActive()
    {
        return m_GameState == GameState.GamePlay;
    }

    public void Play()
    {
        m_CurrentWaveIndex = 0;
        WaveData wave = m_Waves[m_CurrentWaveIndex];
        SpawnManager.Instance.StartBattle(wave, true);
        SetState(GameState.GamePlay);
        m_Score = 0;
        if (onScoreChanged != null)
            onScoreChanged(m_Score);
    }
    public void Pause()
    {
        SetState(GameState.Pause);
    }
    public void Home()
    {
        SetState(GameState.Home);
        SpawnManager.Instance.CLear();
    }
    public void Resume()
    {
        SetState(GameState.GamePlay);
    }
    public void GameOver(bool win)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore");
        if (currentHighScore < m_Score)
        {
            PlayerPrefs.SetInt("HighScore", m_Score);
            currentHighScore = m_Score;
        }

        m_Win = win;
        SetState(GameState.GameOver);
        m_GameOverPanel.DisplayResult(m_Win);
        m_GameOverPanel.DisplayHighScore(currentHighScore);
    }
    public void AddScore(int value)
    {
        m_Score += value;
        if (onScoreChanged != null)
            onScoreChanged(m_Score);
        if (SpawnManager.Instance.IsClear())
        {
            m_CurrentWaveIndex++;
            if (m_CurrentWaveIndex >= m_Waves.Length)
                GameOver(true);
            else
            {
                WaveData wave = m_Waves[m_CurrentWaveIndex];
                SpawnManager.Instance.StartBattle(wave, false);
            }
        }
    }
}