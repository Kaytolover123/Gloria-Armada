using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBarrel : Weapon
{
    private float timer = 0; 
    private int counter = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetupWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerWeapon){
            PlayerUpdate();
        }
    }

    public override void Fire(Vector3 velocity)
    {
        if(canFire){
            base.Fire(velocity);
            counter++;
            base.Fire(velocity);
            counter++;
            base.Fire(velocity);
            timer = 0;
            canFire = false;
            counter = 1;
        }
    } 

    public override void EnemyFire()
    {
        base.EnemyFire();
        counter++;
        base.EnemyFire();
        counter++;
        base.EnemyFire();
        counter = 1;
    }

    public override void SetupWeapon(){
        //weaponStats.fireInterval = 1f;
        //weaponStats.projectileStats.damage = 1;
        //weaponStats.projectileStats.speed = 6;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = fireSound;
    }

    private void PlayerUpdate(){
        // Increase timer
        timer += Time.deltaTime; 
        if(!canFire && timer >= weaponStats.fireInterval){
            canFire = true;
        }
    }

    public override Vector3 GetSpawnPos(){
        switch(counter){
            case 1:
                return gameObject.transform.position + gameObject.transform.forward * 8 + gameObject.transform.up * 3;
            case 2:
                return base.GetSpawnPos();
            case 3:
                return gameObject.transform.position + gameObject.transform.forward * 8 + gameObject.transform.up * -3;
            default:
                return base.GetSpawnPos();
        }
    }

    public override Quaternion GetSpawnRotation(){
        switch(counter){
            case 1:
                return Quaternion.Euler(gameObject.transform.eulerAngles.x - 45, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
            case 2:
                return base.GetSpawnRotation();
            case 3:
                return Quaternion.Euler(gameObject.transform.eulerAngles.x + 45, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
            default:
                return base.GetSpawnRotation();
        }
    }

    public override void PlaySound(){
        if(counter == 1){
            base.PlaySound();
        }
    }

}