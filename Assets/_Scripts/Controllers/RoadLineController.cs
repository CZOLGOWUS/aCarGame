using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame
{
    [RequireComponent(typeof(LineRenderer))]
    public class RoadLineController : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        private float originalLineLength;
        private float currentLineLength;

        private Vector3 lineDisplacment;
        private Vector3[] originalPositions = new Vector3[2];

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();

            originalPositions[0] = lineRenderer.GetPosition(0);
            originalPositions[1] = lineRenderer.GetPosition(1);

            lineDisplacment = originalPositions[0];

            originalLineLength = Mathf.Abs( lineRenderer.GetPosition(0).z - lineRenderer.GetPosition(1).z);
            currentLineLength = originalLineLength;
        }

        public void Advance(float speed)
        {
            lineDisplacment.z -= Time.deltaTime * speed;
            currentLineLength += Time.deltaTime * speed;

            if (currentLineLength >= originalLineLength * 2f)
            {
                lineRenderer.SetPosition(0, originalPositions[0]);
                lineDisplacment = originalPositions[0];
                currentLineLength = originalLineLength;
            }
            else
            {
                lineRenderer.SetPosition(0, lineDisplacment);
            }
        }
    }
}
