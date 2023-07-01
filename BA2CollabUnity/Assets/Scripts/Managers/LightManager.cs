using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    [Header("Player Lights")] 
    [SerializeField] private GameObject playerLightYellow;
    [SerializeField] private GameObject playerLightWhite;
    [SerializeField] private GameObject playerLightBlue;
    [SerializeField] private GameObject playerLightPurple;
    
    [Header("Front Stage Lights")] 
    [SerializeField] private GameObject stage1;
    [SerializeField] private GameObject stage2;
    
    [Header("Play Area Lights")]
    [SerializeField] private GameObject middleBlue;
    [SerializeField] private GameObject frontWarmOrange;
    [SerializeField] private GameObject doorlightRight;
    [SerializeField] private GameObject doorlightLeft;
    
    [Header("BedroomLights")]
    [SerializeField] private GameObject bedroomOfficeDoorHighlight;
    
    [Header("OfficeLights")]
    [SerializeField] private GameObject OfficeMovingBoxhighlight;
    [Header("EntranceLights")]
    [SerializeField] private GameObject entranceLivingRoomhighlight;
    [SerializeField] private GameObject entranceBedRoomhighlight;
    [Header("LivingRoomLights")]
    [SerializeField] private GameObject livingRoomEntranceHighlight;
    
    public bool canLookatPlayer = false;
    private void Awake()
    {
        //Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void TurnOffFrontStageLights()
    {
        stage1.SetActive(false);
        stage2.SetActive(false);
    }
    public void TurnOnFrontStageLights()
    {
        stage1.SetActive(true);
        stage2.SetActive(true);
    }

    public void TurnOnPlayerLights()
    {
        playerLightBlue.SetActive(true);
        playerLightPurple.SetActive(true);
        playerLightWhite.SetActive(true);
        playerLightYellow.SetActive(true);
        frontWarmOrange.SetActive(true);
        doorlightLeft.SetActive(true);
        doorlightRight.SetActive(true);
        playerLightBlue.transform.DOLookAt(new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y -0.88f, Player.instance.transform.position.z), 1).SetEase(Ease.OutBack);
        playerLightPurple.transform.DOLookAt(new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y -0.88f, Player.instance.transform.position.z), 1).SetEase(Ease.OutBack);
        playerLightWhite.transform.DOLookAt(new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y -0.88f, Player.instance.transform.position.z), 1).SetEase(Ease.OutBack);
        playerLightYellow.transform.DOLookAt(new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y -0.88f, Player.instance.transform.position.z), 1).SetEase(Ease.OutBack).OnComplete(() => canLookatPlayer = true );
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lightOpen,transform.position);
    }
    
    public void TurnOffPlayerLights()
    {
        playerLightBlue.SetActive(false);
        playerLightPurple.SetActive(false);
        playerLightWhite.SetActive(false);
        playerLightYellow.SetActive(false);
        frontWarmOrange.SetActive(false);
        doorlightLeft.SetActive(false);
        doorlightRight.SetActive(false);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lightOpen,transform.position);
    }

    public void OpenBedroomOfficeDoorHighlights(bool state)
    {
        bedroomOfficeDoorHighlight.SetActive(state);
    }

    public void OpenOfficeMovingBoxHighLight(bool state)
    {
        OfficeMovingBoxhighlight.SetActive(state);
    }
    
    public void OpenEntranceLivingRoomDoorHighLight(bool state)
    {
        entranceLivingRoomhighlight.SetActive(state);
    }
    
    public void OpenEntranceBedRoomDoorHighLight(bool state)
    {
        entranceBedRoomhighlight.SetActive(state);
    }
    public void OpenLivingRoomEntranceDoorHighLight(bool state)
    {
        livingRoomEntranceHighlight.SetActive(state);
    }
    
    
    
}
