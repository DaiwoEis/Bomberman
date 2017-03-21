using UnityEngine;

public class Enemy : Pawn
{
    [SerializeField]
    private Animator _animator = null;

	private void Awake() { }

    private void Start()
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
            _animator.SetBool(AnimatorConfig.BOOL_IS_DEAD, true);
        }

        Destroy(gameObject, 2f);
    }

    private void OnDestroy()
    {
        TriggerOnDestroyEvent();
    }
}
