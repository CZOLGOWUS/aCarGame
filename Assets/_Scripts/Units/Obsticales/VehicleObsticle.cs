using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Managers;

namespace CarGame
{
    public class VehicleObsticle : Obsticale
    {

        private float speedModifier;

        private void OnEnable()
        {
            speedModifier = (Random.value + 0.5f);
        }

        private void FixedUpdate()
        {
            if (playerManager == null)
                throw new System.Exception("player Manager was not set in " + this.ToString());

            transform.position -= speedModifier * Vector3.forward * playerManager.playerCurrentState.speedMultiplier * playerManager.baseSpeed * Time.deltaTime;
            
        }

        public override void OnHit(PlayerManager player)
        {
            Debug.Log("player hit");
            player.ChangeState(player.playerStandardHitState);
        }
    }
}
