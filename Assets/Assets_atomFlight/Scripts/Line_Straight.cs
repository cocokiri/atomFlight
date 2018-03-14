using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line_Straight : MonoBehaviour {

    LineRenderer line;


    public GameObject player;
    public int segments =100;
    Transform other;
    bool connected = false;
    LinkingUp linkup;
    GameObject currentLink;

    void Start()
    {
        //Connect(other);
        line = gameObject.GetComponent<LineRenderer>();

        linkup = GetComponent<LinkingUp>();
    }

    public void CreateLine(Transform a, Transform b)
    {
       
        line.SetVertexCount(segments);
        line.useWorldSpace = true;
        Debug.Log(line);


        Vector3 deltaVec = a.position - b.position;
        Debug.Log(deltaVec);
        Debug.Log("delta");

        Vector3 step = deltaVec / segments;
      
        for (int i = 1; i < segments; i++)
        {
            line.SetPosition(i, a.position + (step * i)    );
        }
    }

    Transform GetClosestTransform(Transform[] availTransforms)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in availTransforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    bool IsCompatible(Transform ele1, Transform ele2)
    {
        if (ele1.transform.parent.parent.GetInstanceID() != ele2.transform.parent.parent.GetInstanceID())
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //find electron
            GameObject[] eles = GameObject.FindGameObjectsWithTag("Electron");
            GameObject closest = eles.OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                           .FirstOrDefault();   //or use .FirstOrDefault();  if you need just toArray(

            closest.GetComponent<LinkingUp>().LinkTo(transform); //(GameObject.FindGameObjectWithTag("RayLocation").transform)
            //link to player (this)

            currentLink = closest;

            GameObject.Find("LinkManager").GetComponent<LinkManager>().SetLinks();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObject[] eles = GameObject.FindGameObjectsWithTag("Electron");

            List<GameObject> compatibles = new List<GameObject>();

            foreach (GameObject g in eles)
            {
                if (IsCompatible(g.transform, currentLink.transform))
                {
                    compatibles.Add(g);
                }
            }
            GameObject closestAndCompatible = compatibles.OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

            closestAndCompatible.GetComponent<LinkingUp>().LinkTo(currentLink.transform);
            GameObject.Find("LinkManager").GetComponent<LinkManager>().SetLinks(true);


        }

    }
       
}
