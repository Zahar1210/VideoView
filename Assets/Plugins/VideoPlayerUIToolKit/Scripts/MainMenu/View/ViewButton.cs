using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewButton : MonoBehaviour
{
    [SerializeField] private RoomManager _roomManager;
    [SerializeField] private ViewModeActivity _viewModeActivity;
    [SerializeField] private VideoModeActivity _videoModeActivity;
    
    public void Initialize(VisualElement root)
    {
        Button viewButton = root.Q<Button>("View");
        if (viewButton != null)
        {
            viewButton.clicked += OnViewButtonClick;
        }
    }
    
    private void OnViewButtonClick()
    {
        _videoModeActivity.SetActive(false);
        _viewModeActivity.SetActive(true);
        _roomManager.Initialize();
        Debug.LogError("нажали на кнопку для 360 просмотра");
    }
}
