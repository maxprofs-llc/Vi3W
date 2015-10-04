using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoParser {
	public Dictionary<string, List<Vector2>> getDistricts() {
		districts = Resources.load("nepal-districts.geojson") as string;

		var data = new JSONObject(districts);
		var res = new Dictionary<string, List<Vector2>>();
		
		foreach(var district in data["features"].list) {
			string name = district["properties"]["DISTRICT"].str;
			List<Vector2> arc = new List<Vector2>();
			foreach(var point in district["properties"]["geometry"]["coordinates"][0].list) {
				arc.Add(new Vector2(double.parse(point.list[0].str), double.parse(point.list[1].str)));
			}
			
			res[name] = arc;
		}
		
		return res;
	}

	public Dictionary<string, Vector2> getCities() {
		cities = Resources.load("nepal-district-headquarters.geojson") as string;
		
		var data = new JSONObject(cities);
		var res = new Dictionary<string, Vector2>();
		
		foreach(var city in data["features"].list) {
			string name = city["properties"]["HQ_NAME"].str;
			double longtitude = double.parse(city["properties"]["geometry"]["coordinates"].list[0].str);
			double latitude = double.parse(city["properties"]["geometry"]["coordinates"].list[1].str);
			Vector2 point = new Vector2(longtitude,latitude);
			
			res[name] = point;
		}
		
		return res;
	}
}