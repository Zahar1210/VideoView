using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private RoomView _startRoomView;
    [SerializeField] private RoomView[] _roomViews;
    [SerializeField] private CloseViewMode _closeButton;
    public static RoomManager Instance { get; set; }
    public RoomView CurrentRoomView { get; set; }
    
    private void OnEnable()
    {
        Instance = this;
        // Initialize();
    }

    public void Initialize()
    {
        foreach (var roomView in _roomViews)
        {                
            if (_startRoomView == roomView)
            { 
                CurrentRoomView = roomView;
                CurrentRoomView.SetActiveRoomView(true);
            }
            else
            {
                roomView.SetActiveRoomView(false);
            }
        }
        _closeButton.Initialize(gameObject.GetComponent<UIDocument>().rootVisualElement);
    }
    
    public void SetCurrentRoom(RoomView roomView)
    {
        CurrentRoomView?.gameObject.SetActive(false);
        CurrentRoomView?.SetActiveRoomView(false);
        
        CurrentRoomView = roomView;
        
        CurrentRoomView?.gameObject.SetActive(true);
        CurrentRoomView?.SetActiveRoomView(true);
    }
}