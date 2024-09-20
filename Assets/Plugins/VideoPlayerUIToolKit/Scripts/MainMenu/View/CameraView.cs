using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraView : MonoBehaviour
{
    public Camera _cam;
    public bool _isDragging = false;
    [SerializeField] private UIDocument _roomUIDocument;
    [SerializeField] private Vector3 _offset = new Vector3(0, 5, -10);  // Смещение камеры от объекта
    [SerializeField] private float rotateSpeed = 2f;  // Скорость поворота камеры
    [SerializeField] private float _zoomTargetFOV;
    [SerializeField] private float _zoomSimpleFOV;
    [SerializeField, Range(1, 120)] private float _zoomSpeed = 10f; // Скорость изменения угла обзора
    [SerializeField, Range(1, 120)] private float _minFov = 15f; // Минимальное значение угла обзора
    [SerializeField, Range(1, 120)] private float _maxFov = 90f; // Максимальное значение угла обзора
    
    private Coroutine cameraMoveCoroutine;  // Ссылка на корутину, чтобы контролировать ее выполнение
    private float _startMouseX;
    private float _startMouseY;
    

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
    
    // Корутину для перемещения и поворота камеры
    public async Task MoveCameraToObject(Transform targetObject)
    {
        Vector3 targetPosition = targetObject.transform.position + _offset;
    
        float initialFOV = _cam.fieldOfView;

        Quaternion initialRotation = _cam.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetObject.transform.position.x, _cam.transform.position.y, targetObject.transform.position.z) - _cam.transform.position);
        
        while (Quaternion.Angle(_cam.transform.rotation, targetRotation) > 1f)
        {
            _cam.transform.rotation = Quaternion.Slerp(_cam.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        
            // Рассчитываем процент завершённости поворота
            float rotationProgress = Quaternion.Angle(initialRotation, _cam.transform.rotation) / Quaternion.Angle(initialRotation, targetRotation);
        
            // Используем процент для линейного изменения угла обзора (зум)
            _cam.fieldOfView = Mathf.Lerp(initialFOV, _zoomTargetFOV, rotationProgress);

            if (Quaternion.Angle(_cam.transform.rotation, targetRotation) <= 1f)
            {
                _cam.transform.position = targetPosition;
                _cam.transform.rotation = targetRotation;
                return;
            }
        
            await Task.Yield();
        }
    }

    public void AfterZoom()
    {
        _cam.transform.position = Vector3.zero;
        _cam.fieldOfView = _zoomSimpleFOV;
    }
}