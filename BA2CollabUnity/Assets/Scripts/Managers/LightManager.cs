using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;
    [Header("BackGroundColors")] 
    public Color bedRoomColor;
    public Color officeColor;
    public Color screeningRoomColor;
    public Color VideoColor;
    public Color GardenColor;
    public Color livingRoomColor;
    public Color entranceRoomColor;
    public List<Light> bgLights;
    [Header("OverHeadLightColors")]
    public Color overHeadBedRoomColor;
    public Color overHeadOfficeColor;
    public Color overHeadScreeningRoomColor;
    public Color overHeadVideoColor;
    public Color overHeadGardenColor;
    public Color overHeadLivingRoomColor;
    public Color overHeadEntranceRoomColor;
    public List<Light> overHeadLight;
    [Header("MainIlluminator")] 
    public GameObject middleLight;
    
    [Header("Player Lights")] 
    [SerializeField] private GameObject playerLightYellow;
    [SerializeField] private GameObject playerLightWhite;
    [SerializeField] private GameObject playerLightBlue;
    [SerializeField] private GameObject playerLightPurple;
    
    [Header("Front Stage Lights")] 
    [SerializeField] private GameObject stage1;
    [SerializeField] private GameObject stage2;
    [SerializeField] private GameObject buttonFollowerStart;
    [SerializeField] private GameObject dialogBoxLight;

    [Header("UI Lights")]
    [SerializeField] private GameObject JournalLight;
    [SerializeField] private GameObject letterLight;
    [SerializeField] private GameObject lockLight;
    [SerializeField] private GameObject giftLight;
    [SerializeField] private GameObject textPuzzleLight;



    [Header("BedroomLights")]
    [SerializeField] private GameObject bedroomOfficeDoorHighlight;
    
    [Header("OfficeLights")]
    [SerializeField] private GameObject OfficeMovingBoxhighlight;
    [Header("EntranceLights")]
    [SerializeField] private GameObject entranceLivingRoomhighlight;
    [SerializeField] private GameObject entranceBedRoomhighlight;
    
    [SerializeField] private GameObject entranceLightTowardsLivingDoor;
    [SerializeField] private GameObject entranceLightTowardsBedroomDoor;
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

    private void Start()
    {
        OpenLetterLight(false);
        OpenGiftCardLight(false);
        OpenLockPuzzleLight(false);
        OpenJournalUILight(false);
        OpenTextPuzzleLight(false);
        OpenOverHeadLights(false);
        OpenMiddleLight(false);
        
    }


    public void ChangeColorBG(Color color)
    {
        foreach (var light in bgLights)
        {
            light.DOColor(color, 2);
        }
    }
    public void ChangeColorOverHead(Color color)
    {
        foreach (var light in overHeadLight)
        {
            light.DOColor(color, 2);
        }
    }

    public void OpenOverHeadLights(bool state)
    {
        foreach (var light in overHeadLight)
        {
            light.gameObject.SetActive(state);
        }
    }

    public void OpenMiddleLight(bool state)
    {
        middleLight.SetActive(state);
    }
    public void OpenJournalUILight(bool state)
    {
        JournalLight.SetActive(state);
    }
    public void OpenLetterLight(bool state)
    {
        letterLight.SetActive(state);
    }
    public void OpenLockPuzzleLight(bool state)
    {
        lockLight.SetActive(state);
    }
    public void OpenGiftCardLight(bool state)
    {
        giftLight.SetActive(state);
    }
    public void OpenTextPuzzleLight(bool state)
    {
        textPuzzleLight.SetActive(state);
    }



    public void OpenFrontStageLights(bool state)
    {
        stage1.SetActive(state);
        stage2.SetActive(state);
    }

    public void OpenEntranceDoorLights(bool state)
    {
        entranceLightTowardsBedroomDoor.SetActive(state);
        entranceLightTowardsLivingDoor.SetActive(state);
    }
    

    public void TurnOnPlayerLights()
    {
        playerLightBlue.SetActive(true);
        playerLightPurple.SetActive(true);
        playerLightWhite.SetActive(true);
        playerLightYellow.SetActive(true);
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

    public void OpenButtonFollwerLight(bool state)
    {
        buttonFollowerStart.SetActive(state);
    }
    public void OpenDialogBoxLight(bool state)
    {
        dialogBoxLight.SetActive(state);
    }
    
    
    
}
