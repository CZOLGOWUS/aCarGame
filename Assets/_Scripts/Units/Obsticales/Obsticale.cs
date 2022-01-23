using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Managers;

namespace CarGame
{
    public abstract class Obsticale : MonoBehaviour, IHittable
    {
        public PlayerManager playerManager { get; set; }

        public abstract void OnHit(PlayerManager player);
    }
}
