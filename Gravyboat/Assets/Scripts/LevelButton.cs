using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string level;
    public int index;
    public Color dull;
    public Color highlighted;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = dull;
        if (PlayerPrefs.HasKey("MaxLevel"))
        {
            if (PlayerPrefs.GetInt("MaxLevel") >= index)
                text.color = highlighted;
        }
    }

    public void Click()
    {
        if (text.color == highlighted)
        {
            MainMenu.instance.StartCoroutine(MainMenu.instance.Load(level));
        }
    }
}
