using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ToolInfo
{
    public string toolName;
    public int toolCount;
    public int toolType;

    public ToolInfo(string name, int count, int type)
    {
        toolName = name;
        toolCount = count;
        toolType = type;
    }
}
