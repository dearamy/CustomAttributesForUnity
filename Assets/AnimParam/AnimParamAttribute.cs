using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using Object = UnityEngine.Object;
using UnityEditor;
using UnityEditor.Animations;
using System;
using System.Collections.Generic;
#endif

/// <summary>
/// Animator Paramater attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class AnimParamAttribute : PropertyAttribute {
	public ParamType parameterType = ParamType.None;
	public int selectedValue;
	public string animatorName;

	public AnimParamAttribute() : this(ParamType.None) {
		
	}

	public AnimParamAttribute(ParamType parameterType) {
		this.parameterType = parameterType;
	}
	//Set a name search for field, if there isn't animator component on the GO
	public AnimParamAttribute(string animatorName) : this(animatorName, ParamType.None) {
		this.animatorName = animatorName;
	}

	public AnimParamAttribute(string animatorName, ParamType parameterType) {
		this.animatorName = animatorName;
		this.parameterType = parameterType;
	}

	public enum ParamType {
		Float = 1,
		Int = 3,
		Bool = 4,
		Trigger = 9,
		None = 9999,
	}
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(AnimParamAttribute))]
public class AnimParamDrawer : PropertyDrawer {
	AnimParamAttribute ap { get { return  attribute as AnimParamAttribute; } }

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		EditorGUI.BeginProperty(position, label, property);


		AnimatorController animatorController;
		animatorController = ap.animatorName == null ? 
			(property.serializedObject.targetObject as Component).GetComponent<Animator>().runtimeAnimatorController as AnimatorController :
			property.serializedObject.FindProperty(ap.animatorName).objectReferenceValue as AnimatorController;

		if(animatorController == null) {
			DefaultInspector(position, property, label);
			return;
		}

		var parameters = animatorController.parameters;

		if(parameters.Length == 0) {
			Debug.LogWarning("AnimationParamater is 0");
			if(property.propertyType == SerializedPropertyType.String) {
				property.stringValue = string.Empty;
			} else if(property.propertyType == SerializedPropertyType.Integer) {
				property.intValue = 0;
			}
			DefaultInspector(position, property, label);
			return;
		}

			
		var eventNames = parameters.Where(t => CanAddEventName(t.type)).Select(t => t.name).ToList();
		var eventInts = parameters.Where(t => CanAddEventName(t.type)).Select(t => t.nameHash).ToList();

		if(eventNames.Count == 0) {
			Debug.LogWarning(string.Format("{0} Parameter is 0", ap.parameterType));
			if(property.propertyType == SerializedPropertyType.String) {
				property.stringValue = string.Empty;
			} else if(property.propertyType == SerializedPropertyType.Integer) {
				property.intValue = 0;
			}
			DefaultInspector(position, property, label);
			return;
		}

		var eventNamesArray = eventNames.ToArray();

		if(property.propertyType == SerializedPropertyType.String) {
			var matchIndex = eventNames.FindIndex(eventName => eventName.Equals(property.stringValue));

			if(matchIndex != -1) {
				ap.selectedValue = matchIndex;
			}

			ap.selectedValue = EditorGUI.IntPopup(position, label.text, ap.selectedValue, eventNamesArray, SetOptionValues(eventNamesArray));
			property.stringValue = eventNamesArray[ap.selectedValue];
		} else if(property.propertyType == SerializedPropertyType.Integer) {
			
			//FIXME: Doesn't work, for unity creates one instance to draw all of them.
			var matchIndex = eventNames.FindIndex(eventName => eventName.Equals(property.intValue));
			if(matchIndex != -1) {
				ap.selectedValue = matchIndex;
			}

			var l = position;
			l.xMax -= position.width * 0.4f;
			var r = position;
			r.xMin = l.xMax + 20;	

			ap.selectedValue =	EditorGUI.IntPopup(l, label.text, ap.selectedValue, eventNamesArray, SetOptionValues(eventNamesArray));
			var s = new GUIStyle();

			s.normal.textColor = Color.blue;
			property.intValue = eventInts[ap.selectedValue];
			EditorGUI.IntField(r, property.intValue, s);
			//property.intValue = Animator.StringToHash(eventNamesArray[ap.selectedValue]);
		}
	
		EditorGUI.EndProperty();
	}

	bool CanAddEventName(AnimatorControllerParameterType animatorControllerParameterType) {
		return !(ap.parameterType != AnimParamAttribute.ParamType.None
		&& (int)animatorControllerParameterType != (int)ap.parameterType);
	}

	/// <summary>
	/// Sets the option values.
	/// </summary>
	/// <returns>
	/// The option values.
	/// </returns>
	/// <param name='eventNames'>
	/// Event names.
	/// </param>
	int[] SetOptionValues(string[] eventNames) {
		int[] optionValues = new int[eventNames.Length];
		for(int i = 0; i < eventNames.Length; i++) {
			optionValues[i] = i;
		}
		return optionValues;
	}


	void DefaultInspector(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.PropertyField(position, property, label);
	}
}

#endif