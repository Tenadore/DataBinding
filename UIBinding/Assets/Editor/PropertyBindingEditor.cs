using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(PropertyBinding))]
public class PropertyBindingEditor : Editor
{
    string[] _classChoices;
    int _classChoiceIndex;
    string[] _propertyChoices;
    int _propertyChoiceIndex;
    string[] _bindingTypes = new[] { "Text" };
    string[] b2;
    int _bindingTypeIndex;

    string[] _fieldChoices;
    int _fieldChoiceIndex;
    PropertyInfo[] _fieldType;

    readonly string[] _acceptedFields = new[] { "text", "color", "fontsize", "fontstyle", "font", "supportrichtext", "resizetextforbestfit", "resizetextminsize", "resizetextmaxsize",
        "alignment", "alignbygeometry", "horizontaloverflow", "verticaloverflow", "linespacing", "raycasttarget", "enabled", "tag", "name", "material"};






    public override void OnInspectorGUI()
    {

        //PropertyDescriptorCollection properties;
        //GameObject obj = new GameObject();

        //properties = TypeDescriptor.GetProperties(typeof(GameObject));
        //Console.WriteLine(properties[0].Attributes.Count); // Prints X

        //TypeDescriptor.CreateProperty(typeof(System.Int32), "asdf", typeof(System.Int32), null);

        //obj.as


        //properties = TypeDescriptor.GetProperties(typeof(GameObject));
        //Console.WriteLine(properties[0].Attributes.Count); // Prints X+1



        
        var targetClass = target as PropertyBinding;

        var availableTypes = Assembly.GetExecutingAssembly().GetTypes();
        List<string> classchoiceList = new List<string>();

        var myAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == "Assembly-CSharp").FirstOrDefault();

        var types = myAssembly.GetTypes();
        foreach (var atype in types)
        {
            if (!string.IsNullOrEmpty(atype.ToString()))
                if(atype.GetProperties().ToList().Count > 0 && !atype.Name.Contains("Singleton"))
                    classchoiceList.Add(atype.Name);

        }
        _classChoices = classchoiceList.ToArray();
        // Draw the default inspector



        //BUTTON

            DrawDefaultInspector();


        _classChoiceIndex = EditorGUILayout.Popup("Selected Class", targetClass.ClassIndex, _classChoices);
        // Update the selected choice in the underlying object
        
        targetClass.Class = _classChoices[_classChoiceIndex];
        targetClass.ClassIndex = _classChoiceIndex;


        //once this happens we need to update the property options
        var selectedClass = types.Where(x => x.Name == targetClass.Class).FirstOrDefault();
        List<string> availableProperties = new List<string>();
        foreach (var prop in selectedClass.GetProperties())
        {
            if(prop.DeclaringType.Namespace != "UnityEngine")
            {
                availableProperties.Add(prop.Name);
            }
        }


        List<string> availablePropTypes = new List<string>();
        List<Type> availablePropTypes2 = new List<Type>();
        var myass = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == "UnityEngine.UI").FirstOrDefault();
        var myasstypes = myass.GetTypes();
        
        foreach (var prop in myasstypes)
        {

            if (new[] { "text", "image", "button", "inputfield", "toggle", "slider" }.Contains(prop.Name.ToLower())) {
                availablePropTypes.Add(prop.Name);
                availablePropTypes2.Add(prop);
            }


        }
        b2 = availablePropTypes.ToArray();





            _propertyChoices = availableProperties.ToArray();
        if(_propertyChoices.Length > 0)
        {
            _propertyChoiceIndex = EditorGUILayout.Popup("Selected Property", targetClass.PropertyIndex, _propertyChoices);









            // Update the selected choice in the underlying object
            targetClass.Property = _propertyChoices[_propertyChoiceIndex];

            targetClass.PropertyIndex = _propertyChoiceIndex;
        }

        _bindingTypeIndex = EditorGUILayout.Popup("Property UI Type", targetClass.TypeIndex, b2);
        targetClass.Type = b2[_bindingTypeIndex];
        targetClass.TypeIndex = _bindingTypeIndex;



        //PROPERTY FIELDS

        if (availablePropTypes.Count > 0)
        {
            List<string> availablePropFields = new List<string>();
            Type type = availablePropTypes2[_bindingTypeIndex];
            var myFieldInfo = type.GetProperties().Where(x => x.CanWrite);
            foreach (var info in myFieldInfo)
            {
                if (_acceptedFields.Contains(info.Name.ToLower()))                
                    availablePropFields.Add(info.Name);
            }
            _fieldChoices = availablePropFields.ToArray();
            if (_fieldChoices.Length > 0)
            {
                if (targetClass.FieldIndex > _fieldChoices.Length)
                {
                    targetClass.FieldIndex = 0;
                }
                _fieldChoiceIndex = EditorGUILayout.Popup("Selected Field", targetClass.FieldIndex, _fieldChoices);

            }

            targetClass.Field = _fieldChoices[_fieldChoiceIndex];
            targetClass.FieldIndex = _fieldChoiceIndex;
        }




        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}

