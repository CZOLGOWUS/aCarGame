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
        private Vector3 vel;
        private Vector3 targetVel = new Vector3();
        public override float speedMultiplier { get; set; } = 1f;

        public override void EnterState(PlayerManager player)
        {
            rb = player.rigidBody;
        }

        public override void Interact(PlayerManager player)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(PlayerManager player)
        {

            targetVel.x = player.baseSpeed * speedMultiplier * player.xAxis;
            targetVel.y = rb.velocity.y;
            targetVel.z = player.baseSpeed * speedMultiplier * player.zAxis;
           

            player.targetVelocity = Vector3.SmoothDamp(
                rb.velocity,
                targetVel,
                ref vel, 
                player.smothingTime
                );
        }
    }
}
