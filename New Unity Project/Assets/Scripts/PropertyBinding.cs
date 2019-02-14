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
    public int PropertyIndex;
    [HideInInspector]
    public int ClassIndex;
    [HideInInspector]
    public int TypeIndex;



    private void Start()
    {
        //PropertyChanged += UpdateUI;
        //GetBoundUiElements();

    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) { Number = 50; }
    }




}
