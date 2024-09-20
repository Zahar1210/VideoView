using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; set; }
    public VisualElement Blackout => _root.Q<VisualElement>("BlackoutEffect");
    public RoomView CurrentRoomView { get; set; }
    
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private RoomView _startRoomView;
    [SerializeField] private RoomView[] _roomViews;
    [SerializeField] private CloseViewMode _closeButton;

    private VisualElement _root;

    private void Awake()
    {
        _root = _uiDocument.rootVisualElement;
    }

    private void OnEnable()
    {
        Instance = this;
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
        StartCoroutine(BlackoutEffect(roomView));
    }

    private IEnumerator BlackoutEffect(RoomView roomView)
    {
        Blackout.RemoveFromClassList("returnBlackout");
        Blackout.AddToClassList("turnBlackout");
        yield return new WaitForSeconds(0.5f);
        CurrentRoomView?.gameObject.SetActive(false);
        CurrentRoomView?.SetActiveRoomView(false);
        
        CurrentRoomView = roomView;
        
        CurrentRoomView?.gameObject.SetActive(true);
        CurrentRoomView?.SetActiveRoomView(true);
        Blackout.RemoveFromClassList("turnBlackout");
        Blackout.AddToClassList("returnBlackout");
    }
}