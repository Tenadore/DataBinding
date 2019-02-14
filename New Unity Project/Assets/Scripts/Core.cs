using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Linq;
using UnityEngine.UI;
using System;

public class Core : MonoBehaviour, INotifyPropertyChanged
{

    public Core(){
        PropertyChanged += UpdateBoundElements;
    }
    private int number;
    public int Number {
        get {
            return number;
        }
        set {
            if (number == value) return;
            number = value;
            OnPropertyChanged("Number");

           
        }
    }

    private int number2;
    public int Number2
    {
        get
        {
            return number2;
        }
        set
        {
            if (number2 == value) return;
            number2 = value;
            OnPropertyChanged("Number2");


        }
    }

    private string s1;
    public string S1
    {
        get
        {
            return s1;
        }
        set
        {
            if (s1 == value) return;
            s1 = value;
            OnPropertyChanged("S1");


        }
    }
    private Sprite sprite1;
    public Sprite Sprite1
    {
        get
        {
            return sprite1;
        }
        set
        {
            if (sprite1 == value) return;
            sprite1 = value;
            OnPropertyChanged("Sprite1");


        }
    }




    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public void UpdateBoundElements(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (BindingController.GetElementsByProperty(e.PropertyName) != null)
        {
            foreach (var boundElement in BindingController.GetElementsByProperty(e.PropertyName))
            {
                var uivalue = sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null);

                var bindingEl = boundElement.GetComponent<PropertyBinding>();
                switch (bindingEl.Type)
                {
                    case "Text":
                        boundElement.GetComponent<Text>().text = replaceBoundStringElement(boundElement.GetComponent<Text>().text, uivalue.ToString());
                        break;
                    case "Image":
                        Sprite sprite = uivalue as Sprite;
                        boundElement.GetComponent<Image>().sprite = sprite;
                        break;
                    default:
                        break;
                }
            } 
        }

    }



    public static string replaceBoundStringElement(string inputString, string value)
    {
        return "Object value : " + value;
    }

}