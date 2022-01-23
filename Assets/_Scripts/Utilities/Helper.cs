using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Helpers
{

    public static class Helper
    {
        public static SquareArea GetCameraViewportArea(Camera camera, LayerMask layer)
        {
            CameraRays _cameraRays = new CameraRays();
            CameraRayHits _cameraRayHits = new CameraRayHits();
            SquareArea _viewportArea = new SquareArea();

            _cameraRays.topLeft = camera.ViewportPointToRay(new Vector3(0, 1, 0));
            _cameraRays.topRight = camera.ViewportPointToRay(new Vector3(1, 1, 0));
            _cameraRays.bottomLeft = camera.ViewportPointToRay(new Vector3(0, 0, 0));
            _cameraRays.bottomRight = camera.ViewportPointToRay(new Vector3(1, 0, 0));

            Physics.Raycast(_cameraRays.topLeft, out _cameraRayHits.topLeft);
            Physics.Raycast(_cameraRays.topRight, out _cameraRayHits.topRight);
            Physics.Raycast(_cameraRays.bottomLeft, out _cameraRayHits.bottomLeft);
            Physics.Raycast(_cameraRays.bottomRight, out _cameraRayHits.bottomRight);

            _viewportArea.topLeft = _cameraRayHits.topLeft.point;
            _viewportArea.topRight = _cameraRayHits.topRight.point;
            _viewportArea.bottomLeft = _cameraRayHits.bottomLeft.point;
            _viewportArea.bottomRight = _cameraRayHits.bottomRight.point;

            return _viewportArea;

        }


        /// <summary>
        /// 
        /// 0 : is in bounds, 
        /// -1 : out of bounds to the left, 
        /// 1 : out of bounds to the right
        /// 
        /// </summary>
        /// <param name="area">the SquareArea struct of the given area</param>
        /// <param name="t"> transform of the object that is mean to be checked</param>
        /// <returns></returns>
        public static int IsOutOfBoundsX(SquareArea area, Transform t, BoxCollider c , float boundsThicknes)
        {
            Vector3 position = t.position;
            Vector3 bounds = c.bounds.extents;

            if (position.x - bounds.x <= area.topLeft.x || position.x - bounds.x <= area.bottomLeft.x)
            {
                return -1;
            }

            if (position.x + bounds.x >= area.bottomRight.x || position.x + bounds.x >= area.topRight.x)
            {
                return 1;
            }

            return 0;
        }


        /// <summary>
        /// 
        /// 0 : is in bounds, 
        /// -1 : out of bounds to the bottom, 
        /// 1 : out of bounds to the top
        /// 
        /// </summary>
        /// <param name="area">the SquareArea struct of the given area</param>
        /// <param name="t"> transform of the object that is mean to be checked</param>
        /// <returns></returns>
        public static int IsOutOfBoundsZ(SquareArea area, Transform t, BoxCollider c, float boundsThicknes)
        {
            Vector3 position = t.position;
            Vector3 bounds = c.bounds.extents;

            if (position.z + bounds.z <= area.bottomRight.z || position.z - bounds.z <= area.bottomLeft.z)
            {
                return -1;
            }

            if (position.z + bounds.z >= area.topRight.z || position.z + bounds.z >= area.topLeft.z)
            {
                return 1;
            }

            return 0;
        }

    }

    public struct CameraRays
    {
        public Ray topRight;
        public Ray topLeft;
        public Ray bottomLeft;
        public Ray bottomRight;
    }

    public struct CameraRayHits
    {
        public RaycastHit topRight;
        public RaycastHit topLeft;
        public RaycastHit bottomLeft;
        public RaycastHit bottomRight;
    }

    public struct SquareArea
    {
        public Vector3 topRight;
        public Vector3 topLeft;
        public Vector3 bottomLeft;
        public Vector3 bottomRight;
    }

}
