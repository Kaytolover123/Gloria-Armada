using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject enemy;

    public enum Team
    {
        player,
        enemy,
    }
    [SerializeField] Team team = Team.enemy;

    Autopilot ap;
    Plane planeSelf;
    Rigidbody rb;

    bool isArmed = false;
    [SerializeField] float burnTime = 1.0f;
    [SerializeField] float selfDetTime = 3.0f;
    Vector3 lastPosition;
    [SerializeField] GameObject detonationEffect;
    [SerializeField]LayerMask collisionMask;

    FieldOfView fov;


    Perspective pers = Perspective.Side_On;
    // Start is called before the first frame update
    void Start()
    {
        planeSelf = GetComponent<Plane>();
        ap = GetComponent<Autopilot>();
        rb = GetComponent<Rigidbody>();
        fov = GetComponent<FieldOfView>();

        if (pers == Perspective.Side_On)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY  | RigidbodyConstraints.FreezePositionZ;
        }

        rb.angularDrag = 0.01f;
        //Find the player by tag
        // TODO: This should get passed by the spawner
        if (enemy)
        {
            ap.setTargetObject(enemy);
            ap.setAPState(Autopilot.AutopilotState.pointAt);
        }
        ap.onAxes = true;
        planeSelf.SetThrottle(1.0f);
        // Wait for a short time before homing in on player
        StartCoroutine(Wait());  
    }

    // Coroutine for missile to wait before homing in on player
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        planeSelf.SetThrottle(1.0f);
        isArmed = true;
        StartCoroutine(BurnOut());
    }

    // Coroutine for setting thrust to 0 after a certain amount of time
    IEnumerator BurnOut()
    {
        yield return new WaitForSeconds(burnTime);
        planeSelf.SetThrottle(0.0f);
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDetTime);
        Detonate();
    }

    void Detonate(){
        //StopAllCoroutines();
        Instantiate(detonationEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.tag == "Player")
    //     {
    //         // Detonate missile
    //         Instantiate(detonationEffect, transform.position, Quaternion.identity);
    //         Destroy(gameObject);
    //     }
    // }

    void FixedUpdate(){
        var diff = rb.position - lastPosition;
        lastPosition = rb.position;

        Ray ray = new Ray(lastPosition - diff, diff.normalized * 2);
        Debug.DrawRay(lastPosition - diff, diff.normalized * 2, Color.red, 0.1f);
        RaycastHit hit;
        float width = 2.5f;

        if (!isArmed){
            return;
        }
        if (Physics.SphereCast(ray, width, out hit, diff.magnitude, collisionMask.value)) {
            //Plane other = hit.collider.GetComponent<Plane>();
            Detonate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            if (fov.visibleTargets.Count > 0)
            {
                // Check if the gameObject still exists
                if (fov.visibleTargets[0] != null)
                {
                    enemy = fov.visibleTargets[0].gameObject;
                    ap.setTargetObject(enemy);
                    ap.setAPState(Autopilot.AutopilotState.pointAt);
                }
            }
        }
    }
}