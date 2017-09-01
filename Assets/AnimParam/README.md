**AnimatorParameter**
=====================


![](https://s26.postimg.org/bodvt4215/Anim_Param_Mono.png)

![](http://img.pixady.com/2017/09/542257_animparamexscriptableobj_460x233.png)

```
using UnityEngine;

public class AnimParamAttributeExample : MonoBehaviour {
    [SerializeField] Animator _anim;
    [SerializeField,AnimParam] string _paramName;
    [SerializeField,AnimParam(AnimParamAttribute.ParamType.Bool)] string _paramNameBool;
    [SerializeField,AnimParam(AnimParamAttribute.ParamType.Int)] int[] _paramHashInt;

    [SerializeField] AnimParamAttributeExampleScriptableObj _paramAsset;

    void Reset() {
        _anim = GetComponent<Animator>();
    }

}

```

```

using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(menuName = "Examples/AnimParamAttributeExampleScriptableObj", fileName = "AnimParamAttributeExample")]
public class AnimParamAttributeExampleScriptableObj : ScriptableObject {

    [SerializeField] AnimatorController _animController;
    [SerializeField,AnimParam("_animController", AnimParamAttribute.ParamType.Float)] string _paramNameFloat;
    [SerializeField,AnimParam("_animController", AnimParamAttribute.ParamType.Trigger)] int _paramHashTrigger;

    public void Play(Animator anim) {
        if(anim.runtimeAnimatorController != _animController) {
            return;
        }

        Debug.Log(anim.GetFloat(_paramNameFloat));
        anim.SetTrigger(_paramHashTrigger);
    }
}


```
 













