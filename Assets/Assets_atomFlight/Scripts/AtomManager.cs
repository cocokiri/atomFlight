using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AtomManager : MonoBehaviour
{

    public Atom atom;
    int[] atomNumbers = new int[] { 1, 8, 1 }; //make H2O
    // Use this for initialization
   // private Atom at;
    private Atom[] at = new Atom[3]; //atomNumberslength
    void Awake()
    {
        //instantiate 
        //generate
        for (int i = 0; i < atomNumbers.Length; i++)
        {
            at[i] = Instantiate<Atom>(atom);
            at[i].transform.position = new Vector3(i * 150, i * 100, i * 150);
            at[i].transform.name = atomNumbers[i].ToString();


        }
        StartCoroutine(WaitForAtomForSomeReason());
        //customize
    }

    void LateGenerate()
    {
        for (int i = 0; i < atomNumbers.Length; i++)
        {
            at[i].Generate(atomNumbers[i]);
            at[i].ScaleMe(3);
        }
    }

    public void markElectronsAvailable() { 


    }

    IEnumerator WaitForAtomForSomeReason()
    {
        yield return new WaitForSeconds(1f);
        LateGenerate();
    }

    private void Start()
    {
        
    }
}
  
