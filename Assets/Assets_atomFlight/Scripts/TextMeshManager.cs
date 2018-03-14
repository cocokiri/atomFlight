using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshManager : MonoBehaviour {


    TextMeshPro textMeshPro;	
    // Use this for initialization
	void Start () {
        textMeshPro = gameObject.GetComponent<TextMeshPro>();
        textMeshPro.text = "<u>9x<sup><#00ff00>3</color></sup></u>";
    }

    public void DisplayText(string text)
    {
        textMeshPro.text = text;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
