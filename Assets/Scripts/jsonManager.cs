using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using SimpleJSON;
public class jsonManager : MonoBehaviour
{
    [SerializeField]
    Graph graph;


    [SerializeField]
    string[] jsonPaths;
    string jsonString;
    string path;

    void Start()
    {
        graph = FindObjectOfType<Graph>();
    }
    public void ReadJsonData(int id)
    {
        path = "C:/Users/mgesc/GraphNavigation/Assets/" + jsonPaths[id];
        jsonString = File.ReadAllText(path);
        var data = JSON.Parse(jsonString);

        int count = 0;
        while (data[count] != null)
        {
            graph.InsertVertex(data[count]["Name"], data[count]["Coordenates"], data[count]["Vertices"]);
            //Debug.Log(count);
            //Debug.Log(data[count]);
            count++;
        }

        graph.InitializeArrays();

    }
}