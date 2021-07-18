using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME STEP INTERPOLATION
// similar configuration from timer
public class AdvancedInterpolation
{
    // works with rotation, scale, position, velocity, color 
    public MonoBehaviour monoBehaviourObj;
    public delegate void InterpolationFinishDelegate();
    public delegate void UpdateObjectProperty(Vector3 property);
    delegate float InterpolateFunction(float t);
    public enum Ease { InExpo, OutExpo, InOutExpo, InBounce, OutBounce, InOutBounce, InBack, OutBack, InOutBack, InElastic, OutElastic, InOutElastic, 
    InCircular, OutCircular, InOutCircular, InSinusoidal, OutSinusoidal, InOutSinusoidal, InQuintic, OutQuintic, InOutQuintic, InQuartic, OutQuartic,
    InOutQuartic, InCubic, OutCubic, InOutCubic, InQuadratic, OutQuadratic, InOutQuadratic, Linear };

    public float duration;
    float s = 1.70158f, s2 = 2.5949095f;
    bool isActive = false;
    public Vector3 startValue, endValue;
    InterpolationFinishDelegate onFinishDelegate;
    UpdateObjectProperty updateObjectProperty;
    InterpolateFunction interpolateFunction;
    LoudTimer interpolationTimer;


    public AdvancedInterpolation(MonoBehaviour mono, float d, Vector3 end, Vector3 start, InterpolationFinishDelegate finishFunc, UpdateObjectProperty updatePropertyFunc)
    { 
        onFinishDelegate = finishFunc;
        updateObjectProperty = updatePropertyFunc;
        monoBehaviourObj = mono;
        startValue = start;
        endValue = end;
        duration = d;

        interpolationTimer = new LoudTimer(duration, 0f, monoBehaviourObj, InterpolationEnded, UpdateInterpolation);
    }

    public virtual void Interpolate(Ease easingFunction) 
    { 
        isActive = true; 
        interpolateFunction = GetEasingFunction(easingFunction);
        interpolationTimer.Execute();
    }

    InterpolateFunction GetEasingFunction(Ease easingFunction)
    {
        switch (easingFunction)
        {
            case Ease.InExpo: return EaseInExpo;
            case Ease.OutExpo: return EaseOutExpo;
            case Ease.InOutExpo: return EaseInOutExpo;
            case Ease.InBounce: return EaseInBounce;
            case Ease.OutBounce: return EaseOutBounce;
            case Ease.InOutBounce: return EaseInOutBounce;
            case Ease.InBack: return EaseInBack;
            case Ease.OutBack: return EaseOutBack;
            case Ease.InOutBack: return EaseInOutBack;
            case Ease.InElastic: return EaseInElastic;
            case Ease.OutElastic: return EaseOutElastic;
            case Ease.InOutElastic: return EaseInOutElastic;
            case Ease.InCircular: return EaseInCircular;
            case Ease.OutCircular: return EaseOutCircular;
            case Ease.InOutCircular: return EaseInOutCircular;
            case Ease.InSinusoidal: return EaseInSinusoidal;
            case Ease.OutSinusoidal: return EaseOutSinusoidal;
            case Ease.InOutSinusoidal: return EaseInOutSinusoidal;
            case Ease.InQuintic: return EaseInQuintic;
            case Ease.OutQuintic: return EaseOutQuintic;
            case Ease.InOutQuintic: return EaseInOutQuintic;
            case Ease.InQuartic: return EaseInQuartic;
            case Ease.OutQuartic: return EaseOutQuartic;
            case Ease.InOutQuartic: return EaseInOutQuartic;
            case Ease.InCubic: return EaseInCubic;
            case Ease.OutCubic: return EaseOutCubic;
            case Ease.InOutCubic: return EaseInOutCubic;
            case Ease.InQuadratic: return EaseInQuadratic;
            case Ease.OutQuadratic: return EaseOutQuadratic;
            case Ease.InOutQuadratic: return EaseInOutQuadratic;
            case Ease.Linear: return EaseLinear;
            default: return null;
        }
    }

    public virtual void StopInterpolation()
    {
        if (!isActive) return; 
        interpolationTimer.StopTimer();
        isActive = false;
    }

    public void ResetInterpolation()
    {
        interpolationTimer.ResetTimer();
        isActive = false;
    }

    void InterpolationEnded()
    {
        ResetInterpolation();
        updateObjectProperty(endValue);
        onFinishDelegate();
    }
    
    void UpdateInterpolation()
    {
        float progress = interpolateFunction(interpolationTimer.timerProgress);
        float x = startValue.x + (endValue.x - startValue.x) * progress;
        float y = startValue.y + (endValue.y - startValue.y) * progress;
        float z = startValue.z + (endValue.z - startValue.z) * progress;

        updateObjectProperty(new Vector3(x, y, z));
    }

