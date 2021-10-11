using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = WorldInfo.Color;

public class UIBug : MonoBehaviour
{
    public static UIBug instance;
    public Text[] texts = new Text[4];
    public Image[] images = new Image[4];
    private Image selected;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        selected = images[0];

        //r topright
        //b bottomleft
        //y topleft
        //g bottomright

        foreach (Image image in images)
        {
            if (image != selected)
                image.enabled = false;
        }

        for (int i = 0; i < 4; i++)
            texts[i].text = SceneStuff.instance.colorNums[i].ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adjust the Bug UI image or the associated color count using true/false respectively
    /// </summary>
    /// <param name="image"></param>
    public void ColorStuff(bool image, int adjustment = -1, Color color = Color.Red)
    {
        if (image)
        {
            selected.enabled = false;

            switch (SceneStuff.selectedColor)
            {
                case (Color)0:
                    selected = images[0];
                    break;
                case (Color)1:
                    selected = images[1];
                    break;
                case (Color)2:
                    selected = images[2];
                    break;
                case (Color)3:
                    selected = images[3];
                    break;
            }

            selected.enabled = true;
        }
        else
        {
            int textInt = 0;

            switch (color)
            {
                case (Color)0:
                    textInt = int.Parse(texts[0].text);
                    textInt += adjustment;
                    texts[0].text = textInt.ToString();
                    break;
                case (Color)1:
                    textInt = int.Parse(texts[1].text);
                    textInt += adjustment;
                    texts[1].text = textInt.ToString();
                    break;
                case (Color)2:
                    textInt = int.Parse(texts[2].text);
                    textInt += adjustment;
                    texts[2].text = textInt.ToString();
                    break;
                case (Color)3:
                    textInt = int.Parse(texts[3].text);
                    textInt += adjustment;
                    texts[3].text = textInt.ToString();
                    break;
            }
        }
    }
}
