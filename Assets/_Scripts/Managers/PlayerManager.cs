using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using CarGame.Units;

namespace CarGame.Managers
{
    public class PlayerManager : MonoBehaviour
    {

        private Rigidbody _rb;
        private BoxCollider _col;

        private PlayerState _playerCurrentState;
        private PlayerBaseState _playerBaseState = new PlayerBaseState();


        [SerializeField] private float _maxSpeed = 10f;
        [Range(0f,1f)]
        [SerializeField] private float _smothingTime = 0.01f;


        public float ZAxis { get; private set; }
        public float XAxis { get; private set; }
        public bool isInteractionPressed { get; private set; }
        public Vector3 targetVelocity { get; set; } = new Vector3();

        public float maxSpeed { get { return _maxSpeed; } private set { } }
        public Rigidbody rigidBody { get => _rb; private set { } }
        public Collider boxCollider { get => _col; private set { } }

        public float SmothingTime { get => _smothingTime; set => _smothingTime = value; }

        struct CameraRays
        {
            public Ray topRight;
            public Ray topLeft;
            public Ray bottomLeft;
            public Ray bottomRight;
        }

        struct CameraRayHits
        {
            public RaycastHit topRight;
            public RaycastHit topLeft;
            public RaycastHit bottomLeft;
            public RaycastHit bottomRight;
        }

        struct CameraBoundriesToWorld
        {
            public Vector3 topRight;
            public Vector3 topLeft;
            public Vector3 bottomLeft;
            public Vector3 bottomRight;
        }

        CameraBoundriesToWorld _gameArea = new CameraBoundriesToWorld();
        CameraRays _cameraRays = new CameraRays();
        CameraRayHits cameraRayHits = new CameraRayHits();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<BoxCollider>();
        }

        // Start is called before the first frame update
        void Start()
        {

            _cameraRays.topLeft = Camera.main.ViewportPointToRay(new Vector3(0, 1, 0));
            _cameraRays.topRight = Camera.main.ViewportPointToRay(new Vector3(1, 1, 0));
            _cameraRays.bottomLeft = Camera.main.ViewportPointToRay(new Vector3(0, 0 , 0));
            _cameraRays.bottomRight = Camera.main.ViewportPointToRay(new Vector3(1, 0, 0));

            Physics.Raycast(_cameraRays.topLeft, out cameraRayHits.topLeft);
            Physics.Raycast(_cameraRays.topRight, out cameraRayHits.topRight);
            Physics.Raycast(_cameraRays.bottomLeft, out cameraRayHits.bottomLeft);
            Physics.Raycast(_cameraRays.bottomRight, out cameraRayHits.bottomRight);


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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(cameraRayHits.topLeft.point, 0.5f);
        }

    }
}
