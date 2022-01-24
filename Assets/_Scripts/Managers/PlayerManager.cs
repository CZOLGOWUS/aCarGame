using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using CarGame.Units;
using CarGame.Controllers;
using CarGame.Helpers;
using System;

namespace CarGame.Managers
{
    [RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
    public class PlayerManager : Singleton<PlayerManager>
    {

        public event Action OnPlayerDeath;
        public event Action<int> OnPlayerHit;

        private Rigidbody _rb;
        private BoxCollider _col;

        private MovementController _movementController;

        public PlayerState playerCurrentState { get; private set; }
        public PlayerBaseState playerBaseState { get; } = new PlayerBaseState();
        public PlayerStandardHitState playerStandardHitState { get; } = new PlayerStandardHitState();

        private SquareArea _gameArea;
        private Vector3 _targetVelocity = new Vector3();
        private int _currentHP;


        [SerializeField] private int _maxHP = 10;
        [SerializeField] private int _startingHP = 3;
        [SerializeField] private float _baseSpeed = 10f;
        [Range(0f,1f)]
        [SerializeField] private float _smothingTime = 0.01f;
        [SerializeField] private LayerMask terrainLayer;
        [SerializeField] private float XAreaBoundsThickness;
        [SerializeField] private float ZAreaBoundsThickness;

        public float zAxis { get; private set; }
        public float xAxis { get; private set; }
        public bool isInteractionPressed { get; private set; }
        public Vector3 targetVelocity { get => _targetVelocity; set { _targetVelocity = value; } }

        public Rigidbody rigidBody { get => _rb; private set { } }
        public Collider boxCollider { get => _col; private set { } }

        public float smothingTime { get => _smothingTime; set => _smothingTime = value; }
        public float baseSpeed { get { return _baseSpeed; } private set { _baseSpeed = value; } }
        public int maxHP { get => _maxHP; set => _maxHP = value;  }
        public int currentHP { get => _currentHP; set => _currentHP = value; }
        public int startingHP { get => _startingHP; set => _startingHP = value; }

        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<BoxCollider>();

            _movementController = new MovementController(_rb, _col);
        }

        // Start is called before the first frame update
        void Start()
        {
            _gameArea = Helper.GetCameraViewportArea(Camera.main, terrainLayer);

            //currentHP = 3;

            playerCurrentState = playerBaseState;
            playerCurrentState.EnterState(this);
        }

        // Update is called once per frame
        void Update()
        {
            playerCurrentState.Update(this);

            HandlePlayAreaBounds();

            _movementController.Move(targetVelocity);
        }


        public void ModifyHP(int hpModification)
        {
            if (currentHP + hpModification > maxHP)
            {
                return;
            }
            else if (currentHP + hpModification <= 0)
            {
                OnPlayerDeath();
            }

            if(hpModification > 0)
            {
                currentHP += hpModification;
                OnPlayerHit(hpModification);
            }
            else if(hpModification < 0)
            {
                currentHP += hpModification;
                OnPlayerHit(hpModification);
            }
            
        }
    


        private void HandlePlayAreaBounds()
        {
            int outOfBoundsX = Helper.IsOutOfBoundsX(_gameArea, this.transform, _col, XAreaBoundsThickness);
            int outOfBoundsZ = Helper.IsOutOfBoundsZ(_gameArea, this.transform, _col, ZAreaBoundsThickness);

            if (outOfBoundsX != 0)
            {
                _targetVelocity.x = Mathf.Sign(targetVelocity.x) == outOfBoundsX ? 0 : targetVelocity.x;
                transform.position = new Vector3(
                    (outOfBoundsX < 0 ? _gameArea.topLeft.x : _gameArea.topRight.x) - outOfBoundsX * _col.bounds.extents.x, 
                    transform.position.y,
                    transform.position.z);
            }

            if (outOfBoundsZ != 0)
            {
                _targetVelocity.z = Mathf.Sign(targetVelocity.z) == outOfBoundsZ ? 0 : targetVelocity.z;
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    (outOfBoundsZ < 0 ? _gameArea.bottomLeft.z : _gameArea.topLeft.z) - outOfBoundsZ * _col.bounds.extents.z* 1.01f
                    );
            }
        }


        public void ChangeState(PlayerState newState)
        {
            playerCurrentState = newState;
            playerCurrentState.EnterState(this);
        }



        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<IHittable>(out IHittable result);

            if (result == null)
                return;

            result.OnHit(this);
        }

        private void OnTriggerExit(Collider other)
        {
            
        }

        #region Inputs
        public void OnZAxisPress(InputAction.CallbackContext ctx )
        {
            if (ctx.performed)
                zAxis = ctx.ReadValue<float>();
        }

        public void OnXAxisPress(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                xAxis = ctx.ReadValue<float>();
        }

        public void OnInteractionPress(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                isInteractionPressed = ctx.ReadValue<bool>();

                playerCurrentState.Interact(this);
            }
        }
        #endregion

    }
}
