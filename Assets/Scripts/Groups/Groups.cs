using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Groups : MonoBehaviour {

	private static Groups _instance;

	[Serializable]
	public class SUGARGroup
	{
		public string Name;
		public int Id;
		public Sprite Icon;
		public Color Color;
	}

	public List<SUGARGroup> SugarGroups;

	public int GetGroupId(int num)
	{
		return SugarGroups[num].Id;
	}
	void Start()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public Sprite GetSpriteById(int id)
	{
		var group = SugarGroups.First(g => g.Id == id);
		return group != null ? group.Icon : null;
	}

	public string GetNameById(int id)
	{
		var group = SugarGroups.First(g => g.Id == id);
		return group != null ? group.Name : "";
	}

	public Color GetColorById(int id)
	{
		var group = SugarGroups.First(g => g.Id == id);
		return group != null ? group.Color : Color.white;
	}

	public List<int> GroupIds()
	{
		return SugarGroups.Select(g => g.Id).ToList();
	}

}
