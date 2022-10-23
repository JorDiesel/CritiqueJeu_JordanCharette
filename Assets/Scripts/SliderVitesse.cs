using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVitesse : MonoBehaviour
{

    public Slider slider;
    public BouleBlanche bouleBlanche;

    void Start()
    {
        slider.onValueChanged.AddListener(delegate { bouleBlanche.SetVitesse(slider.value); });
    }
}