    // Exponencial
    float EaseInExpo(float t) { return t == 0f ? 0f : Mathf.Pow(1024f, t - 1f); }
    float EaseOutExpo(float t) { return t == 1f ? 1f : 1f - Mathf.Pow(2f, - 10f * t); }
    float EaseInOutExpo(float t) 
    {
        if (t == 0f) return 0f;
        if (t == 1f) return 1f;
        if ((t *= 2f) < 1f) return 0.5f * Mathf.Pow(1024f, t - 1f);
        return 0.5f * (- Mathf.Pow(2f, - 10f * (t - 1f)) + 2f); 
    }

    // Bounce
    float EaseOutBounce(float t)
    {
        if (t < (1f / 2.75f)) return 7.5625f * t * t;				
        else if (t < (2f / 2.75f)) return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
        else if (t < (2.5f / 2.75f)) return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
        else return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
    }
    float EaseInBounce(float t) { return 1f - EaseOutBounce(1f - t); }
    float EaseInOutBounce(float t) 
    {
        if (t < 0.5f) return EaseInBounce(t * 2f) * 0.5f;
        return EaseOutBounce(t * 2f - 1f) * 0.5f + 0.5f;
    }

    // Back
    float EaseOutBack(float t) { return (t -= 1f) * t * ((s + 1f) * t + s) + 1f; }
    float EaseInBack(float t) { return t * t * ((s + 1f) * t - s); }
    float EaseInOutBack(float t) 
    { 
        if ((t *= 2f) < 1f) return 0.5f * (t * t * ((s2 + 1f) * t - s2));
        return 0.5f * ((t -= 2f) * t * ((s2 + 1f) * t + s2) + 2f);
    }

    // Elastic
    float EaseOutElastic(float t) 
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        return Mathf.Pow(2f, - 10f * t) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
    }
    float EaseInElastic(float t) 
    { 
        if (t == 0) return 0;
        if (t == 1) return 1;
        return - Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f);
    }
    float EaseInOutElastic(float t) 
    { 
        if ((t *= 2f) < 1f) return - 0.5f * Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f);
        return Mathf.Pow(2f, - 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
    }

    // Circular
    float EaseOutCircular(float t) { return Mathf.Sqrt(1f - ((t -= 1f) * t)); }
    float EaseInCircular(float t) { return 1f - Mathf.Sqrt(1f - t * t); }
    float EaseInOutCircular(float t) 
    { 
        if ((t *= 2f) < 1f) return - 0.5f * (Mathf.Sqrt(1f - t * t) - 1);
        return 0.5f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f);
    }
    
    // Sinusoidal
    float EaseOutSinusoidal(float t) { return Mathf.Sin(t * Mathf.PI / 2f); }
    float EaseInSinusoidal(float t) { return 1f - Mathf.Cos(t * Mathf.PI / 2f); }
    float EaseInOutSinusoidal(float t) { return 0.5f * (1f - Mathf.Cos(Mathf.PI * t)); }
    
    // Quintic
    float EaseOutQuintic(float t) { return 1f + ((t -= 1f) * t * t * t * t); }
    float EaseInQuintic(float t) { return t * t * t * t * t; }
    float EaseInOutQuintic(float t) 
    { 
        if ((t *= 2f) < 1f) return 0.5f * t * t * t * t * t;
        return 0.5f * ((t -= 2f) * t * t * t * t + 2f);
    }
    
    // Quartic
    float EaseOutQuartic(float t) { return 1f - ((t -= 1f) * t * t * t); }
    float EaseInQuartic(float t) { return t * t * t * t; }
    float EaseInOutQuartic(float t) 
    { 
        if ((t *= 2f) < 1f) return 0.5f * t * t * t * t;
        return - 0.5f * ((t -= 2f) * t * t * t - 2f);
    }

    // Cubic
    float EaseOutCubic(float t) { return 1f + ((t -= 1f) * t * t); }
    float EaseInCubic(float t) { return t * t * t; }
    float EaseInOutCubic(float t) 
    { 
        if ((t *= 2f) < 1f) return 0.5f * t * t * t;
        return 0.5f * ((t -= 2f) * t * t + 2f);
    }

    // Quadratic
    float EaseOutQuadratic(float t) { return t * (2f - t); }
    float EaseInQuadratic(float t) { return t * t; }
    float EaseInOutQuadratic(float t) 
    { 
        if ((t *= 2f) < 1f) return 0.5f * t * t;
        return - 0.5f * ((t -= 1f) * (t - 2f) - 1f);
    }

    // Linear
    float EaseLinear(float t) { return t; }
}
