using System;
using System.Collections;
using System.Collections.Generic;
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

        private Vector2 _movementValue;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _inputs = new PlayerInputActions();

            _move = _inputs.Player.Move;

            _move.performed += Move;
            _move.canceled += Move;
        }
        private void OnEnable()
        {
            _move.Enable();
        }
        private void OnDisable()
        {
            _move.Disable();
        }

        private void FixedUpdate()
        {
            if (_movementValue != Vector2.zero)
                _rb.MovePosition(_rb.position + _movementValue * _moveSpeed * Time.fixedDeltaTime);

            _animator.SetFloat("Horizontal", _movementValue.x);
            _animator.SetFloat("Vertical", _movementValue.y);
        }
        private void Move(InputAction.CallbackContext value)
        {
            _movementValue = value.ReadValue<Vector2>();
        }
    }

}
