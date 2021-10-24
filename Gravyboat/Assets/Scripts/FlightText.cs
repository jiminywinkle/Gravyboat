using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlightText : MonoBehaviour
{
    private TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Gravyboat is currently flying: " + MainBody.direction.ToString();
    }
}
