using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;





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
        var myass = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == "UnityEngine.UI").FirstOrDefault();
        var myasstypes = myass.GetTypes();
        foreach (var prop in myasstypes)
        {
            if(new[]{"text", "image", "button", "inputfield", "toggle", "slider"}.Contains(prop.Name.ToLower()))
                availablePropTypes.Add(prop.Name);
        }
        b2 = availablePropTypes.ToArray();



        _propertyChoices = availableProperties.ToArray();
        if(_propertyChoices.Length > 0)
        {
            _propertyChoiceIndex = EditorGUILayout.Popup("Selected Property", targetClass.PropertyIndex, _propertyChoices);
            



            var somethings = GameObject.FindObjectOfType<UnityEngine.UI.Text>();

            foreach(var something in somethings.GetComponents<UnityEngine.Component>())
            {
                var type = something.GetType();
            }
            // Update the selected choice in the underlying object
            targetClass.Property = _propertyChoices[_propertyChoiceIndex];

            targetClass.PropertyIndex = _propertyChoiceIndex;
        }

        _bindingTypeIndex = EditorGUILayout.Popup("Property UI Type", targetClass.TypeIndex, b2);
        targetClass.Type = b2[_bindingTypeIndex];
        targetClass.TypeIndex = _bindingTypeIndex;




        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}

