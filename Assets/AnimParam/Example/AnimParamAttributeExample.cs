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

	[ContextMenu("Test")]
	void Test() {
		if(Application.isPlaying) {
			_paramAsset.Play(_anim);
		}
		//Debug.Log(_anim.GetInteger(_paramHashInt));
	}
}
