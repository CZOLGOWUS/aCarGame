using CarGame.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Units
{
    public class PlayerBaseState : PlayerState
    {
        // Start is called before the first frame update

        private Rigidbody rb;
        private Collider collider;
        private Vector3 vel;
        private float speedMultiplayer = 1f;
        private Vector3 targetVel = new Vector3();


        public override void EnterState(PlayerManager manager)
        {
            rb = manager.rigidBody;
            collider = manager.boxCollider;
        }

        public override void Interact(PlayerManager manager)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(PlayerManager manager)
        {
            targetVel.x = manager.maxSpeed * speedMultiplayer * manager.XAxis;
            targetVel.y = rb.velocity.y;
            targetVel.z = manager.maxSpeed * speedMultiplayer * manager.ZAxis;
           

            manager.targetVelocity = Vector3.SmoothDamp(
                rb.velocity,
                targetVel,
                ref vel, 
                manager.SmothingTime
                );
        }
    }
}
