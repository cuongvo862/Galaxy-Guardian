using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreTxt;
    [SerializeField] private Image m_ImgHPBar;

    private void OnEnable()
    {
        UIManager.Instance.onScoreChanged += OnScoreChanged;
        SpawnManager.Instance.Player.onHPChanged += OnHPChanged;
    }

    private void OnDisable()
    {
        UIManager.Instance.onScoreChanged -= OnScoreChanged;
        SpawnManager.Instance.Player.onHPChanged -= OnHPChanged;

    }

    private void OnHPChanged(int currentHP, int maxHP)
    {
        m_ImgHPBar.fillAmount = currentHP * 1f / maxHP;
    }

    //private UIManager m_UIManager;

    // Start is called before the first frame update
    void Start()
    {
        //m_UIManager = FindObjectOfType<UIManager>();
    }
    public void PauseBtn()
    {
        UIManager.Instance.Pause();
    }
    private void OnScoreChanged(int score)
    {
        m_ScoreTxt.text = "SCORE: " + score;

    }
}
