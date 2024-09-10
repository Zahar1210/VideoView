using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraView : MonoBehaviour
{
    [SerializeField] private UIDocument _roomUIDocument;
    public bool _isDragging = false;
    
    private float _startMouseX;
    private float _startMouseY;
    
    public Camera _cam;

    [SerializeField, Range(1, 120)]
    private float _zoomSpeed = 10f; // Скорость изменения угла обзора

    [SerializeField, Range(1, 120)]
    private float _minFov = 15f; // Минимальное значение угла обзора

    [SerializeField, Range(1, 120)]
    private float _maxFov = 90f; // Максимальное значение угла обзора
    
    void OnEnable()
    {
        _cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        HandleMouseDrag();
        HandleMouseScroll();
    }

    void HandleMouseDrag()
    {
        // если нажата левая кнопка мыши и мы не начали перетаскивание
        if (Input.GetMouseButtonDown(0) && !_isDragging)
        {
            // установить флаг в true
            _isDragging = true;

            // сохранить начальное положение мыши
            _startMouseX = Input.mousePosition.x;
            _startMouseY = Input.mousePosition.y;
        }
        // если левая кнопка мыши отпущена и мы были в состоянии перетаскивания
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            // установить флаг в false
            _isDragging = false;
        }
    }

    void HandleMouseScroll()
    {
        VerticalScrollView activePanel = GetActivePanel();
        if (activePanel is { IsMouseContains: true })
        {
            return;
        }
        
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        _cam.fieldOfView = Mathf.Clamp(_cam.fieldOfView - scrollData * _zoomSpeed, _minFov, _maxFov);
    }

    private VerticalScrollView GetActivePanel()
    {
        if (_roomUIDocument.gameObject.activeSelf)
        {
            List<VerticalScrollView> panels = _roomUIDocument.rootVisualElement.Query<VerticalScrollView>().ToList();
            return panels.FirstOrDefault(p => p.parent.parent.style.display == DisplayStyle.Flex);
        }

        return null;
    }
    
    void LateUpdate()
    {
        if (_isDragging)
        {
            float endMouseX = Input.mousePosition.x;
            float endMouseY = Input.mousePosition.y;
            
            float diffX = endMouseX - _startMouseX;
            float diffY = endMouseY - _startMouseY;
            
            float newCenterX = Screen.width / 2 + diffX;
            float newCenterY = Screen.height / 2 + diffY;
            
            Vector3 lookHerePoint = _cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, _cam.nearClipPlane));
            
            transform.LookAt(lookHerePoint);
            _startMouseX = endMouseX;
            _startMouseY = endMouseY;
        }
    }
}