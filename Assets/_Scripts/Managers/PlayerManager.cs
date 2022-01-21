using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using CarGame.Units;
using CarGame.Helpers;

namespace CarGame.Managers
{
    public class PlayerManager : MonoBehaviour
    {

        private Rigidbody _rb;
        private BoxCollider _col;

        private PlayerState _playerCurrentState;
        private PlayerBaseState _playerBaseState = new PlayerBaseState();

        private ViewportArea _gameArea;

        [SerializeField] private float _maxSpeed = 10f;
        [Range(0f,1f)]
        [SerializeField] private float _smothingTime = 0.01f;
        [SerializeField] private LayerMask terrainLayer;

        public float ZAxis { get; private set; }
        public float XAxis { get; private set; }
        public bool isInteractionPressed { get; private set; }
        public Vector3 targetVelocity { get; set; } = new Vector3();

        public float maxSpeed { get { return _maxSpeed; } private set { } }
        public Rigidbody rigidBody { get => _rb; private set { } }
        public Collider boxCollider { get => _col; private set { } }

        public float SmothingTime { get => _smothingTime; set => _smothingTime = value; }



        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<BoxCollider>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _gameArea = Helper.GetCameraViewportArea(Camera.main, terrainLayer);

            _playerCurrentState = _playerBaseState;
            _playerCurrentState.EnterState(this);
        }

        // Update is called once per frame
        void Update()
        {
            _playerCurrentState.Update(this);



            _rb.velocity = targetVelocity;
        }

        public void ChangeState(PlayerState newState)
        {
            _playerCurrentState = newState;
        }

        public void OnZAxisPress(InputAction.CallbackContext ctx )
        {
            if (ctx.performed)
                ZAxis = ctx.ReadValue<float>();
        }

        public void OnXAxisPress(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                XAxis = ctx.ReadValue<float>();
        }

        public void OnInteractionPress(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                isInteractionPressed = ctx.ReadValue<bool>();

                _playerCurrentState.Interact(this);
            }
        }

    }
}
