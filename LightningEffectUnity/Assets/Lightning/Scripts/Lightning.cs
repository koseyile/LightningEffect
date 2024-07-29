using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.ThunderAndLightning;

public class Lightning : MonoBehaviour
{
    public Slider IntensitySlider;
    public LightningBoltPrefabScriptBase[] LightningBolts;
    // Start is called before the first frame update
    void Start()
    {
        LightningIntensityChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightningIntensityChange()
    {
        for (int i = 0; i < LightningBolts.Length; i++)
        {
            LightningBolts[i].CountRange.Maximum = (int)Mathf.Lerp(1, 8, IntensitySlider.value);
            LightningBolts[i].ChaosFactor = Mathf.Lerp(0.1f, 0.2f, IntensitySlider.value);

            //LightningBolts[i].IntervalRange.Minimum = Mathf.Lerp(1f, 0.05f, IntensitySlider.value);
            //LightningBolts[i].IntervalRange.Maximum = Mathf.Lerp(1f, 0.05f, IntensitySlider.value);
        }

    }
}
