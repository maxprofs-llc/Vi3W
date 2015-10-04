using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoParser
{
	public Dictionary<string, List<Vector2>> getDistricts()
	{
		TextAsset districtsAsset = Resources.Load("nepal-districts") as TextAsset;
		string districts = districtsAsset.text;


		var data = new JSONObject(districts);
		var res = new Dictionary<string, List<Vector2>>();

		foreach (var district in data["features"].list)
		{
			string name = district["properties"]["DISTRICT"].str;
			List<Vector2> arc = new List<Vector2>();
			if (district != null)
			{
				foreach (var point in district["properties"]["geometry"]["coordinates"].list[0].list)
				{
					arc.Add(new Vector2(float.Parse(point.list[0].str), float.Parse(point.list[1].str)));
				}
			}
			else
			{
				Debug.Log("Null District");
			}

			res[name] = arc;
		}

		return res;
	}

	public Dictionary<string, Vector2> getCities()
	{
		string cities = (Resources.Load("nepal-district-headquarters") as TextAsset).text;

		var data = new JSONObject(cities);
		var res = new Dictionary<string, Vector2>();

		foreach (var city in data["features"].list)
		{
			string name = city["properties"]["HQ_NAME"].str;
			float longtitude = float.Parse(city["properties"]["geometry"]["coordinates"].list[0].str);
			float latitude = float.Parse(city["properties"]["geometry"]["coordinates"].list[1].str);
			Vector2 point = new Vector2(longtitude, latitude);

			res[name] = point;
		}

		return res;
	}
}