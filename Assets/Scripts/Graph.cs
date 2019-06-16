using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    GameObject vertex_prefab;

    [SerializeField]
    jsonManager jManager;

    [SerializeField]
    List<Vertex> vertices;

    [SerializeField]
    float[] distance;
    [SerializeField]
    int[] prev;
    [SerializeField]
    int[] visited;

    [SerializeField]
    int id;
    [SerializeField]
    int index;
    [SerializeField]
    int ini;


    [SerializeField]
    string[] previous;

    int count;
    int shortest = -1;

    void Start()
    {
        vertices = new List<Vertex>();
        jManager = GameObject.Find("JSONManager").GetComponent<jsonManager>();
        jManager.ReadJsonData(id);
    }

    public void InsertVertex(string name, string coordinates, JSONNode verticesID)
    {
        float[] floatData = Array.ConvertAll(coordinates.Split(','), float.Parse);

        GameObject vertex = Instantiate(vertex_prefab, new Vector3(0, 0, 0), Quaternion.identity);
 
        vertex.GetComponent<Vertex>().InitializeData(name, count, floatData, verticesID);

        vertices.Add(vertex.GetComponent<Vertex>());

        count++;
    }

    public void InitializeArrays()
    {
        previous = new string[count];
        distance = new float[count];
        prev = new int[count];
        visited = new int[count];

        ini = 0;

        ShortestPath();
    }

    void ShortestPath()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            previous[i] = "";
            prev[i] = 0;
            distance[i] = -1;
            visited[i] = 0;
        }

        while (count > 0)
        {
            int shortest = SearchShortestDistance();

            distance[ini] = 0;

            Debug.Log(vertices[shortest].getName());

            if (shortest == -1)
                break;

            visited[shortest] = 1;
            count--;

            string[] neighbors = vertices[shortest].getVertices();

            for (int u = 0; u < neighbors.Length; u++)
            {
                Vertex neighbor = FindNeighborbyName(neighbors[u]);

                if (distance[neighbor.getID()] < 0)
                {
                    distance[neighbor.getID()] = distance[shortest] + Vector2.Distance(vertices[shortest].getCoordinates(), neighbor.getCoordinates());

                    previous[neighbor.getID()] = vertices[shortest].getName();
                    prev[neighbor.getID()] = vertices[shortest].getID();
                }
                else
                {
                    var dist = Vector2.Distance(vertices[shortest].getCoordinates(), neighbor.getCoordinates());

                    if (distance[neighbor.getID()] > distance[shortest] + dist)
                    {
                        distance[neighbor.getID()] = distance[shortest] + Vector2.Distance(vertices[shortest].getCoordinates(), neighbor.getCoordinates());
                        previous[neighbor.getID()] = vertices[shortest].getName();
                        prev[neighbor.getID()] = vertices[shortest].getID();
                    }
                }
            }
        }
    }

    int SearchShortestDistance()
    {
        int first = 1;
        int shortest = 0;
        for (int i = 0; i < vertices.Count; i++)
        {
            if (distance[i] >= 0 && visited[i] == 0)
            {
                if (first == 1)
                {
                    shortest = i;
                    first = 0;
                    visited[0] = 1;
                }
                else
                {
                    if (distance[shortest] > distance[i])
                        shortest = i;
                }
            }
        }
        return shortest;
    }

    Vertex FindNeighborbyName(string name)
    {
        foreach (Vertex vertex in vertices)
        {
            if (vertex.getName() == name)
                return vertex;
        }

        return null;
    }

}
