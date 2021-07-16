using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/**
 * Obstacles generator on scene logic
 */

public class StandObstaclesSpawner : MonoBehaviour
{
    [Serializable]
    public class ObstaclePatternSample
    {
        public Transform startPoint;
        public Transform endPoint;
        public bool spawnEndPoint;  //used to spawn last obstacle in the line
        
        public float GetPatternLength()
        {
            return Vector3.Distance(startPoint.position, endPoint.position);
        }

        //choose spawn point for new obstacle
        //solution works only for 90" angles!
        public Vector3 GetCalculatedPos(int _obstacleIndex, float _offset)
        {
            Vector3 _dir = endPoint.position - startPoint.position; //pattern vector dir
            Vector3 _newPos = Vector3.zero;

            //choose direction to move new obstacle from start point
            if(Mathf.Round(_dir.x) != 0)
                _newPos = startPoint.position + new Vector3(_obstacleIndex * _offset, 0, 0);
            if(Mathf.Round(_dir.y) != 0)
                _newPos = startPoint.position + new Vector3(0, _obstacleIndex * _offset, 0);
            if(Mathf.Round(_dir.z) != 0)
                _newPos = startPoint.position + new Vector3(0, 0, _obstacleIndex * _offset);
            
            return _newPos;
        }
    }

    public List<ObstaclePatternSample> obstaclePatternSamples = new List<ObstaclePatternSample>();
    public GameObject obstaclePref;
    [Range(.25f,10)]
    public float obstacleOffset;
    
    public void GenerateObstacles()
    {
        Generation();
    }

    //select places to spawn
    private void Generation()
    {
        foreach (ObstaclePatternSample _pattern in obstaclePatternSamples)
        {
            SpawnObstacle(_pattern.startPoint.position, _pattern.startPoint);
            
            //spawn the last one obstacle in the list
            if(_pattern.spawnEndPoint)
                SpawnObstacle(_pattern.endPoint.position, _pattern.startPoint);

            float _desireObstaclesCount = Mathf.FloorToInt(_pattern.GetPatternLength() / obstacleOffset);   //calculate desire obstacles num in this pattern
            float _realObstaclesOffset = _pattern.GetPatternLength() / _desireObstaclesCount;   //get real offset between estimated obstacles count 

            //fit the gap between corner obstacles
            for (int _i = 1; _i < _desireObstaclesCount; _i++)
            {
                SpawnObstacle(_pattern.GetCalculatedPos(_i, _realObstaclesOffset), _pattern.startPoint);
            }
        }
    }

    //obstacle creation on scene
    private void SpawnObstacle(Vector3 _pos, Transform _parent)
    {
        Instantiate(obstaclePref, _pos, quaternion.identity, _parent);
    }
}

