using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester3 : MonoBehaviour
{
    AdvancedInterpolation interpolation1, interpolation2;

    void Start()
    {
        interpolation1 = new AdvancedInterpolation(this, 4f, new Vector3(3f, 0, 0), transform.position, () => {interpolation2.Interpolate(AdvancedInterpolation.Ease.InOutQuartic);}, property => {transform.position = property;});
        interpolation2 = new AdvancedInterpolation(this, 4f, transform.position, interpolation1.endValue, () => {interpolation1.Interpolate(AdvancedInterpolation.Ease.InOutQuartic);}, property => {transform.position = property;});
        interpolation1.Interpolate(AdvancedInterpolation.Ease.InOutQuartic);
    }

    void Update()
    {
        
    }
}
