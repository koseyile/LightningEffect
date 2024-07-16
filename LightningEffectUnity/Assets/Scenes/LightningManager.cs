using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.ThunderAndLightning;
using UnityEngine.UI;

public class LightningManager : MonoBehaviour
{
    public Slider IntensitySlider;
    public LightningBoltPrefabScriptBase WallLightning;
    public LightningBoltPrefabScriptBase GroundLightning;
    public LightningBoltPrefabScriptBase[] RandomLightings;
    // Start is called before the first frame update
    void Start()
    {
        LightningIntensityChange();
        //StartCoroutine(LightningAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LightningIntensityChange()
    {
        WallLightning.CountRange.Maximum = (int)Mathf.Lerp(1, 8, IntensitySlider.value);
        WallLightning.ChaosFactor = Mathf.Lerp(0.15f, 0.15f, IntensitySlider.value);
        WallLightning.IntervalRange.Minimum = Mathf.Lerp(1f, 0.05f, IntensitySlider.value);
        WallLightning.IntervalRange.Maximum = Mathf.Lerp(1f, 0.05f, IntensitySlider.value);

        GroundLightning.CountRange.Maximum = (int)Mathf.Lerp(1, 8, IntensitySlider.value);
        GroundLightning.ChaosFactor = Mathf.Lerp(0.25f, 0.25f, IntensitySlider.value) * 2f;
        GroundLightning.IntervalRange.Minimum = Mathf.Lerp(1f, 0.3f, IntensitySlider.value);
        GroundLightning.IntervalRange.Maximum = Mathf.Lerp(1f, 0.3f, IntensitySlider.value);

        for (int i = 0; i < RandomLightings.Length; i++)
        {
            RandomLightings[i].IntervalRange.Minimum = Mathf.Lerp(0.3f, 0.1f, IntensitySlider.value);
            RandomLightings[i].IntervalRange.Maximum = Mathf.Lerp(0.3f, 0.1f, IntensitySlider.value);
        }
    }

    private IEnumerator LightningAnimation()
    {
        int index = 0;
        while ( true )
        {
            ShowRandomLightning(index);
            index++;
            if (index>= RandomLightings.Length)
            {
                index = 0;
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    public void ShowRandomLightning(int index)
    {
        for (int i = 0; i < RandomLightings.Length; i++)
        {
            if (i!=index)
            {
                RandomLightings[i].enabled = false;
            }
            else
            {
                RandomLightings[i].enabled = true;
            }
            
        }
    }
}
