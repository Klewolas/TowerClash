using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProvider : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform[] _wayPoints;

    public Transform StartPoint => _startPoint;
    public Transform[] WayPoints => _wayPoints;

    public Vector3[] GetWayPointsVector3()
    {
        var vectors = new Vector3[_wayPoints.Length];

        for (var i = 0; i < _wayPoints.Length; i++)
        {
            vectors[i] = _wayPoints[i].position;
        }

        return vectors;
    }
    
}
