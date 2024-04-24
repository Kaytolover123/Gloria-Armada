using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpecialMeter : MonoBehaviour
{
    public Image meter;
    public float meterLevel = 1;
    //private float meterCooldown = 3;

    public Color uncharged, charged;


    // Start is called before the first frame update
    void Start()
    {
        meterLevel = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        /*meterCooldown += Time.deltaTime;
        if (meterLevel < 0)
        {
            meterLevel = 0;
        }

        if (meterLevel < 1)
        {
            if (meterCooldown >= 3)
            {
                meterLevel += 0.1f * Time.deltaTime;
            }
        }
        else
        {
            meterLevel = 1;
        }

        if (meterLevel < 0.25f)
        {
            meter.color = uncharged;
        }
        else
        {
            meter.color = charged;
        }*/

        meter.fillAmount = meterLevel;

        /*if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(meterLevel >= 0.25f)
            {
                meterLevel -= 0.25f;
                meterCooldown = 0;
            }
        }*/
    }

    private void OnEnable(){
        // Update Score on enemy death 
        Actions.OnAmmoChange += UpdateMeter;
        
    }

    private void OnDisable(){
        // if gameobject is disabled remove all listeners
        Actions.OnAmmoChange -= UpdateMeter;

    }


    private void UpdateMeter(float ammo){
        Debug.Log("Level: " + ammo);
        meterLevel = ammo * 0.25f;
        Debug.Log("meterLevel: " + meterLevel);

    }
}