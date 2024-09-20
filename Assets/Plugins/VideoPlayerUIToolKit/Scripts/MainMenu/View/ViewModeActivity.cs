using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModeActivity : MonoBehaviour
{
    public bool IsViewModeActive => _roomManager.gameObject.activeSelf;
    [SerializeField] private RoomManager _roomManager;

    public void SetActive(bool isActive)
    {
        _roomManager.gameObject.SetActive(isActive);
    }
}
