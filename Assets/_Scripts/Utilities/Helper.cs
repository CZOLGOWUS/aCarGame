using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Helpers
{

    public class Helper
    {
        public static ViewportArea GetCameraViewportArea(Camera camera, LayerMask layer)
        {
            CameraRays _cameraRays = new CameraRays();
            CameraRayHits _cameraRayHits = new CameraRayHits();
            ViewportArea _viewportArea = new ViewportArea();

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

    public struct ViewportArea
    {
        public Vector3 topRight;
        public Vector3 topLeft;
        public Vector3 bottomLeft;
        public Vector3 bottomRight;
    }
}
