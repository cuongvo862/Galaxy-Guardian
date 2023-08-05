using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Transform[] m_WayPoints;
    //[SerializeField] private ProjectileController m_Projectile;
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private float m_MinFireCooldown;
    [SerializeField] private float m_MaxFireCooldown;
    [SerializeField] private int m_HP;

    private float m_TempCooldown;
    private int m_CurrentWayPointIndex;
    private bool m_Active;
    private int m_CurrentHP;
    private float m_CurrentMoveSpeed;
    private float m_SpeedMultiplier;
    //private SpawnManager m_SpawnManager;
    //private UIManager m_UIManager;
    //private AudioManager m_AudioManager;

    void Start()
    {
        //m_SpawnManager = FindObjectOfType<SpawnManager>();
        //m_UIManager = FindObjectOfType<UIManager>();
        //m_AudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Active)
            return;

        int nextWayPoint = m_CurrentWayPointIndex + 1;
        if (nextWayPoint > m_WayPoints.Length - 1)
            nextWayPoint = 0;

        transform.position = Vector3.MoveTowards(transform.position, m_WayPoints[nextWayPoint].position, m_CurrentMoveSpeed * Time.deltaTime);
        if (transform.position == m_WayPoints[nextWayPoint].position)
            m_CurrentWayPointIndex = nextWayPoint;

        Vector3 direction = m_WayPoints[nextWayPoint].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        if (m_TempCooldown <= 0)
        {
            Fire();
            m_TempCooldown = Random.Range(m_MinFireCooldown, m_MaxFireCooldown);
        }
        m_TempCooldown -= Time.deltaTime;
    }

    public void Init(Transform[] wayPoints,float speedMultiplier)
    {
        m_WayPoints = wayPoints;
        m_SpeedMultiplier = speedMultiplier;
        m_CurrentMoveSpeed = m_MoveSpeed * speedMultiplier;
        m_Active = true;
        transform.position = wayPoints[0].position;
        m_TempCooldown = Random.Range(m_MinFireCooldown, m_MaxFireCooldown) / speedMultiplier;
        m_CurrentHP = m_HP;
    }

    private void Fire()
    {
        //ProjectileController projectile = Instantiate(m_Projectile, m_FirePoint.position, Quaternion.identity, null);
        ProjectileController projectile = SpawnManager.Instance.SpawnEnemyProjectile(m_FirePoint.position);
        projectile.Fire(m_SpeedMultiplier);
        AudioManager.Instance.PlayPlasmaSFX();
    }

    public void Hit(int damage)
    {
        m_CurrentHP -= damage;
        if (m_CurrentHP <= 0)
        {
            //Destroy(gameObject);
            SpawnManager.Instance.ReleaseEnemyController(this);
            SpawnManager.Instance.SpawnExplosionFX(transform.position);
            //m_UIManager.AddScore(1);
            UIManager.Instance.AddScore(1);
            AudioManager.Instance.PlayExplosionSFX();
        }
        AudioManager.Instance.PlayHitSFX();
    }
}
