using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atom : MonoBehaviour {
    public int atomNumber = 2;
    public int scale = 5;
    private int spawnDistance = 5;
    public Proton myProton;
    public Proton myNeutron;
    public Electron myElectron;
    public CircleLine ring;

    public bool negCharge = true;
    public GameObject pulseObj;

    private Material pulserMaterial;
    private GameObject Pulser;
    public float pulseSpeed = 0.5f;
    private float orbitRadius = 30;
    private float pullSpeed = 100f;
    public float minDistanceToOther = 5f;

    TextMeshManager textMgm;

    public GameObject Hydrogen;
    public GameObject Oxygen;

    private int[] orbitThreshold = new int[] { 2, 8, 8 };
    int[,] elAndO;

    public string textMeshName = "HH";
    Transform pullTo = null;

    void PulserMake() //backup
    {
        Pulser = Instantiate<GameObject>(pulseObj);
        Pulser.transform.parent = transform;
        Pulser.transform.localPosition = Vector3.zero;
    }
    // Use this for initialization
    void Start () {

        GameObject AtomAbstract;
        if (transform.name == "1")
        {
            AtomAbstract = Instantiate<GameObject>(Hydrogen);
        }
        else
        {
            AtomAbstract = Instantiate<GameObject>(Oxygen);

        }
        AtomAbstract.transform.parent = transform;
        AtomAbstract.tag = "Abstracted";
        AtomAbstract.transform.localPosition = Vector3.zero;
        AtomAbstract.gameObject.SetActive(false);
      
    

        textMgm = GameObject.Find("TextMeshTip").GetComponent<TextMeshManager>();
        textMeshName = UpdateData(0, 0, 0);
        textMgm.DisplayText(textMeshName);
        Debug.Log("Updated");
    }

    public void AtomConnect(Transform other)
    {
        pullTo = other;

        //lerp to other atom;
    }

    public void Abstract(string direction = "up")
    {
        if (direction == "up")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(0).gameObject.SetActive(true);
            //Cheating here for demo
            textMeshName = "H" + "<sub>" + "2" + "</sub>" + "O";
        }

        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (pullTo != null)
        {
            float step = pullSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pullTo.position, step);
            if (Vector3.Distance(transform.position, pullTo.position) < 15f)
            {
                pullTo = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Abstract("up");
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Abstract("down");
        }
    }

    private void Awake()
    {
        orbitRadius = transform.localScale.x * 6;

        elAndO = ElectronOrbitMap(8, orbitThreshold);
    }

    string UpdateData(int p, int e, int n)
    {
        ChemistryCalc chem = GameObject.Find("ChemistryCalc").GetComponent<ChemistryCalc>();
        string text = "<sub>" + p.ToString() + "</sub>" + chem.AtomShortCuts[p] + "<sup>" + chem.ChargeNess(p, e) + "</sup>";

        gameObject.tag = "Defined";

        return text;
    }


    int CalcOrbits(int electrons)
    {
        int nextOrbitAt = 0;
        for (int i = 0; i < orbitThreshold.Length; i++)
        {
            nextOrbitAt += orbitThreshold[i];
            if (electrons < nextOrbitAt)
            {
                return i;
            }
        }
        return 13;
    }

    public void ScaleMe(int myScale)
    {
        if (scale == 0)
        {
            scale = 10;
        }
        scale = myScale;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Generate(int protons, int electrons = -1, int neutrons = -1) //just use H or O
    {
      

        //deal w default params
        if (electrons == -1)
        {
            electrons = protons;
        }
        if (neutrons == -1)
        {
            neutrons = protons;
        }

        textMeshName = UpdateData(protons, electrons, neutrons);
        Debug.Log("----hallo" + textMeshName);

        int orbits = CalcOrbits(electrons);

        //Protons
        GameObject protContainer = new GameObject();
        protContainer.name = "Protons";

        //before knowing parent!
        protContainer.transform.localScale = new Vector3(1, 1, 1);

        protContainer.transform.parent = transform;
        protContainer.transform.localPosition = Vector3.zero; //.after knowing parent


        for (int i = 0; i < protons; i++)
        {
            Proton prot = Instantiate<Proton>(myProton);
            prot.transform.parent = protContainer.transform;
            prot.transform.localPosition = Random.onUnitSphere * spawnDistance;
        }

        //Neutrons
        GameObject neutContainer = new GameObject();
        neutContainer.transform.parent = transform;
        neutContainer.transform.localPosition = Vector3.zero;
        neutContainer.name = "Neutrons";
        for (int i = 0; i < neutrons; i++)
        {
            Proton neut = Instantiate<Proton>(myNeutron);
            neut.transform.parent = neutContainer.transform;
            neut.transform.localPosition = Random.onUnitSphere * spawnDistance;
        }
  
        MakeOrbits(electrons, orbits);
    }
    int[,] ElectronOrbitMap(int totalElectrons, int[] orbitThreshs)
    {
        int[,] electAndOrbit = new int[orbitThreshs.Length, 2];
        int elLeft = totalElectrons;

        for (int i = 0; i < orbitThreshs.Length; i++)
        {
            electAndOrbit[i, 0] = orbitThreshs[i];
            if (orbitThreshs[i] <= elLeft)
            {
                electAndOrbit[i, 1] = orbitThreshs[i];
                elLeft -= orbitThreshs[i];
            }
            else
            {
                electAndOrbit[i, 1] = elLeft;
                return electAndOrbit;
            }
        }
        return electAndOrbit;
    }

    void MakeOrbits(int electrons, int orbitCount)
    {
        int elRest = electrons;
        for (int i = 0; i <= orbitCount; i++)
        {
            GameObject orbit = new GameObject();
            orbit.name = "Orbit" + i;
            orbit.transform.parent = transform;
            orbit.transform.localPosition = Vector3.zero;

            CircleLine myRing = Instantiate<CircleLine>(ring);
            myRing.SetOrbitCapacity(elAndO[i, 0]);
            myRing.SetElectronCount(elAndO[i, 1]);


            myRing.CreatePoints(orbitRadius * (i+1));
            myRing.transform.parent = transform;
            myRing.transform.localPosition = Vector3.zero;

            //Shell for Orbit
            GameObject Shell = Instantiate<GameObject>(pulseObj);
            Shell.transform.parent = orbit.transform;
            Shell.transform.localPosition = Vector3.zero;
            float sr = (i + 1) * orbitRadius * 2f;

            Shell.transform.localScale = new Vector3(sr, sr, sr);
            //Electrons
            for (int j = 0; j < orbitThreshold[i]; j++)
            {
                Electron ele = Instantiate<Electron>(myElectron);
                ele.transform.parent = orbit.transform;
                float partinCircle = (float)j / orbitThreshold[i];
                ele.Arrange(i, orbitRadius, partinCircle);

                elRest--;
                if (elRest <= 0)
                {
                    return;
                }
            }
        }
    }

    void Pulse()
    {
        if (negCharge)
        {
            Pulser.transform.localScale += new Vector3(pulseSpeed, pulseSpeed, pulseSpeed);
        }
        if (Pulser.transform.localScale.x > 50)
        {
            Pulser.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}

