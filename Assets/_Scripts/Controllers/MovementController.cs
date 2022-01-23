using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Controllers
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class MovementController
    {

        private Rigidbody rb;
        private Collider colldier;

        public MovementController(Rigidbody rb, Collider col)
        {
            this.rb = rb;
            this.colldier = col;
        }

        public void Move(Vector3 velocity)
        {
            rb.velocity = velocity;
        }


    }
}
