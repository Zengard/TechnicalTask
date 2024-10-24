using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour, ITouchable, IMouseable
{
    [Header("Strips settings")]
    [SerializeField] private LineRenderer[] _lineRenderers = new LineRenderer[2];
    [SerializeField] private Transform[] _striptsPosition = new Transform[2];
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private float _maxLength = 3;

    private Vector3 _currentPosition;

    [Header("Bird settings")]
    [SerializeField] private Bird _currentBird;
    [SerializeField] private float _birdPositionOffset;
    [SerializeField] private float _shootForce;

    [SerializeField] private RectTransform _touchArea;

    //getters and setters
    public Bird CurrentBird { get { return _currentBird; } set { _currentBird = value; } }
    public Transform CenterPosition { get { return _centerPosition; } }

    public bool IsFingerTouchScreen { get ; set; }
    public bool IsMouseClickedScreen { get; set; }

    private void Start()
    {
        InitLines();
        ResetLines();     
    }

    private void Update()
    {
        if(_currentBird != null)
        {
            FingerTouch();
            MouseClick();
        }

        ScreenTouched();
        ScreenClicked();
    }

    public void FingerTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(RectTransformUtility.RectangleContainsScreenPoint(_touchArea, touch.position))
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                {
                    IsFingerTouchScreen = true;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (IsFingerTouchScreen == true)
                    {
                        IsFingerTouchScreen = false;
                        Shoot();
                        ResetLines();
                    }
                }
            }     
        }
    }

    public void ScreenTouched()
    {
        if (IsFingerTouchScreen == true)
        {
            Vector2 mousePosition = Input.mousePosition;
            _currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            _currentPosition = _centerPosition.position +
                Vector3.ClampMagnitude(_currentPosition - _centerPosition.position, _maxLength);

            UpdateLinesPos(_currentPosition);
        }
    }

    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_touchArea, Input.mousePosition))
            {
                IsMouseClickedScreen = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(IsMouseClickedScreen == true)
            {
                IsMouseClickedScreen = false;
                Shoot();
                ResetLines();
            }               
        }
    }

    public void ScreenClicked()
    {
        if (IsMouseClickedScreen == true)
        {
            Vector2 mousePosition = Input.mousePosition;
            _currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            _currentPosition = _centerPosition.position +
                Vector3.ClampMagnitude(_currentPosition - _centerPosition.position, _maxLength);

            UpdateLinesPos(_currentPosition);
        }
    }

    private void InitLines()
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].positionCount = 2;
            _lineRenderers[i].SetPosition(0, _striptsPosition[i].position);
        }
    }

    private void UpdateLinesPos(Vector3 position)
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].SetPosition(1, position);
        }

        if (_currentBird != null)
        {
            Vector3 direction =position - _centerPosition.position;
            _currentBird.transform.position = position + direction.normalized * _birdPositionOffset;
        }
    }

    private void Shoot()
    {
        _currentBird.Rigidbody2D.isKinematic = false;
        _currentBird.CircleCollider2D.isTrigger = false;
        Vector3 birdForce = (_currentPosition - _centerPosition.position) * _shootForce * -1;
        _currentBird.Rigidbody2D.velocity = birdForce;

        _currentBird.GetComponent<Bird>().ReleaseBird();

        _currentBird = null;
    }

    private void ResetLines()
    {
        _currentPosition = _idlePosition.position;
        UpdateLinesPos(_currentPosition);
    }
}
