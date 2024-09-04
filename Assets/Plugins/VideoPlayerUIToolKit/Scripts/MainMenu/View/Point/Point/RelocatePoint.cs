using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RelocatePoint : Point
{
    [SerializeField] private RoomView _targetRoomView; 
    
    private Camera _mainCamera;
    private bool isVisible;

    void OnEnable()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        _mainCamera = Camera.main;
        
        AddRelocatePoint();
        InvokeRepeating(nameof(UpdatePosition), 0, 0.001f);
    }

    void UpdatePosition()
    {
        if (RoomManager.Instance.CurrentRoomView == _targetRoomView && isVisible)
            return;

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        if (screenPosition.z > 0)
        {
            screenPosition.y = Screen.height - screenPosition.y;
            PointElement.style.left = screenPosition.x - PointElement.resolvedStyle.width / 2;
            PointElement.style.top = screenPosition.y - PointElement.resolvedStyle.height / 2;
        }
    }
    
    private void AddRelocatePoint()
    {
        PointElement?.RemoveFromHierarchy();

        Button point = new Button();
        point.AddToClassList("hintPoint");
        PointElement = point;
        UIDocument.rootVisualElement.Add(PointElement);
        
        PointElement.clicked += OnClick;
    }
    
    public void OnClick()
    {
        RoomManager.Instance.SetCurrentRoom(_targetRoomView);
    }

    public override void SetActive(bool isActive)
    {
        PointElement.style.display = (isActive) ? DisplayStyle.Flex : DisplayStyle.None;
    }
}