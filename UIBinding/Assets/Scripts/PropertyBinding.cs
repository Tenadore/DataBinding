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

    public Sprite sprite;



    private void Start()
    {
        BindingController.core.PropertyChanged += BindingController.core.UpdateUI;
        BindingController.core.GetBoundUiElements();
        BindingController.core.Number++;
        BindingController.core.Number2 = 100;
        BindingController.core.S1 = "Awesome";
        BindingController.core.Sprite1 = sprite;
}
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) { Number = 50; }
    }




}
