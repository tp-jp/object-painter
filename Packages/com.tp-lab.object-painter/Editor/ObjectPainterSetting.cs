using System;
using UnityEngine;

namespace TpLab.ObjectPainter.Editor
{
    public class ObjectPainterSetting : ScriptableObject
    {
        public MeshFilter targetMesh;
        public GameObject placeObject;
        public Transform parentObject;
        public float brushSize = 1f;
        public float brushSizeLimit = 10f;
        public int density = 10;
        public int densityLimit = 50;
        public bool showPlacementSettings = true;
        public bool showLimitSettings;

        public RandomizableRange rotationX = new RandomizableRange(true, 0f, 360f, -360f, 360f);
        public RandomizableRange rotationY = new RandomizableRange(true, 0f, 360f, -360f, 360f);
        public RandomizableRange rotationZ = new RandomizableRange(true, 0f, 360f, -360f, 360f);
        public RandomizableRange scale = new RandomizableRange(true, 0.8f, 1.2f, 0.1f, 10f);
    }

    [Serializable]
    public struct RandomizableRange
    {
        public bool isEnabled;
        public float minValue;
        public float maxValue;
        public float minLimit;
        public float maxLimit;
        
        public RandomizableRange(bool isEnabled, float minValue, float maxValue, float minLimit, float maxLimit)
        {
            this.isEnabled = isEnabled;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }
        
        public float RandomValue => UnityEngine.Random.Range(minValue, maxValue);
    }
}
