using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Linq;
using UnityEngine.UI;
using System;

public class Core : MonoBehaviour, INotifyPropertyChanged
{

    public List<GameObject> boundUIElements = new List<GameObject>();
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
        try
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void GetBoundUiElements()
    {
        GameObject[] elements = GameObject.FindObjectsOfType<GameObject>();
        foreach (var element in elements)
        {
            if (element.GetComponent<PropertyBinding>())
            {
                var binding = element.GetComponent<PropertyBinding>();
                if (binding.Class == this.GetType().ToString())
                {
                    boundUIElements.Add(element);
                }
            }
        };
    }

    public void UpdateUI(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var elementsToUpdate = this.boundUIElements.Where(x => x.GetComponent<PropertyBinding>().Property == e.PropertyName).ToList();
        if (elementsToUpdate.Count() > 0)
        {
            var uivalue = sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null);
            foreach (var element in elementsToUpdate)
            {
                var bindingEl = element.GetComponent<PropertyBinding>();
                switch (bindingEl.Type)
                {
                    case "Text":
                        element.GetComponent<Text>().text = replaceBoundStringElement(element.GetComponent<Text>().text, uivalue.ToString());
                        break;
                    case "Image":
                        Sprite sprite = uivalue as Sprite;
                        element.GetComponent<Image>().sprite = sprite;
                        break;
                    default:
                        break;
                }


            }
        }

    }
    public static string replaceBoundStringElement(string inputString, string value)
    {
        //if(inputString.Contains("{{bound}}"))
            //return inputString.Replace("{{bound}}", value);
        return "Object value : " + value;
    }

}