using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	// Put the data you want to save in a file
	public int gold = 0;
	public List<SerializedItem> weapons = new List<SerializedItem>();
	public List<SerializedItem> consumables = new List<SerializedItem>();

	public void DefaultData()
    {
		this.ResetData();

		// Player needs at least one weapon
	}

	public void ResetData()
    {
		this.gold = 0;
		this.weapons.Clear();
		this.consumables.Clear();
    }

	public string ToJson()
	{
		return JsonUtility.ToJson(this);
	}

	public void FromJson(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);
	}
}