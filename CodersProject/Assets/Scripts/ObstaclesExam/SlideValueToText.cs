using UnityEngine;
using UnityEngine.UI;
using TMPro;

//display slider value multiplied for "stepValue" to "valueText"
public class SlideValueToText : MonoBehaviour
{
    [SerializeField] private float minValue;            // bottom limit of value to show 
    [SerializeField] private TextMeshProUGUI valueText; //text object to schow current value from slider
    [SerializeField] private float stepValue = 0.1f;    
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        ValueChanged();
    }

    // called if slide value changed 
    //chande ui valueText 
    public void ValueChanged()
    {
        float _modifiedSliderValue = slider.value * stepValue;
        float _coeficient = _modifiedSliderValue < minValue ? minValue : _modifiedSliderValue;
        valueText.text = _coeficient.ToString();
    }
}
