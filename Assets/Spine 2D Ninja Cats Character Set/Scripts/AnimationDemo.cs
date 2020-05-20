using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationDemo : MonoBehaviour
{
    public SkeletonAnimation anim;
    public int index;
    private string m_name = "";

    private void Start()
    {
        m_name = anim.skeleton.Data.Animations.Items[index].Name;
    }

    public void LateUpdate()
    {
        if (anim.AnimationName == m_name)
        {
            gameObject.GetComponent<Button>().Select();
        }
    }

    public void ChangeAnimation()
    {
        anim.AnimationState.SetAnimation(0, anim.skeleton.Data.Animations.Items[index], true);
    }
}
