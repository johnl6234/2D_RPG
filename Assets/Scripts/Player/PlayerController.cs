using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _moveSpeed = 2.0f;

        private PlayerInputActions _inputs;
        private InputAction _move;
        private InputAction _fire;

        private Vector2 _movementValue;
        private bool _attacking = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _inputs = new PlayerInputActions();

            _move = _inputs.Player.Move;
            _fire = _inputs.Player.Fire;

            _move.performed += Move;
            _move.canceled += Move;
            _fire.performed += Fire;
            //_fire.canceled += Fire;
        }
        private void OnEnable()
        {
            _move.Enable();
            _fire.Enable();
        }
        private void OnDisable()
        {
            _move.Disable();
            _fire.Disable();
        }

        private void FixedUpdate()
        {
            if (_movementValue != Vector2.zero && !_attacking)
                _rb.MovePosition(_rb.position + _movementValue * _moveSpeed * Time.fixedDeltaTime);

            _animator.SetFloat("Horizontal", _movementValue.x);
            _animator.SetFloat("Vertical", _movementValue.y);
        }
        private void Move(InputAction.CallbackContext value)
        {
            _movementValue = value.ReadValue<Vector2>();
        }

        private void Fire(InputAction.CallbackContext value)
        {
            _attacking = true;
            _animator.SetTrigger("Attack");
        }

        private void OnAnimEnd()
        {
            _attacking = false;
        }
    }

}
