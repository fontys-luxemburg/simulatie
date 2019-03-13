using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class OSMVisualizer : MonoBehaviour
{

    XmlDocument doc = new XmlDocument();
    List<Transform> wayObjects = new List<Transform>();
    //public Node n;
    public float x;
    public float y;
    public float boundsX = 34;
    public float boundsY = -118;
    public string fileName;
    [SerializeField]
    private float lineWidth = 0.03f;

    public struct Node
    {

        public long id;
        public float lat, lon;

        public Node(long ID, float LAT, float LON)
        {
            id = ID;
            lat = LAT;
            lon = LON;
            Debug.Log("ID: " + id + ", LAT: " + lat + ", LON: " + lon);
        }
    }

    public struct Way
    {
        public long id;
        public List<long> wnodes;

        public Way(long ID)
        {
            id = ID;
            wnodes = new List<long>();
        }
    }

    public List<Node> nodes = new List<Node>();
    public List<Way> ways = new List<Way>();

    void Start()
    {
        doc.Load(new XmlTextReader("Assets/OSM/" + fileName + ".osm"));
        XmlNodeList elemList = doc.GetElementsByTagName("node");
        foreach (XmlNode attr in elemList)
        {
            nodes.Add(new Node(long.Parse(attr.Attributes["id"].InnerText), float.Parse(attr.Attributes["lat"].InnerText), float.Parse(attr.Attributes["lon"].InnerText)));
        }

        XmlNodeList wayList = doc.GetElementsByTagName("way");
        int ct = 0;
        foreach (XmlNode node in wayList)
        {

            XmlNodeList wayNodes = node.ChildNodes;
            ways.Add(new Way(int.Parse(node.Attributes["id"].InnerText)));
            foreach (XmlNode nd in wayNodes)
            {
                if (nd.Attributes[0].Name == "ref")
                {
                    ways[ct].wnodes.Add(long.Parse(nd.Attributes["ref"].InnerText));
                    Debug.Log(ways[ct].wnodes.Count);
                }
            }
            ct++;
        }
        for (int i = 0; i < ways.Count; i++)
        {
            wayObjects.Add(new GameObject("wayObject" + ways[i].id).transform);
            wayObjects[i].gameObject.AddComponent<LineRenderer>();
            LineRenderer lineRenderer = wayObjects[i].GetComponent<LineRenderer>();
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = ways[i].wnodes.Count;
            for (int j = 0; j < ways[i].wnodes.Count; j++)
            {

                foreach (Node nod in nodes)
                {
                    if (nod.id == ways[i].wnodes[j])
                    {
                        Debug.Log("MATCH!");
                        x = nod.lat;
                        y = nod.lon;
                    }
                }
                lineRenderer.SetPosition(j, new Vector3((x - boundsX) * 100, 0, (y - boundsY) * 100));
            }
        }

    }
}
