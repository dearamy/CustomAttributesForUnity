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
