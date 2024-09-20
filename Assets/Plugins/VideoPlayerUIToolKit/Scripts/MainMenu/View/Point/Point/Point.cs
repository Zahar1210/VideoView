using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Point : MonoBehaviour
{
    [SerializeField] private ElementPoint _elementPoints;
    [SerializeField] private UIDocument _uiDocument;
    public UIDocument UIDocument => _uiDocument;
    public ElementPoint PointElement => _elementPoints;

    public abstract void SetActive(bool isActive);
    public abstract void Animation(AnimationParams animationParams);

    [System.Serializable]
    public class ElementPoint
    {
        public Button MainPoint { get; set; }
        public AnimationParams[] AnimationParamsArray => _animationParamsArray;
        [SerializeField] private AnimationParams[] _animationParamsArray;
    }

    [System.Serializable]
    public class AnimationParams
    {
        public float StartIndex;
        public float TargetIndex;
        [Space]
        public float MinThickness;
        public float MaxThickness;
        [Space]
        public float MinOpacity;
        public float MaxOpacity;
        [Space]
        public float Speed; 
        public VisualElement Point { get; set; }
        public float AnimationIndex { get; set; }
    }
}