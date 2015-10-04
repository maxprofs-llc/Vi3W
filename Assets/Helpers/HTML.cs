using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Helpers;
using Assets.Models;
using System.Globalization;
using UnityEngine;

public class HTMLInterface {
	private bool fetching = true;
	public JSONObject data;
	public List<List<W3Object>> lists;
	
	public JSONObject getJSON(Dictionary<string,string> query, string url) {
		string queryString = "?";
		foreach(KeyValuePair<string,string> item in query) {
			queryString += item.Key + "=" + item.Value + "&";
		}
		if(queryString == "?") {
			queryString = "";
		}
		
		//var www = new WWW(url + queryString);
		//yield return www;
		
		var assets = Resources.Load(url) as TextAsset;
		string text = assets.text;
		
		return new JSONObject(text);
	}
	
	public List<List<W3Object>> getLists(Dictionary<string,string> query, string url) {
		this.data = this.getJSON(query, url);
	
		//while(fetching)
		//	yield return new WaitForSeconds(0.1f);

		int indicatorCount = 0;
		if(data != null && data.list.Count > 0) {
			var first = data.list[0];
			var keys = first["d"].keys;
			indicatorCount = keys.Count;
		}
		
		var res = new List<List<W3Object>>();
		
		for(int i = 0;i < indicatorCount; i++) {
			res.Add(new List<W3Object>());
		}
		
		foreach(var item in data.list) {
			for(int i = 0;i < indicatorCount; i++) {
				var obj = new W3Number();
				
				obj.value = item["d"][item["d"].keys[i]].n;
				obj.type = item["type"].str;
				obj.tag = item["tag"].str;
				obj.indicator = item["d"].keys[i];
				obj.name = item["Name"].str;
				obj.longitude = double.Parse(item["long"].str,  CultureInfo.InvariantCulture);
				obj.latitude = double.Parse(item["lat"].str,  CultureInfo.InvariantCulture);
				
				res[i].Add(obj);
			}
		}
		
		this.lists = res;
		return res;
	}
}