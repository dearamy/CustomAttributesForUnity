**AnimatorParameter**
=====================


![](https://photos-6.dropbox.com/t/2/AAC5pUHwKgBXwGhl69EsGaZVWp51IwOyioHb30PJ20-RhA/12/419545301/png/32x32/1/_/1/2/AnimParamExScriptableObj.png/EOXDzN8FGAIgAigC/LOJ3ph_O4KOOTWxJebhxwN0ksCrvHApTdJhhJJHoDYE?size=2048x1536&size_mode=3)

![](https://photos-2.dropbox.com/t/2/AABi8LlknXQR48MLo1Mhkm0lyYXlnLNID8r4lPmcXrg67w/12/419545301/png/32x32/1/_/1/2/AnimParamMono.png/EOXDzN8FGAIgAigC/3vJFb1D8pjLFeaeZeIJ2hUT1dcecu3EWDIAcH0WUpNw?size=2048x1536&size_mode=3)

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


----------


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
 













