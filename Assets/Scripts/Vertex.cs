using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
public class Vertex : MonoBehaviour
{
    [SerializeField]
    int id;

    [SerializeField]
    string letter;

    [SerializeField]
    string[] vertices;

    [SerializeField]
    JSONNode verticesID;

    [SerializeField]
    Text idText;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    float[] coordinates;

    [SerializeField]
    float representation_distance;

    public void InitializeData(string _letter, int _id, float[] _coordinates, JSONNode _vertices)
    {
        canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform);
        letter = _letter;
        id = _id;
        coordinates = _coordinates;
        transform.localPosition = new Vector2(coordinates[0] * representation_distance, coordinates[1] * representation_distance);
        idText.text = letter;
        verticesID = _vertices;
        ConvertJSONNode();
    }

    public JSONNode getVerticesID()
    {
        return verticesID;
    }

    public string[] getVertices()
    {
        return vertices;
    }

    public int getID()
    {
        return id;
    }

    public string getName()
    {
        return letter;
    }

    public Vector2 getCoordinates()
    {
        Vector2 pos = new Vector2(coordinates[0], coordinates[1]);

        return pos;
    }

    void ConvertJSONNode()
    {
        vertices = new string[verticesID.Count];

        for (int i = 0; i < verticesID.Count; i++)
        {
            vertices[i] = verticesID[i];
        }
    }

}

