using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCharacter : MonoBehaviour
{
    Toggle m_Toggle;
    public SkeletonDataAsset anim;
    public GameController gc;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

      
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        
        gc.ChangeAnim(anim);  
    }
}
