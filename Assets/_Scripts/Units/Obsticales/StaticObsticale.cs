using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Managers;


namespace CarGame
{
    public class StaticObsticale : Obsticale
    {

        private void Update()
        {
            if (playerManager == null)
                throw new System.Exception("player Manager was not set in " + this.ToString());

            transform.position -=  Vector3.forward * playerManager.playerCurrentState.speedMultiplier * playerManager.baseSpeed * Time.deltaTime;
        }

        public override void OnHit(PlayerManager player)
        {
            player.ChangeState(player.playerStandardHitState);
        }
    }
}
