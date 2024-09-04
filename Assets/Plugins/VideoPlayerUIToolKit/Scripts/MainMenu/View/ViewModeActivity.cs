using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModeActivity : MonoBehaviour
{
    [SerializeField] private RoomManager _roomManager;

    public void SetActive(bool isActive)
    {
        _roomManager.gameObject.SetActive(isActive);
    }
}
