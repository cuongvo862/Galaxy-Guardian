using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<int, int> onHPChanged;

    [SerializeField] private float m_MoveSpeed;
    //[SerializeField] private ProjectileController m_Projectile;
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private float m_FireCooldown;
    [SerializeField] private int m_HP;
    [SerializeField] private bool m_UseNewInputSystem;

    private float m_TempCooldown;
    private int m_currentHP;
    private PlayerInput m_PlayerInput;
    private Vector2 m_MoveInputValue;
    private bool m_AttackInputValue;
    //private SpawnManager m_SpawnManager;
    //private UIManager m_UIManager;
    //private AudioManager m_AudioManager;

    private void OnEnable()
    {
        if (m_PlayerInput == null)
        {
            m_PlayerInput = new PlayerInput();
            m_PlayerInput.Player.Move.started += OnMove;
            m_PlayerInput.Player.Move.performed += OnMove;
            m_PlayerInput.Player.Move.canceled += OnMove;

            m_PlayerInput.Player.Attack.started += OnAttack;
            m_PlayerInput.Player.Attack.performed += OnAttack;
            m_PlayerInput.Player.Attack.canceled += OnAttack;
            m_PlayerInput.Enable();
        }
    }

    private void OnDisable()
    {
        m_PlayerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentHP = m_HP;
        if (onHPChanged != null)
            onHPChanged(m_currentHP, m_HP);
        //m_SpawnManager = FindObjectOfType<SpawnManager>();
        //m_UIManager = FindObjectOfType<UIManager>();
        //m_AudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsActive())
            return;

        Vector2 direction = Vector2.zero;
        if(!m_UseNewInputSystem)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            direction = new Vector2(horizontal, vertical);

            if (Input.GetKey(KeyCode.Space))
            {
                if (m_TempCooldown <= 0)
                {
                    Fire();
                    m_TempCooldown = m_FireCooldown;
                }
            }
        }
        else
        {
            direction = m_MoveInputValue;

            if (m_AttackInputValue)
            {
                if (m_TempCooldown <= 0)
                {
                    Fire();
                    m_TempCooldown = m_FireCooldown;
                }
            }
        }
        transform.Translate(direction * Time.deltaTime * m_MoveSpeed);

        m_TempCooldown -= Time.deltaTime;
    }

    private void Fire()
    {
        //ProjectileController projectile = Instantiate(m_Projectile, m_FirePoint.position, Quaternion.identity, null);
        ProjectileController projectile = SpawnManager.Instance.SpawnPlayerProjectile(m_FirePoint.position);
        projectile.Fire(1);

        SpawnManager.Instance.SpawnShootingFX(m_FirePoint.position);
        AudioManager.Instance.PlayLazerSFX();
    }

    public void Hit(int damage)
    {
        m_currentHP -= damage;
        if (onHPChanged != null)
            onHPChanged(m_currentHP, m_HP);
        if (m_currentHP <= 0)
        {
            Destroy(gameObject);
            SpawnManager.Instance.SpawnExplosionFX(transform.position);
            UIManager.Instance.GameOver(false);
            AudioManager.Instance.PlayExplosionSFX();
        }
        AudioManager.Instance.PlayHitSFX();
    }

    private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            m_AttackInputValue = true;
        }
        else if (obj.performed)
        {
            m_AttackInputValue = true;
        }
        else if (obj.canceled)
        {
            m_AttackInputValue = false;
        }
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            m_MoveInputValue = obj.ReadValue<Vector2>();
        }
        else if (obj.performed)
        {
            m_MoveInputValue = obj.ReadValue<Vector2>();

        }
        else if (obj.canceled)
        {
            m_MoveInputValue = Vector2.zero;
        }
    }
}
