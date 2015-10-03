using UnityEngine;
using System.Collections;

public class ObjectonGlobe : MonoBehaviour {


	public float lat_degree = 0f;
	public float lon_degree = 0f;

	public GameObject obj;
	public float distanceToCenter = 22f;

	public float towerHeight = 2f;
	public float towerEdge = 1f;
	
	void Start () 
	{
		obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		obj.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
		obj.GetComponent<Renderer>().material.color = Color.red; 
		obj.name = "tower";

	
	}

	public Vector3 CalculatePosition(float lat, float lon)
	{
		float latRad = Mathf.Deg2Rad * (lat);
		float lonRad = Mathf.Deg2Rad * (lon);

		float x = distanceToCenter * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
		float y = distanceToCenter * Mathf.Cos(latRad) * Mathf.Sin(lonRad);
		float z = distanceToCenter * Mathf.Sin(latRad);

		return new Vector3 (x,z,y);

	}
	
	
	void Update () 
	{
		obj.transform.position = CalculatePosition(lat_degree, lon_degree);
		obj.transform.LookAt(this.transform);

		obj.transform.localScale = new Vector3(0.3f*towerEdge,0.3f*towerEdge ,0.3f*towerHeight);
	
	}
}
