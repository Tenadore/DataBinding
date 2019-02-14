using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewBehaviourScript : MonoBehaviour {



    [SerializeField]
    [StringInList(typeof(PropertyDrawersHelper), "SomeProps")] public List<string> SomeProperties;


    public static class PropertyDrawersHelper
    {
        public static List<string> SomeProps()
        {
            var temp = new List<string>();
            foreach (string s in new[] { "Some Prop 1", "Some Prop 2", "Some Prop 3" })
            {
                string name = s;
                temp.Add(name);

            }
            return temp;
        }

    }
}
