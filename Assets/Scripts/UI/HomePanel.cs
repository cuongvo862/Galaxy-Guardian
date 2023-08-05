using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_HighScoreTxt;
    //private UIManager m_UIManager;
    private void OnEnable()
    {
        m_HighScoreTxt.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore");

    }

    // Start is called before the first frame update
    void Start()
    {
        //m_UIManager = FindObjectOfType<UIManager>();  
    }
    public void PlayBtn()
    {
        UIManager.Instance.Play();
    }
}
