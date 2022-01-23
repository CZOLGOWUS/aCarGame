using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Managers;

namespace CarGame
{
    public interface IHittable
    {
        public void OnHit(PlayerManager player);
    }
}
