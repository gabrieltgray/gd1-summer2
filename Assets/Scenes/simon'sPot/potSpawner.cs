using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potSpawner : MonoBehaviour
{

    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;
    float target_Distance;
    float projectile_Velocity;
    float Vx;
    float Vy;
    float flightDuration;
    float elapse_time = 0;
    float delay;
    float delayTimer;
    public float upperDelay;
    public float lowerDelay;
    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        resetPos();


    }

    public void resetPos(){
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
        target_Distance = Vector3.Distance(Projectile.position, Target.position);
        projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle* Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
        Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle* Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle* Mathf.Deg2Rad);

        // Calculate flight time.
        flightDuration = target_Distance / Vx;

        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);
        delay = Random.Range(lowerDelay, upperDelay);
        delayTimer = 0f;
    }

	

	

    private void Update()
    {
        if(delay> delayTimer){
            transform.GetChild(0).gameObject.SetActive(false);
            delayTimer += Time.deltaTime;
            return;
        }
        transform.GetChild(0).gameObject.SetActive(true);
        if(Projectile.transform.position.y < -3f){
            resetPos();
            elapse_time = 0;
        }
        Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
        elapse_time += Time.deltaTime;
        Projectile.GetChild(0).transform.rotation *= Quaternion.AngleAxis(10f, Projectile.transform.right);
        Projectile.GetChild(0).transform.rotation *= Quaternion.AngleAxis(10f, Projectile.transform.up);

    }


}
