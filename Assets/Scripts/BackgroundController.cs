using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Material m_NebulaBg;
    [SerializeField] private float m_NebulaBgSpeed;
    [SerializeField] private Material m_BigStarsBg;
    [SerializeField] private float m_BigStarsBgSpeed;
    [SerializeField] private Material m_MedStarsBg;
    [SerializeField] private float m_MedStarsBgSpeed;

    private Vector2 m_CurBigStarsBgOffset;
    private Vector2 m_CurMedStarsBgOffset;
    private Vector2 m_CurNebulaBgOffset;

    private void Update()
    {
        m_CurNebulaBgOffset += new Vector2(0, m_NebulaBgSpeed * Time.deltaTime);
        m_NebulaBg.SetTextureOffset("_MainTex", m_CurNebulaBgOffset);

        m_CurBigStarsBgOffset += new Vector2(0, m_BigStarsBgSpeed * Time.deltaTime);
        m_BigStarsBg.SetTextureOffset("_MainTex", m_CurBigStarsBgOffset);

        m_CurMedStarsBgOffset += new Vector2(0, m_MedStarsBgSpeed * Time.deltaTime);
        m_MedStarsBg.SetTextureOffset("_MainTex", m_CurMedStarsBgOffset);
    }
}
