﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomMove : MonoBehaviour
{
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private Map _map = null;

    [SerializeField]
    private LayerMask _cantMoveLayer = default(LayerMask);

    [SerializeField]
    private Animator _animator = null;

    private float _moveSpeed = 3f;

    private Vector3 _destPos = Vector3.zero;

    private Vector3 _moveDirection = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
       MoveRight();
    }

    private void MoveRight()
    {
        _moveDirection = Vector3.right;

        for (int x = (int)transform.position.x; x < _map.floor.GetLength(1); ++x)
        {
            if (_map.floor[(int)transform.position.z, x])
            {
                _destPos = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
    }

    private void MoveLeft()
    {
        _moveDirection = Vector3.left;

        for (int x = (int) transform.position.x; x >= 0; --x)
        {
            if (_map.floor[(int) transform.position.z, x])
            {
                _destPos = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
    }

    private bool MoveComplete()
    {
        return Vector3.Distance(transform.position, _destPos) < 0.1f;
    }

    private void Update()
    {
        if (MoveComplete())
        {
            if (transform.forward == Vector3.left)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        _rigidbody.velocity = _moveDirection*_moveSpeed;
        _animator.SetFloat("Speed", 1f);
    }

    private bool CheckBlock(Vector3 direction)
    {        
        return Physics.Raycast(transform.position + Vector3.up*0.5f, direction, 1f, _cantMoveLayer);
    }
}