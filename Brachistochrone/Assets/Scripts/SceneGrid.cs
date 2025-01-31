using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

public class SceneGrid
{
    private Dictionary<string, Node> Dictionary = new Dictionary<string, Node>();
    private int _sceneBorders;
    public SceneGrid([Inject(Id = "SceneBorders")]int SceneBorders)
    {
        _sceneBorders = SceneBorders;
    }
    public void CreateNode(int x, int z)
    {
        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < z; j++)
            {
                Dictionary.Add($"x{i}z{j}", new Node($"x{i}z{j}"));
            }
        }
    }
    public string TranscodeNode(Vector3 vector)
    {
        var x = Mathf.Abs(Mathf.FloorToInt(vector.x / 20));
        var z = Mathf.Abs(Mathf.FloorToInt(vector.z / 20));
        Debug.Log($"x{x}z{z}");
        return ($"x{x}z{z}");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodeKey"></param>
    public bool GetNode(string nodeKey, out Node node)
    {
        if (Dictionary.ContainsKey(nodeKey))
        {
            node = Dictionary[nodeKey];
                return true;
        }
        else throw new Exception($"Dictionary doesnt contain {nodeKey}");
    }
    public List<Directions> GetFreeNeighbours(Node node, out List<Directions> primaryDirections)
    {
        primaryDirections = new List<Directions>();
        string stringX = "";
        string stringZ = "";
        var nodes = new List<Directions>();
        foreach (var stringIndex in node.Key)
        {
            if (stringIndex == 'x') continue;
            else if (stringIndex == 'z') break;           
            else stringX += stringIndex;
        }
        bool flag = false;
        foreach (var stringIndex in node.Key)
        {
            if(stringIndex == 'z') { flag = true; continue; }
            if (flag) stringZ += stringIndex;
        }                                                       //x10,z15
        int x = Int32.Parse(stringX);
        int z = Int32.Parse(stringZ);
        if (z >= 1 && GetNode($"x{x}z{z - 1}", out Node outnodeDownZ))
        {
            if (!outnodeDownZ.IsBusy) nodes.Add(Directions.Down);
            else if(outnodeDownZ.DoorUpBusy) primaryDirections.Add(Directions.Down);
        }
        if ((z + 1) <= _sceneBorders && GetNode($"x{x}z{z + 1}", out Node outnodeUpZ))
        {
            if (!outnodeUpZ.IsBusy) nodes.Add(Directions.Up);
            else if (outnodeUpZ.DoorDownBusy) primaryDirections.Add(Directions.Up);
        }
        if ((x + 1) <= _sceneBorders && GetNode($"x{x + 1}z{z}", out Node outnodeRightX))
        {
            if (!outnodeRightX.IsBusy) nodes.Add(Directions.Right);
            else if (outnodeRightX.DoorLeftBusy) primaryDirections.Add(Directions.Right);
        }
        if (x >= 1 && GetNode($"x{x - 1}z{z}", out Node outnodeLeftX))
        {
            if (!outnodeLeftX.IsBusy) nodes.Add(Directions.Left);
            else if (outnodeLeftX.DoorRightBusy) primaryDirections.Add(Directions.Left);
        }
        return nodes;
    }
    
}

public class Node
{
    public string Key;
    public bool IsBusy = false;
    public bool DoorRightBusy, DoorLeftBusy, DoorUpBusy, DoorDownBusy;
    public Node(string key)
    {
        Key = key;
    }
}
public enum Directions { Up, Right, Down, Left }

