using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class VideoVisualControl : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private InactionAction _inactionAction;
    [SerializeField] private float _inactivityThreshold; 
    [SerializeField] private float _inactionTime;
    [SerializeField] private string _activeStyle;
    [SerializeField] private string _nonActiveStyle;
    private float _inactivityTimer = 0.0f;
    private bool _isMouseMoving = false;

    private bool _mouseEnter;
    private VisualElement _buttonControls;
    private Vector3 _lastMousePosition;

    private void Start()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        root.RegisterCallback<GeometryChangedEvent>(_ => Initialize());
    }
    
    private void Update()
    {
        if (_buttonControls == null) return;

        var mousePosition = Input.mousePosition;

        if (mousePosition != _lastMousePosition)
        {
            _isMouseMoving = true;
            _inactivityTimer = 0.0f;
        }
        else
        {
            _isMouseMoving = false;
            _inactivityTimer += Time.deltaTime;
        }

        if (_inactivityTimer >= _inactionTime)
        {
            _inactivityTimer = 0.0f;
            _inactionAction.OnInaction();
        }

        if (_isMouseMoving && _buttonControls.ClassListContains(_nonActiveStyle))
            OnMouseMove();
        else if (_inactivityTimer >= _inactivityThreshold && _buttonControls.ClassListContains(_activeStyle) && !_mouseEnter)
            OnMouseIdle();
            
        _lastMousePosition = mousePosition;
    }

    // Метод вызывается при движении мыши
    private void OnMouseMove()
    {
        Debug.Log("Mouse is moving." + (_buttonControls.style.display == DisplayStyle.None));
        _buttonControls.RemoveFromClassList(_nonActiveStyle);
        _buttonControls.AddToClassList(_activeStyle);
        // Здесь можно добавить логику для обработки движения мыши
    }

    // Метод вызывается при бездействии мыши
    private void OnMouseIdle()
    {
        Debug.Log("Mouse is idle."+ (_buttonControls.style.display == DisplayStyle.Flex));
        _buttonControls.RemoveFromClassList(_activeStyle);
        _buttonControls.AddToClassList(_nonActiveStyle);
    }
    
    private void Initialize()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        VisualElement buttonControls = root.Q<VisualElement>("Button_Controls");
        _buttonControls = buttonControls;
        
        _buttonControls.RegisterCallback<MouseLeaveEvent>(evt => _mouseEnter = false);
        _buttonControls.RegisterCallback<MouseEnterEvent>(evt => _mouseEnter = true);
    }
}
