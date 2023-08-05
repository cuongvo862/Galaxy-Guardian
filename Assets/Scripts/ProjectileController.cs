using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Vector2 m_Direction;
    [SerializeField] private int m_Damage;

    private bool m_FromPlayer;
    //private SpawnManager m_SpawnManager;
    private float m_LifeTime;
    private float m_CurrentMoveSpeed;
    
    void Start()
    {
        //m_SpawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Direction * Time.deltaTime * m_CurrentMoveSpeed);

        m_LifeTime -= Time.deltaTime;
        if (m_LifeTime <= 0)
        {
            if (m_FromPlayer)
                SpawnManager.Instance.ReleasePlayerProjectile(this);
            else
                SpawnManager.Instance.ReleaseEnemyProjectile(this);
        }
    }
    public void Fire(float speedMultiplier)
    {
        //Destroy(gameObject, 10f);
        m_LifeTime = 10f / speedMultiplier;
        m_CurrentMoveSpeed = m_MoveSpeed * speedMultiplier;
    }
    public void SetFromPlayer(bool fromPlayer)
    {
        m_FromPlayer = fromPlayer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger: "+ collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(gameObject);
            if (m_FromPlayer)
                SpawnManager.Instance.ReleasePlayerProjectile(this);
            else
                SpawnManager.Instance.ReleaseEnemyProjectile(this);

            Vector3 hitPos = collision.ClosestPoint(transform.position);
            SpawnManager.Instance.SpawnHitFX(hitPos);

            EnemyController enemy;
            collision.gameObject.TryGetComponent(out enemy);
            enemy.Hit(m_Damage);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
            if (m_FromPlayer)
                SpawnManager.Instance.ReleasePlayerProjectile(this);
            else
                SpawnManager.Instance.ReleaseEnemyProjectile(this);

            Vector3 hitPos = collision.ClosestPoint(transform.position);
            SpawnManager.Instance.SpawnHitFX(hitPos);

            PlayerController player;
            collision.gameObject.TryGetComponent(out player);
            player.Hit(m_Damage);
        }
    }
}
