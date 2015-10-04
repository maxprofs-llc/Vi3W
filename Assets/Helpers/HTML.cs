using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTMLInterface : MonoBehaviour {
	public JSONObject data;
	public List<list<W3Object>> lists;
	
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
	}
	
	public IEnumerator getLists() {
		int indicatorCount = 0;
		if(data != null && data.list.Count > 0) {
			indicatorCount = data.list[0]['v'].Keys.Count;
		}
		
		var res = new List<List<W3Object>>();
		
		for(int i = 0;i < indicatorCount; i++) {
			res.Add(new List<W3Object>());
		}
		
		foreach(var item in data.list) {
			for(int i = 0;i < indicatorCount; i++) {
				var obj = new W3Number();
				
				obj.value = double.parse(item["v"][item["v"].Keys[i]],  CultureInfo.InvariantCulture);
				obj.type = item["type"];
				obj.tag = item["tag"];
				obj.indicator = item["v"].Keys[i];
				obj.name = item["name"];
				obj.longtitude = double.parse(item["longtitude"],  CultureInfo.InvariantCulture);
				obj.latitude = double.parse(item["latitude"],  CultureInfo.InvariantCulture);
				
				res[i].Add(obj);
			}
		}
		
		this.lists = res;
	}
}