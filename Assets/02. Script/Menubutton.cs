using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Menubutton : MonoBehaviour
{

    public GameObject panel;
    public GameObject joystick_;
    public GameObject menu;
    public GameObject uiInventory;
    public GameObject uiEquipment;
    public GameObject uiSound;
    public Player player;


    public void exitButtonClicked()
    {

        player.DataSave();
        Application.Quit();
    }

    public void InventoryClicked()
    {
        menu.SetActive(false);
        uiInventory.SetActive(true);
    }

    public void EquipmentClicked()
    {
        menu.SetActive(false);
        uiEquipment.SetActive(true);
    }

    public void SoundClicked()
    {
        menu.SetActive(false);
        uiSound.SetActive(true);
    }



    public void onClicked()
    {
        if (panel.activeInHierarchy)
        {
            player.flag = false;
            panel.SetActive(false);
            joystick_.SetActive(false);
        }
        else
        {
            joystick_.SetActive(true);
            player.flag = true;
            panel.SetActive(true);
            
        }
            
        
        if (menu.activeInHierarchy)

            menu.SetActive(false);
        else
            menu.SetActive(true);

        if (uiInventory.activeInHierarchy)
        {
            uiInventory.SetActive(false);
            player.flag = false;
            panel.SetActive(false);
            joystick_.SetActive(false);
        }

        if (uiEquipment.activeInHierarchy)
        {
            uiEquipment.SetActive(false);
            player.flag = false;
            panel.SetActive(false);
            joystick_.SetActive(false);
        }

        if (uiSound.activeInHierarchy)
        {
            uiSound.SetActive(false);
            player.flag = false;
            panel.SetActive(false);
            joystick_.SetActive(false);
        }

    }
}
