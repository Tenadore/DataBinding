using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

public static class BindingController
{

    /// <summary>
    /// Get a list of all elements that have binding.
    /// </summary>
    /// <param name="layer">The layer to get elements from.</param>
    /// <returns>A list of elements with binding</returns>
    public static List<GameObject> FindBoundObjectsInLayer(int layer = 5)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> objectsInLayer = new List<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer && obj.GetComponent<PropertyBinding>())
            {
                objectsInLayer.Add(obj);
            }
        }
        return (objectsInLayer.Count == 0 ? null : objectsInLayer);
    }
    /// <summary>
    /// Get a list of elements bound to a certain property
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static List<GameObject> GetElementsByProperty(string propertyName) {

        List<GameObject> subscribedElements = FindBoundObjectsInLayer().Where(x => x.GetComponent<PropertyBinding>().Property == propertyName).ToList();
        return subscribedElements.Count == 0 ? null : subscribedElements;

    }

}
