using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplineBuilder : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private bool buildPath;
    [SerializeField] private BoardBox[] boxes;

    private void OnValidate()
    {
        if (buildPath)
        {
            spline.Spline.Clear();

            boxes = transform.GetComponentsInChildren<BoardBox>();

            float3[] splinePositions = new float3[boxes.Length];
            for (int i = 0; i < boxes.Length; i++)
            {
                splinePositions[i] = boxes[i].LandingPoint.position/2;
            }

            spline.Spline.AddRange(splinePositions, TangentMode.AutoSmooth);

            buildPath = false;
        }
    }
}
