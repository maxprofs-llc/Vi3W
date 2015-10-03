using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Helpers;
using UnityEngine;
using Vectrosity;

namespace Assets
{
public enum RoadType
{
    Highway,
    MajorRoad,
    MinorRoad,
    Rail,
    Path
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
internal class RoadPolygon : MonoBehaviour
{
    private Tile _tile;
    public string Id { get; set; }
    public RoadType Type { get; set; }
    private List<Vector3> _verts;
    private float _thickness = 8f;
    VectorLine line;
    Material lineMaterial;

    public void Initialize(string id, Tile tile, List<Vector3> verts, string halfWidth)
    {
        Id = id;
        _tile = tile;
        Type = halfWidth.ToRoadType();
        _verts = verts;
        lineMaterial = Resources.Load("DefaultLine3D") as Material;

        Draw(_verts);
    }

    private void Update()
    {
        /*for (int i = 1; i < _verts.Count; i++)
        {
            Debug.DrawLine(_tile.transform.position + _verts[i], _tile.transform.position + _verts[i - 1]);
        }*/

        line.Draw3D();



    }



    private void Draw(List<Vector3> vertexes)
    {


        line = new VectorLine("Spline", vertexes, lineMaterial.GetTexture(0), _thickness, LineType.Continuous, Joins.Weld);
        line.material = lineMaterial;
        //line = new VectorLine("Spline", vertexes, _thickness);
        //line.SetColor(Color.red);

    }

    private void OnPostRender()
    {
        Draw(_verts);

    }
}

}
