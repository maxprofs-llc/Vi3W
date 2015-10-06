using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;


public class GeoDrawer : MonoBehaviour
{
	private Material lineMaterial;
	private List<VectorLine> lines;
	private float lineThickness = 4f;
	private float distanceToCenter = 10.96f;
	private GeoParser GP;
	private float towerEdgeSize = 0.03f;
	private float towerHeigthScale = 1f;


	void Start ()
	{
		lineMaterial = Resources.Load("DefaultLine3D") as Material;
		lines = new List<VectorLine>();
		GP = new GeoParser();

	}

	public void SetGeoList(Dictionary<string, List<Vector2>> list)
	{

		foreach (KeyValuePair<string, List<Vector2>> point in list)
		{

			List<Vector3> vertexes = new List<Vector3>();
			List<Vector2> latlon =  (List<Vector2>)point.Value;

			for (int i = 0; i < latlon.Count; i = i + 5)
			{
				Vector3 xyzpoint = XYZfromLatLon(latlon[i].y, latlon[i].x);
				vertexes.Add(xyzpoint);
			}

			VectorLine line = new VectorLine(point.Key, vertexes, lineMaterial.GetTexture(0), lineThickness, LineType.Continuous, Joins.Weld);
			line.material = lineMaterial;
			line.Draw3D();
			lines.Add(line);
		}
	}

	public void SetCityList(Dictionary<string, Vector2> list)
	{

		foreach (KeyValuePair<string, Vector2> point in list)
		{


			Vector3 xyzcity = XYZfromLatLon(point.Value.y, point.Value.x);
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
			obj.transform.position = xyzcity;
			obj.GetComponent<Renderer>().material.color = Color.red;
			obj.name = point.Key;


		}
	}

	public Vector3 XYZfromLatLon(float lat, float lon)
	{
		float latRad = Mathf.Deg2Rad * (lat);
		float lonRad = Mathf.Deg2Rad * (lon);

		float x = distanceToCenter * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
		float y = distanceToCenter * Mathf.Cos(latRad) * Mathf.Sin(lonRad);
		float z = distanceToCenter * Mathf.Sin(latRad);

		return new Vector3 (x, z, y);

	}

	public void GetTestHTML()
	{
		HTMLInterface html = new HTMLInterface();
		List<List<W3Object>> districtData = html.getLists(new Dictionary<string, string>() , "sample-data");
		List<List<W3Object>> districtData2 = html.getLists(new Dictionary<string, string>() , "example_humanitarian");
		DrawTowers(districtData, Color.red, 1, 1f, 1f);
		DrawTowers(districtData2, Color.blue, 2, 0.3f,0.8f);
	}

	public void DrawTowers(List<List<W3Object>> objects, Color clr, int type, float scale, float edgeScale)
	{
		int index = 0;
		int number = 0;
		foreach (W3Object listObj in objects[0])
		{
			//Debug.Log(number++);
			if (listObj.GetType() == typeof(W3Number))
			{
				W3Number ObjNumber = (W3Number)listObj;
				float value = (float)ObjNumber.value + 1;

				GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
				obj.transform.localScale = new Vector3(towerEdgeSize*edgeScale, towerEdgeSize*edgeScale, towerEdgeSize * towerHeigthScale * scale * (float)value);
				obj.GetComponent<Renderer>().material.color = clr;
				obj.name = "tower";

				obj.transform.position = XYZfromLatLon((float)ObjNumber.latitude, (float)ObjNumber.longitude);
				obj.transform.LookAt(this.transform);
				Tower twr =  obj.AddComponent<Tower>() as Tower;
				twr.SetW3(ObjNumber);
				twr.SetColor(clr);
				twr.SetType(type);
			}

		}

	}



	void Update ()
	{
		if (Input.GetKeyUp("t"))
		{
			SetGeoList(GP.getDistricts());
		}

		if (Input.GetKeyUp("u"))
		{
			SetCityList(GP.getCities());
		}

		if (Input.GetKeyUp("i"))
		{
			GetTestHTML();
		}

		for (int a = 0; a < lines.Count; a++)
		{
			lines[a].Draw3D();
		}

	}
}
