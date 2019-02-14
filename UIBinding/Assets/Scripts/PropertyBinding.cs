using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class PropertyBinding : Core
{
    [HideInInspector]
    public string Class;
    [HideInInspector]
    public string Property;
    [HideInInspector]
    public string Type;
    [HideInInspector]
    public string Field;

    [HideInInspector]
    public int PropertyIndex;
    [HideInInspector]
    public int ClassIndex;
    [HideInInspector]
    public int TypeIndex;
    [HideInInspector]
    public int FieldIndex;

 

}