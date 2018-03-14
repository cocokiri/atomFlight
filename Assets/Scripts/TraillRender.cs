using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraillRender : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("TRIGGER");
        Debug.Log(col.gameObject.name);

        Debug.Log(FindParentWithTag(col.gameObject, "Defined"));

        GameObject Interesting = FindParentWithTag(col.gameObject, "Defined");

        if (Interesting != null)
        {

            GameObject tmp = GameObject.Find("TextMeshTip");
            tmp.GetComponent<TextMeshManager>().DisplayText(Interesting.GetComponent<Atom>().textMeshName);
            //HOW TO make <ATOM> general? So it works for <Molecule>, <Cell>

            //tmp.GetComponent<TextMeshManager>().DisplayText("yoyl");
        }
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        if (childObject.tag == tag)
        {
            return childObject;
        }
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }
}


