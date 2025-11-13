using System.Collections.Generic;
using UnityEngine;

namespace Game.Views.Cable
{
    [ExecuteAlways]
    [RequireComponent(typeof(LineRenderer))]
    public sealed class CableSimple : MonoBehaviour
    {
        private readonly List<Transform> _waypoints = new();

        [Header("Visual")]
        public float width = 0.02f;
        public Material lineMaterial;
        [Range(1, 64)] public int smoothSegmentsPerSection = 8;

        [Header("Shape")]
        [Tooltip("Intensidade da curvatura entre os pontos.")]
        public float curveStrength = 0.25f;
        [Tooltip("Inclina o cabo para baixo entre waypoints, simulando gravidade.")]
        public float sag = 0.1f;

        private LineRenderer _lineRenderer;
        private Vector3[] _smoothedPoints;
        private Vector3[] _rawPoints;

        private void OnValidate()
        {
            _lineRenderer ??= GetComponent<LineRenderer>();
            SetupLineRenderer();
        }

        private void Update()
        {
            if (Application.isPlaying)
                return;
            RefreshCable();
        }

        private void SetupLineRenderer()
        {
            _lineRenderer.useWorldSpace = true;
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;

            if (lineMaterial != null)
                _lineRenderer.sharedMaterial = lineMaterial;
        }

        private void RefreshCable()
        {
            PopulateWaypointList();

            if (_waypoints.Count < 2)
                return;

            _rawPoints = new Vector3[_waypoints.Count];

            for (int i = 0; i < _waypoints.Count; i++)
            {
                if (_waypoints[i] != null)
                    _rawPoints[i] = _waypoints[i].position;
            }

            List<Vector3> smooth = new();

            for (int i = 0; i < _rawPoints.Length - 1; i++)
            {
                Vector3 p0 = i > 0 ? _rawPoints[i - 1] : _rawPoints[i];
                Vector3 p1 = _rawPoints[i];
                Vector3 p2 = _rawPoints[i + 1];
                Vector3 p3 = (i < _rawPoints.Length - 2) ? _rawPoints[i + 2] : _rawPoints[i + 1];

                for (int s = 0; s < smoothSegmentsPerSection; s++)
                {
                    float t = s / (float)smoothSegmentsPerSection;
                    Vector3 point = CatmullRom(p0, p1, p2, p3, t);
                    point.y -= Mathf.Sin(t * Mathf.PI) * sag; // leve queda entre seções
                    smooth.Add(point);
                }
            }
            smooth.Add(_rawPoints[^1]); // último ponto

            _smoothedPoints = smooth.ToArray();
            _lineRenderer.positionCount = _smoothedPoints.Length;
            _lineRenderer.SetPositions(_smoothedPoints);
        }

        private void PopulateWaypointList()
        {
            _waypoints.Clear();
            _waypoints.Add(transform);

            foreach (Transform child in transform)
                _waypoints.Add(child);
        }

        private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            return 0.5f * (
                (2 * p1) +
                (-p0 + p2) * t +
                (2 * p0 - 5 * p1 + 4 * p2 - p3) * t2 +
                (-p0 + 3 * p1 - 3 * p2 + p3) * t3
            );
        }
    }
}