using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Role : MonoBehaviour
{
    [SerializeField]
    private int _speedLevel = 1;

    public int speedLevel { get { return _speedLevel; } set { _speedLevel = value; } }

    [SerializeField]
    private FloatLevelMaper _moveSpeedMaper = null;

    [SerializeField]
    private FloatLevelMaper _animatorSpeedMaper = null;

    [SerializeField]
    private int _bombNumberLevel = 1;

    public int bombNumberLevel { get { return _bombNumberLevel; } set { _bombNumberLevel = value; } }

    [SerializeField]
    private IntLevelMaper _bombNumberMaper = null;

    [SerializeField]
    private int _bombPowerLevel = 1;

    public int bombPowerLevel { get { return _bombPowerLevel; } set { _bombPowerLevel = value; } }

    [SerializeField]
    private IntLevelMaper _bombPowerMaper = null;

    public float moveSpeed { get { return _moveSpeedMaper[_speedLevel]; } }
    public float animatorSpeed { get { return _animatorSpeedMaper[_speedLevel]; } }
    public int bombNumber { get { return _bombNumberMaper[_bombNumberLevel]; } }
    public int bombPower { get { return _bombPowerMaper[_bombPowerLevel]; } }

    public event Action OnSpawn = null;

    public event Action OnDeath = null;

    [SerializeField]
    private Animator _animator = null;

    private void Awake()
    {
        _moveSpeedMaper.Init();
        _animatorSpeedMaper.Init();
        _bombNumberMaper.Init();
        _bombPowerMaper.Init();
    }

    private void Start() { }

    private void Enable()
    {
        if (OnSpawn != null)
            OnSpawn();
    }

    public void TriggerOnDeath()
    {
        if (OnDeath != null)
            OnDeath();
        _animator.speed = 1f;
        _animator.SetBool("IsDead", true);
        Destroy(gameObject, 2f);
    }
}
