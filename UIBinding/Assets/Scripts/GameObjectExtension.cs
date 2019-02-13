using System.Collections.Generic;
using UnityEngine;


public static class AdditionalGameObjectProperties {

    public static void Something(this GameObject go, List<int> intList) {
        foreach (var item in intList) 
        {
            Debug.Log(item.ToString());
        }
    }

}
//GameObject is the type
//gameObject is used to access a specific gameObject