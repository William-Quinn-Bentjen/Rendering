using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSplat : MonoBehaviour
{
    public GameObject Splat;
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float offset = .01f;
    public float splatLifetime = 300;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void Reset()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(part, other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                RaycastHit hit;
                Debug.DrawRay(collisionEvents[i].intersection, collisionEvents[i].velocity, Color.red, 3);
                if (Physics.Raycast(new Ray(collisionEvents[i].intersection, collisionEvents[i].velocity), out hit))
                {
                    GameObject splatter = Instantiate(Splat);
                    splatter.transform.position = hit.point + (collisionEvents[i].velocity * -offset);
                    Vector3 particleRotationEuler = Quaternion.LookRotation(collisionEvents[i].normal).eulerAngles;
                    particleRotationEuler.z = Random.Range(0, 360);
                    splatter.transform.rotation = Quaternion.Euler(particleRotationEuler);
                    if (splatter.transform.rotation.eulerAngles.x < -150)
                    {
                        splatter.transform.localPosition = new Vector3(splatter.transform.localPosition.x, splatter.transform.localPosition.y - offset, splatter.transform.localPosition.z);
                    }
                    else
                    {
                        splatter.transform.localPosition = new Vector3(splatter.transform.localPosition.x, splatter.transform.localPosition.y, splatter.transform.localPosition.z + offset);
                    }
                    Destroy(splatter, splatLifetime);
                }
                
                
                //////
                //Debug.Log(collisionEvents[i].intersection + " " + collisionEvents[i].normal);
                //GameObject splat = Instantiate(debug, collisionEvents[i].intersection, Quaternion.identity);
                ////splat.transform.LookAt(collisionEvents[i].normal);
                //Vector3 pos = collisionEvents[i].intersection;
                //Vector3 force = collisionEvents[i].velocity * 10;
                //rb.AddForce(force);
            }
            i++;
        }
    }
}