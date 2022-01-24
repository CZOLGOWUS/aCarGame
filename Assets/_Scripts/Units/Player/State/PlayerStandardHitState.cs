using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Managers;


namespace CarGame.Units
{
    public class PlayerStandardHitState : PlayerState
    {
        private Rigidbody rb;
        private Vector3 vel;
        private Vector3 targetVel = new Vector3();

        public override float speedMultiplier { get; set; } = 0.1f;
        public float currentSpeedMultiplier { get; private set; } = 0.1f;
        public float smoothingTime { get; private set; } = 0.1f;
        public float effectTime { get; private set; } = 1f;


        public override void EnterState(PlayerManager player)
        {
            player.StartCoroutine(HitTimer(player));

            rb = player.rigidBody;

            rb.AddForce(-Vector3.forward * 15f,ForceMode.Impulse);

            player.ModifyHP(-1);

        }

        public override void Interact(PlayerManager player)
        {
            return;
        }

        public override void Update(PlayerManager player)
        {

            targetVel.x = player.baseSpeed * currentSpeedMultiplier * player.xAxis;
            targetVel.y = rb.velocity.y;
            targetVel.z = player.baseSpeed * currentSpeedMultiplier * player.zAxis;


            player.targetVelocity = Vector3.SmoothDamp(
                rb.velocity,
                targetVel,
                ref vel,
                smoothingTime
                );

        }

        public IEnumerator HitTimer(PlayerManager player)
        {
            float effectTimeCount = 0f;

            player.boxCollider.enabled = false;

            while(effectTimeCount < effectTime)
            {
                yield return new WaitForFixedUpdate();

                currentSpeedMultiplier = Mathf.Lerp(currentSpeedMultiplier, 1f, 0.01f);
                effectTimeCount += Time.deltaTime;

            }
            player.boxCollider.enabled = true;

            currentSpeedMultiplier = speedMultiplier;

            player.ChangeState(player.playerBaseState);
        }
    }
}
