using UnityEngine;
using UnityEngine.UI;


/* multiply scale (x,y,z) of objectsToScale to slider value multipied for stepValue
 * this script locate on slider component
 */
public class ChangeObjectsScaleFromSlider : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToScale;             //target objects to multiply scale 
    [SerializeField] private float stepValue = 0.1f;                  // value of step 
    private Slider slider;                                            //Slider Component
    private float nativeScale;                                        //scale, of first target object ,at the start

    void Start()
    {
        slider = GetComponent<Slider>();
        nativeScale = objectsToScale[0].transform.localScale.x;
        ScaleObjects();
    }

    // multiply original scale of all objects in objectsToScale 
    public void ScaleObjects()
    {
        float localSliderValue = slider.value * stepValue;

        //change scale of all objects in objectsToScale
        foreach (var item in objectsToScale)
        {
            float _newScale = nativeScale * localSliderValue;
            item.transform.localScale = new Vector3(_newScale, _newScale, _newScale);
        }
    }
}
