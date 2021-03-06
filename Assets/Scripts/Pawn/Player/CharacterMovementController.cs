﻿using UnityEngine;

public class CharacterMovementController : Function
{
    private Character _character = null;

    [SerializeField]
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private Animator _animator = null;

    protected override void Awake()
    {
        base.Awake();

        _character = GetComponent<Character>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnUpdate()
    {
        Vector3 moveDirection;
        if (CheckMove(out moveDirection))
        {
            _rigidbody.velocity = moveDirection*_character.moveSpeed;
            transform.forward = moveDirection;
        }

        _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, _rigidbody.velocity.magnitude/_character.moveSpeed);
        _animator.speed = _character.animatorSpeed;
    }

    public override void Off()
    {
        base.Off();

        _rigidbody.velocity = Vector3.zero;
        _animator.SetFloat(AnimatorConfig.FLOAT_SPEED, 0f);
    }

    private bool CheckMove(out Vector3 direction)
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(v) > 0.3f || Mathf.Abs(h) > 0.3f)
        {
            direction = new Vector3(h, 0f, v);
            return true;
        }
        direction = Vector3.zero;
        return false;
    }
}
