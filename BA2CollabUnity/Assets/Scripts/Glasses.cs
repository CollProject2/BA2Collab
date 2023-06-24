using DG.Tweening;
using UnityEngine;


public class Glasses : MonoBehaviour
{
    public GameObject[] glasses;
    public Material[] mat3d;
    public Material[] matr;
    public Material[] matb;
    public Material[] matn;
    public GameObject[] posters;
    public bool isActive;
    public bool left;
    private bool canSwitch;


    private void Awake()
    {
        isActive = false;
        left = false;
        canSwitch = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.hasGlasses)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isActive = !isActive;
                ActivateGlasses(isActive);
            }

            if (isActive && canSwitch)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    RedGlasses();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    BlueGlasses();
                }
            }
        }
    }

    private void SetCanSwitch(bool state)
    {
        canSwitch = state;
    }

    public void ActivateGlasses(bool state)
    {
        if (state)
        {
            for (int i = 0; i < posters.Length; i++)
                posters[i].GetComponent<MeshRenderer>().material = mat3d[i];

        }
        else
        {
            gameObject.transform.DOMoveX(0.0f, 0.3f);
            for (int i = 0; i < posters.Length; i++)
                posters[i].GetComponent<MeshRenderer>().material = matn[i];


        }
        glasses[0].GetComponent<MeshRenderer>().enabled = state;
        glasses[1].GetComponent<MeshRenderer>().enabled = state;
        Player.instance.canMove = !state;
        canSwitch = state;
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
