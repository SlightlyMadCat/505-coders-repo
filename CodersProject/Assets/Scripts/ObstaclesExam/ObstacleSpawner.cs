using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * spawn prefab objects allong "linesParent" canvas image width with certain offset 
 * fit inner child objects of prefab object according to coeficient from slider
 * */
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject linesParent;                    //parent of all targeted elements to create obsticles
    [SerializeField] private GameObject prefab;                         //prefab to Instantiate
    [SerializeField] private float minStep;                             //offset betwen prefab objects 
    [SerializeField] private float minScaleValue;                       //minimal value to scale objects
    [SerializeField] private GameObject fixedObstacles;                 // parent object that contain fixed obstacles in corners

    private float _coeficient = 1;                                      //multiply value from settings(slider)
    private float _offset;                                              //space betwen objects ,used as value container 
    private int _objects;                                               // number of objects to create on current iteration  ,used as value container 

    //creates obsticles on all child objects of linesParent object with certain "offset"
    public void CreateObsticles()
    {
        List<RectTransform> allChildrenTransform = linesParent.GetComponentsInChildren<RectTransform>().ToList();
        allChildrenTransform.RemoveAt(0);
        fixedObstacles.SetActive(true);
        List<RectTransform> cornerPoints = fixedObstacles.GetComponentsInChildren<RectTransform>().ToList();
        cornerPoints.RemoveAt(0);

        foreach (var child in allChildrenTransform)
        {
            //creates and set object container
            GameObject obstacles = new GameObject("obstacles");
            obstacles.transform.SetParent(child.transform);
            obstacles.AddComponent<RectTransform>();

            //fit obstacles.rectTransform to parameters of parent
            SetRectTransform(obstacles.GetComponent<RectTransform>(), child.sizeDelta.x);

            InstantiateByOffset(obstacles.GetComponent<RectTransform>());
        }

        //create single prefab for each point in list
        foreach (var child in cornerPoints)
        {
            Vector3 _position = new Vector3(0, 0, 0.1f);
            InstantiateToParent(child, _position);
        }
    }

    //set rectTransform to fit parent object with certain "width"
    private void SetRectTransform(RectTransform rectTransform, float width)
    {
        rectTransform.localScale = Vector3.one;
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.sizeDelta = new Vector2(width, 0);
    }

    //Instantiation with offset allong object 
    // rootForInstantiation - RectTransform of object on what we create prefabs
    private void InstantiateByOffset(RectTransform rootForInstantiation)
    {
        float putPositionMax = GetStartPosition(rootForInstantiation); 
        float putPosition = -putPositionMax;

        //int step = 0;
        //while(step<_objects)
            for (int i = 0; i < _objects; i++)
            {
                InstantiateToParent(rootForInstantiation, new Vector3(putPosition, 0, 0));
                putPosition += _offset;
            }
        //{
        //    InstantiateToParent(rootForInstantiation, new Vector3(putPosition, 0, 0));
        //    putPosition += _offset;
        //    step++;
        //}
    }

    /* compute offset betwen objects & number of objects to fit into  "rootForInstantiation " width space between objects >= "minStep"
     * rootForInstantiation - RectTransform of parent according to which width offset counted 
    * return position of last element according to offset
    */
    private float GetStartPosition(RectTransform rootForInstantiation)
    {
        float _areaWidthToPlace = rootForInstantiation.sizeDelta.x - minStep;
        int _objectsNumberToFit = Mathf.FloorToInt(_areaWidthToPlace / minStep);
        _offset = _areaWidthToPlace / (_objectsNumberToFit);
        float _midPosition = _objectsNumberToFit % 2 == 0 ? _offset / 2 : 0;
        int _numberOfSteps = _objectsNumberToFit / 2 == 0 ? _objectsNumberToFit / 2 : (_objectsNumberToFit - 1) / 2;
        _objects = _objectsNumberToFit;
        return _midPosition + (_offset * _numberOfSteps);
    }

    //Instantiation of single object in specific position
    private void InstantiateToParent(RectTransform parentPosition, Vector3 prefabLocalPosition)
    {
        GameObject go = Instantiate(prefab, parentPosition, false);
        go.transform.localPosition = prefabLocalPosition;
        SetChildParameters(go);
    }

    //scaling and repositioning of prefab inner objects to fit scale
    //parent - prefab object to find child objects  
    private void SetChildParameters(GameObject parent)
    {
        var childElements = parent.GetComponent<ChildElements>();
        var _pole = childElements.pole.transform;

        Vector3 _localScale = _pole.localScale;
        _pole.localScale = new Vector3(_localScale.x, _localScale.y, _localScale.z * _coeficient);

        var _ball = childElements.ball.transform;
        _ball.localPosition = new Vector3(0, 0, _ball.localPosition.z * _coeficient);
    }

    //method called every slider value chaned called from slider 
    public void CoeficientSet(Slider slider)
    {
        float _modifiedSliderValue = slider.value * 0.1f;
        _coeficient = _modifiedSliderValue < minScaleValue ? minScaleValue : _modifiedSliderValue;
    }
}
