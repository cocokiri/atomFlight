using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkingUp : MonoBehaviour {

    public Transform from;
    public Transform to;

    public void LinkTo(Transform linkToMe)
    {
        linkToMe.GetComponent<LinkingUp>().from = transform;
        to = linkToMe;


        if (to && transform.parent.parent)
        {
            if (transform.tag != "RayLocation")
            {
                Debug.Log(transform.parent.parent);
                Debug.Log(to);
                
                if (to != null && to.tag != "RayLocation")
                {
                    if (transform.parent.parent.GetInstanceID() != to.parent.parent.GetInstanceID())
                    {
                        transform.parent.parent.GetComponent<Atom>().AtomConnect(to.parent.parent);

                    }
                }
             
            }



        }
    }
}
