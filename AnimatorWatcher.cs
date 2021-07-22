using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWatcher
{
    public Event animFinishEvent;
    string animName;
    Animator animator;
    AnimationClip clip;
    AnimationEvent animationEvent;

    public AnimatorWatcher(Animator animationToWatch, string name, string funcName)
    {
        animFinishEvent = new Event();
        animator = animationToWatch;
        animName = name;

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i ++)
            if (clips[i].name == animName)
                clip = animator.runtimeAnimatorController.animationClips[i];

        animationEvent = new AnimationEvent();
        animationEvent.time = clip.length;
        animationEvent.functionName = funcName;

        clip.AddEvent(animationEvent);
    }
}
