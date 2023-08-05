using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalFX : MonoBehaviour
{
    [SerializeField] private float m_LifeTime;

    private float m_CurrentLifeTime;
    private ParticalFXPool m_Pool;

    private void OnEnable()
    {
        m_CurrentLifeTime = m_LifeTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentLifeTime <= 0)
        {
            m_Pool.Release(this);
        }
        m_CurrentLifeTime -= Time.deltaTime;
    }

    public void SetPool(ParticalFXPool pool)
    {
        m_Pool = pool; ;
    }
}
