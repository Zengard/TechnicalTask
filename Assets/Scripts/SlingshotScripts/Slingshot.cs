using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
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

    private ShootController _shootController;
    private InputController _inputController;

    //getters and setters
    public Bird CurrentBird { get { return _currentBird; } set { _currentBird = value; } }
    public Transform CenterPosition { get { return _centerPosition; } }
    public Vector3 CurrentPosition { get { return _currentPosition;} set { _currentPosition = value; } }
    public float ShootForce { get { return _shootForce; } }
    public RectTransform TouchArea { get { return _touchArea; } }
    public ShootController ShootController { get { return _shootController; } }
    public float MaxLength { get { return _maxLength; } }

    private void Start()
    {
        _shootController = new ShootController(this);
        _inputController = new InputController(this);

        InitLines();
        ResetLines();     
    }

    private void Update()
    {
        if(_currentBird != null)
        {
            _inputController.FingerTouch();
            _inputController.MouseClick();
        }

        _inputController.ScreenTouched();
        _inputController.ScreenClicked();
    }

    private void InitLines()
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].positionCount = 2;
            _lineRenderers[i].SetPosition(0, _striptsPosition[i].position);
        }
    }

    public void UpdateLinesPos(Vector3 position)
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

    public void ResetLines()
    {
        _currentPosition = _idlePosition.position;
        UpdateLinesPos(_currentPosition);
    }
}
