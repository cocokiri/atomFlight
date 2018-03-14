using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinkManager : MonoBehaviour {
    // Use this for initialization

    public KeepLink keep;
    List<int> createdLinks = new List<int>();
	void Start () {
		
	}

    GameObject[] AllElectrons ()
    {
        GameObject[] allEles = GameObject.FindGameObjectsWithTag("Electron");
        return allEles;
    }

    public void SetLinks(bool unlinkFromPlayer = false)
    {
        foreach (GameObject g in AllElectrons())
        {
            LinkingUp elescript = g.GetComponent<LinkingUp>();
            if (elescript.to != null && (!createdLinks.Contains(g.GetInstanceID()) || elescript.to.tag=="RayLocation"))
            {
                KeepLink kl = Instantiate<KeepLink>(keep);
                kl.transform.parent = transform;

                createdLinks.Add(g.GetInstanceID());


                kl.Generate(elescript.transform, elescript.to);

            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            KeepLink link = transform.GetChild(i).GetComponent<KeepLink>();
            if (link.myTo.tag == "RayLocation" && unlinkFromPlayer)
            {
                link.Cancel();
            }
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
