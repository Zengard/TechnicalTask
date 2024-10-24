using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [SerializeField] GameObject[] _pathPoints;
    [SerializeField] float _timeInterval;
    
    private List<GameObject> _lastPoint;
    private int _lastIndex = 0;

    public static PathPoint instance;

    //getters and setters
    public float TimeInterval { get { return _timeInterval; } }

    private void Start()
    {
        instance = this;
        _lastPoint = new List<GameObject>();
    }

    public void CreatePathPoint(Vector2 position)
    {
        _pathPoints[_lastIndex].transform.position = position;
        _pathPoints[_lastIndex].transform.rotation = Quaternion.identity;
        _pathPoints[_lastIndex].SetActive(true);

        _lastIndex++;

        if(_lastIndex == _pathPoints.Length)
        {
            _lastIndex = 0;
        }
    }

    public void Clear()
    {
        foreach(GameObject point in _pathPoints)
        {
            point.SetActive(false);
        }
    }

}
