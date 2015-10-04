using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Helpers;
using Assets.Models;
using System.Globalization;

public class HTMLInterface : MonoBehaviour {
	private bool fetching = true;
	public JSONObject data;
	public List<List<W3Object>> lists;
	
	public IEnumerator getJSON(Dictionary<string,string> query, string url) {
		string queryString = "?";
		foreach(KeyValuePair<string,string> item in query) {
			queryString += item.Key + "=" + item.Value + "&";
		}
		if(queryString == "?") {
			queryString = "";
		}
		
		var www = new WWW(url + queryString);
		yield return www;
		
		data = new JSONObject(www.text);
		fetching = false;
	}
	
	public IEnumerator getLists() {
		while(fetching)
			yield return new WaitForSeconds(0.1f);
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
				
				obj.value = double.Parse(item["d"][item["d"].keys[i]].str,  CultureInfo.InvariantCulture);
				obj.type = item["type"].str;
				obj.tag = item["tag"].str;
				obj.indicator = item["d"].keys[i];
				obj.name = item["name"].str;
				obj.longitude = double.Parse(item["longitude"].str,  CultureInfo.InvariantCulture);
				obj.latitude = double.Parse(item["latitude"].str,  CultureInfo.InvariantCulture);
				
				res[i].Add(obj);
			}
		}
		
		this.lists = res;
	}
}