using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ResultTxt;
    [SerializeField] private TextMeshProUGUI m_HighScoreTxt;

    //private UIManager m_UIManager;
    // Start is called before the first frame update
    void Start()
    {
        //m_UIManager = FindObjectOfType<UIManager>(); 
    }
    public void HomeBtn()
    {
        UIManager.Instance.Home();
    }
    public void DisplayHighScore(int score)
    {
        m_HighScoreTxt.text = "HIGH SCORE: " + score;
    }
    public void DisplayResult(bool isWin)
    {
        if (isWin)
            m_ResultTxt.text = "YOU WIN";
        else
            m_ResultTxt.text = "YOU LOSE";
    }
}
