using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Managers;

namespace CarGame
{
    [RequireComponent(typeof(BoxCollider))]
    public class OffRoadController :  MonoBehaviour,IHittable
    {
        private PlayerManager playerManager;
        public int hpBeforeModification { get; set; }

        public int hpModifier { get; } = 1;
        public float speedModifier { get; } = 0.8f;


        public void OnHit(PlayerManager player)
        {
            //player.ChangeState(player.playerStandardHitState);
        }

        public void SetPlayerManager(PlayerManager player)
        {
            playerManager = player;
        }
    }
}
