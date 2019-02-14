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
    //public List<GameObject> boundUIElements = new List<GameObject>();
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
    private Color newColor;
    public Color NewColor
    {
        get
        {
            return newColor;
        }
        set
        {
            if (newColor == value) return;
            newColor = value;
            OnPropertyChanged("NewColor");


        }
    }

    private FontStyle newfontStyle;
    public FontStyle NewFontStyle
    {
        get
        {
            return newfontStyle;
        }
        set
        {
            if (newfontStyle == value) return;
            newfontStyle = value;
            OnPropertyChanged("NewFontStyle");


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
                        UpdateTextComponent(bindingEl, uivalue);
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
        //if(inputString.Contains("{{bound}}"))
            //return inputString.Replace("{{bound}}", value);
        return "Object value : " + value;
    }

    /// <summary>
    /// Update a property of the Text Componenent.
    /// </summary>
    /// <param name="targetElement">The element to update</param>
    /// <param name="uivalue">The new value</param>
    public void UpdateTextComponent(PropertyBinding targetElement, object uivalue) {
        Text textComponent = targetElement.GetComponent<Text>();
        switch (targetElement.Field.ToLower())
        {
            case "text":
                try
                {
                    if (string.IsNullOrEmpty(uivalue.ToString()))
                        Debug.LogError("Warning! String is null or empty");
                    textComponent.text = replaceBoundStringElement(textComponent.text, uivalue.ToString());
                }
                catch (Exception e)
                {

                    Debug.LogError(e.ToString());
                }
                break;
            case "color":
                try
                {
                    textComponent.color = (Color)uivalue;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
                break;
            case "fontsize":
                try
                {
                    textComponent.fontSize = (int)uivalue;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
                break;
            case "fontstyle":
                try
                {
                    if ((int)uivalue > 3)
                        Debug.LogError("Font style does not exist! Set with the (FontStyle) enum instead of (int).");
                    textComponent.fontStyle = (FontStyle)uivalue;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
                break;
            default:
                break;
        }
    }

}