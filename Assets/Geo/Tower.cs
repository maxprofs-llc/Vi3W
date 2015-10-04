using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
	public Renderer rend;

	private string toolTipText = "Bananas"; // set this in the Inspector

	private string currentToolTipText = "";
	private GUIStyle guiStyleFore;
	private GUIStyle guiStyleBack;
	private W3Number W3 = null;



	void Start()
	{
		rend = GetComponent<Renderer>();

		guiStyleFore = new GUIStyle();
		guiStyleFore.normal.textColor = Color.white;
		guiStyleFore.alignment = TextAnchor.UpperCenter ;
		guiStyleFore.wordWrap = true;
		guiStyleBack = new GUIStyle();
		guiStyleBack.normal.textColor = Color.black;
		guiStyleBack.alignment = TextAnchor.UpperCenter ;
		guiStyleBack.wordWrap = true;
	}

	public void SetW3(W3Number W3n)
	{
		W3 = W3n;

		string tooltip = "Name: " + (string)W3.name;
		tooltip += "\n" + "Type: " + (string)W3.type;
		tooltip += "\n" + "Tag: " + (string)W3.tag;
		tooltip += "\n" + "Indicator: " + (string)(W3.indicator).ToString();
		tooltip += "\n" + "\n" + "Latitude: " + (string)W3.latitude.ToString();
		tooltip += "\n" + "Longitude: " + (string)W3.longitude.ToString();

		toolTipText = tooltip;
	}
	void OnMouseEnter()
	{
		rend.material.color = Color.green;
		currentToolTipText = toolTipText;
	}
	void OnMouseOver()
	{
		rend.material.color -= new Color(0, 0.1f, 0) * Time.deltaTime;
	}
	void OnMouseExit()
	{
		rend.material.color = Color.red;
		currentToolTipText = "";
	}

	void OnMouseDown()
	{
		Application.LoadLevel(Application.loadedLevel + 1);	
	}

	void OnGUI()
	{
		if (currentToolTipText != "")
		{
			var skinStyle = GUI.skin.GetStyle("Label");
			//centeredStyle.alignment = TextAnchor.UpperCenter;
			skinStyle.alignment = TextAnchor.MiddleLeft;
			var x = Event.current.mousePosition.x;
			var y = Event.current.mousePosition.y;
			GUI.Label (new Rect (x - 149, y + 21, 300, 60), currentToolTipText, guiStyleBack);
			GUI.Label (new Rect (x - 150, y + 20, 300, 60), currentToolTipText, guiStyleFore);
		}
	}










}