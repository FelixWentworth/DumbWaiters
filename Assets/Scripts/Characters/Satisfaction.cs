using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This tracks the satisfaction for each object, these can be changed by providing the wrong food, or from environmental changes
/// </summary>
public class Satisfaction : MonoBehaviour
{
	[SerializeField] public Color SatisfiedColor;
	[SerializeField] public Color DissatisfiedColor;

	public float CharacterSatisfaction { get { return Mathf.Clamp01(FoodSatisfaction + EnvironmentSatisfaction); } }

	public float FoodSatisfaction { get; set; }
	private const float BaseFoodSatisfaction = 0.5f;

	public float EnvironmentSatisfaction { get; set; }
	private const float BaseEnvironmentSatisfaction = 0f;
	private List<GameObject> EnvironmentModifierObjects = new List<GameObject>();


	void Awake()
	{
		FoodSatisfaction = BaseFoodSatisfaction;
		EnvironmentSatisfaction = BaseEnvironmentSatisfaction;
	}

	public void ApplyEnvironemtChange(GameObject source, float change)
	{
		if (!EnvironmentModifierObjects.Contains(source))
		{
			EnvironmentSatisfaction += change;
			EnvironmentModifierObjects.Add(source);
		}
	}

	public void RemoveEnvironmentChange(GameObject source, float change)
	{
		if (EnvironmentModifierObjects.Contains(source))
		{
			EnvironmentSatisfaction += change;
			EnvironmentModifierObjects.Remove(source);
		}
	}

	public Color GetSatisfactionColor()
	{
		return Color.Lerp(DissatisfiedColor, SatisfiedColor, CharacterSatisfaction);
	}

}
