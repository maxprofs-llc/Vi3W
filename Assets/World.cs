using Assets;
using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
    private Vector2 Center;
    public float lat;
    public float lon;
    public int Zoom;
    public GameObject tile;

    void Start ()
    {
        var go = new GameObject("tile");
        var tile = go.AddComponent<Tile>();
        Center = Deg2num(lat, lon, Zoom);
        StartCoroutine(tile.CreateTile(this, new Vector2(Center.x, Center.y), new Vector2(0, 0), Zoom));
        StartCoroutine(GetTileTexture(new Vector2(Center.x, Center.y), new Vector2(0, 0), Zoom));
    }

    public Vector2 Deg2num(float lat_deg, float lon_deg, int zoom)
    {
        float lat_rad = Mathf.Deg2Rad * lat_deg;
        float n = Mathf.Pow(2.0f, zoom);
        int xtile = (int)((lon_deg + 180.0f) / 360.0f * n);
        int ytile = (int)((1.0 - Mathf.Log(Mathf.Tan(lat_rad) + (1 / Mathf.Cos(lat_rad))) / Mathf.PI) / 2.0 * n);
        return new Vector2(xtile, ytile);
    }

    void Update ()
    {

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 30), Center.x.ToString());
        GUI.Label(new Rect(10, 50, 500, 30), Center.y.ToString());
    }

    /*void GetTileTexture()
    {
        renderer.material.mainTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
        while (true) {
            var www = new WWW(url);
            yield www;
            www.LoadImageIntoTexture(tile.renderer.material.mainTexture);
        }
    }*/

    public IEnumerator GetTileTexture(Vector2 realPos, Vector2 worldCenter, int zoom)
    {
        var tilename = realPos.x + "_" + realPos.y;
        var tileurl = realPos.x + "/" + realPos.y;        
        var url = "http://a.tile.opencyclemap.org/cycle/" + zoom + "/" + tileurl + ".png";       

        
            var www = new WWW(url);
            yield return www;

            //www.texture((Texture2D)tile.GetComponent<Renderer>().material.mainTexture);
            tile.GetComponent<Renderer>().material.mainTexture =  www.texture;
    }
}
