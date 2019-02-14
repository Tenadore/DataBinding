using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : Core {

    private int number3;
    public int Number3
    {
        get
        {
            return number3;
        }
        set
        {
            if (number3 == value) return;
            number3 = value;
            OnPropertyChanged("Number3");


        }
    }
    // Use this for initialization
    private void Awake()
    {

    }
    // Update is called once per frame
    private void Start()
    {
        Number++;
        Number2 = 50;
        Number3 = 100;
    }

}
