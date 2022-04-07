using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> weaponsList = new List<GameObject>();
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
     };
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                SetNewWeapon(i);
            }
        }
    }
    void SetNewWeapon(int index)
    {
        DisableAllWeapons();
        weaponsList[index].SetActive(true);
    }
    void DisableAllWeapons()
    {
        for (int i = 0; i < weaponsList.Count; i++)
        {
            weaponsList[i].SetActive(false);
        }
    }
}
