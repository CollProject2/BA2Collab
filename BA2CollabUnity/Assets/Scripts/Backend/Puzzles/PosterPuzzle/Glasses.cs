using DG.Tweening;
using MoonSharp.VsCodeDebugger.SDK;
using UnityEngine;


public class Glasses : MonoBehaviour
{
    public enum GlassesState
    {
        right,
        left,
        threeD,
        off
    }

    public GlassesState glassesState;
    public GameObject[] glasses;
    public Material[] mat3d;
    public Material[] matr;
    public Material[] matb;
    public Material[] matn;
    public GameObject[] posters;
    
    public bool left;
    private bool canSwitch;
    public GlassesItem glassesItem;


    private void Awake()
    {
        glassesState = GlassesState.off;
        left = false;
        canSwitch = false;
    }

    public void InitializeGlasses()
    {
        glasses[0].GetComponent<MeshRenderer>().enabled = true;
        glasses[1].GetComponent<MeshRenderer>().enabled = true;
        Player.instance.canMove = false;
        canSwitch = true;
        glassesState = GlassesState.threeD;
        ActivateGlasses();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.hasGlasses)
        {
            if (Input.GetKeyDown(KeyCode.Q)) // take off glasses 
            {
                glassesState = GlassesState.off;
                glassesItem.isInteractable = true;
                Player.instance.hasGlasses = false;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                glassesState = GlassesState.threeD;
                
                ItemUIManager.Instance.ToggleItem(1);
            }

            if (glassesState != GlassesState.off && canSwitch)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    glassesState = GlassesState.left;
   
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    glassesState = GlassesState.right;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.D))
            {
                ActivateGlasses();
            }
        }
    }
    

    private void SetCanSwitch(bool state)
    {
        canSwitch = state;
    }

    public void ActivateGlasses()
    {
        switch (glassesState)
        {
            case GlassesState.off:
                gameObject.transform.DOMoveX(0.0f, 0.3f);
                for (int i = 0; i < posters.Length; i++)
                    posters[i].GetComponent<MeshRenderer>().material = matn[i];
                
                glasses[0].GetComponent<MeshRenderer>().enabled = false;
                glasses[1].GetComponent<MeshRenderer>().enabled = false;
                Player.instance.canMove = true;
                canSwitch = false;
                
                break;
            case GlassesState.right:
                BlueGlasses();
                break;
            case GlassesState.left:
                RedGlasses();
                break;
            case GlassesState.threeD:
                ThreeD();
                break;
        }
    }

    private void ThreeD()
    {
        SetCanSwitch(false);
        gameObject.transform.DOMoveX(0, 0.3f).OnComplete(() =>
        {
            SetCanSwitch(true);
        });
        
        for (int i = 0; i < posters.Length; i++)
            posters[i].GetComponent<MeshRenderer>().material = mat3d[i];
    }

    private void RedGlasses()
    {
        SetCanSwitch(false);
        //move glasses to redposition
        gameObject.transform.DOMoveX(-0.5f, 0.3f).OnComplete(() =>
        {
            SetCanSwitch(true);
        });

        for (int i = 0; i < posters.Length; i++)
        {
            posters[i].GetComponent<MeshRenderer>().material = matr[i];
        }
    }

    private void BlueGlasses()
    {
        SetCanSwitch(false);
        gameObject.transform.DOMoveX(0.5f, 0.3f).OnComplete(() =>
        {
            SetCanSwitch(true);
        });
        for (int i = 0; i < posters.Length; i++)
        {
            posters[i].GetComponent<MeshRenderer>().material = matb[i];
        }
    }

}
