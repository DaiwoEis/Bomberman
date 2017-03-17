using UnityEngine;

public class Enemy : Pawn
{
    [SerializeField]
    private Animator _animator = null;

	private void Awake() { }

    private void Start() { }

    private void OnEnable()
    {
        TriggerOnSpawnEvent();
    }

    private void Update() { }

    public override void Death()
    {
        base.Death();

        TriggerOnDeathEvent();

        if (_animator != null)
        {
            _animator.speed = 1f;
            _animator.SetBool("IsDead", true);
        }

        Destroy(gameObject, 2f);
    }
}
