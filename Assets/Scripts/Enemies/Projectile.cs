using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public struct ProjectileStats{
    public float speed; 
    public int damage;
    float lifetime;

}

public class Projectile : MonoBehaviour
{
    // Base Class for All Projectile Objects
    public ProjectileStats projectileStats; 
    public Rigidbody projectileRB;
    float startTime;
    float lifetime = 10;



    // Start is called before the first frame update
    void Start()
    {
        // Set Default Stats
        //projectileStats.speed = 8;
        //projectileStats.damage = 2;
    }
    public void Launch(ProjectileStats stats) {
        startTime = Time.time;
        
        projectileRB = GetComponent<Rigidbody>();
        projectileRB.AddRelativeForce(new Vector3(0, 0, stats.speed), ForceMode.VelocityChange); 
    }
    public void Launch(ProjectileStats stats, Vector3 cameraVelocity) {
        startTime = Time.time;
        projectileRB = GetComponent<Rigidbody>();
        projectileRB.AddRelativeForce(new Vector3(0, 0, stats.speed), ForceMode.VelocityChange);
        projectileRB.AddForce(cameraVelocity, ForceMode.VelocityChange); 
    }


    void FixedUpdate() {
        if (Time.time > startTime + lifetime) {
            Destroy(gameObject);
        }
    }

    public void SetStats(ProjectileStats stats){
        //Debug.Log(stats);
        projectileStats = stats;
    }

}
