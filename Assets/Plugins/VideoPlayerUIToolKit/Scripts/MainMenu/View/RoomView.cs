using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    [SerializeField] private Point[] _points;
    
    public void SetActiveRoomView(bool isActive)
    {
        gameObject.SetActive(isActive);
        foreach (var point in _points)
        {
            point.SetActive(isActive);
        }
    }
}
