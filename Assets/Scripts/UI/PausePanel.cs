using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
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
    public void ResumeBtn()
    {
        UIManager.Instance.Resume();
    }
}
