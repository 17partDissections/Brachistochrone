using System.Collections.Generic;
using System;

public class SceneGrid
{
    private Dictionary<string, Node> Dictionary = new Dictionary<string, Node>();   
    
    public void CreateNode(int x, int z)
    {
        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < z; j++)
            {
                Dictionary.Add($"x{i}z{j}", new Node());
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodeKey"></param>
    public Node GetNode(string nodeKey)
    {
        if (Dictionary.ContainsKey(nodeKey)) { UnityEngine.Debug.Log(nodeKey); return Dictionary[nodeKey]; }
        else throw new Exception($"Dictionary doesnt contain {nodeKey}");
    }
}
public class Node { public bool IsEmpty; }
