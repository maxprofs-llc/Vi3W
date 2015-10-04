using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;


public class GeoDrawer : MonoBehaviour
{
	private Material lineMaterial;
	private List<VectorLine> lines;
	private float lineThickness = 4f;
	private float distanceToCenter = 11f;
	private GeoParser GP;


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

			for (int i = 0; i < latlon.Count; i=i+5)
			{
				Vector3 xyzpoint = XYZfromLatLon(latlon[i].y, latlon[i].x);
				vertexes.Add(xyzpoint);
			}

			VectorLine line = new VectorLine(point.Key, vertexes, lineMaterial.GetTexture(0), lineThickness, LineType.Continuous, Joins.Weld);
			line.material = lineMaterial;
			line.Draw();
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

		for(int a = 0; a<lines.Count; a++)
		{
			lines[a].Draw();
		}

	}
}
