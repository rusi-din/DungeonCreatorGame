using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class NodeTagChanger : MonoBehaviour
{


    public TMPro.TMP_Dropdown nodeTypesDropdown;
    public static TMPro.TMP_Dropdown nodeTypes;

    public class NodeTags
    {
        public string name;
        public int id;

        public NodeTags(int num, string text)
        {
            id = num;
            name = text;
        }



        public override string ToString()
        {
            return name;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodeTypes = nodeTypesDropdown;

        nodeTypes.ClearOptions();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Node"),
            new TMP_Dropdown.OptionData("Start Point"),
            new TMP_Dropdown.OptionData("End Point"),
            new TMP_Dropdown.OptionData("Route")
        };

        nodeTypes.AddOptions(options);
    }


    public static NodeTags getNodeSectedType()
    {
        switch (nodeTypes.value)
        {
            case 0:
                return getTag(1);
            case 1:
                return getTag(2);
            case 2:
                return getTag(3);
            case 3:
                return getTag(4);
            default:
                return new NodeTags(1, "node");

        }
    }

    public static NodeTags getTag(int num)
    {
        switch (num)
        {
            case 1:
                return new NodeTags(1, "node");
            case 2:
                return new NodeTags(2, "StartPoint");
            case 3:
                return new NodeTags(3, "EndPoint");
            case 4:
                return new NodeTags(4, "Route");
            default:
                return getTag(1);
        }
    }
}
