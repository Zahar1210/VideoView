using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Point : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    public UIDocument UIDocument => _uiDocument;
    public Button PointElement { get; set; }

    public abstract void SetActive(bool isActive);
}
