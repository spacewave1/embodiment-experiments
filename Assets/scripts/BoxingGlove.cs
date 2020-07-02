using System.Collections;
using System.Collections.Generic;
using experiments;
using UnityEngine;

public class BoxingGlove : MonoBehaviour
{
    public List<Transform> rayOrigins;
    private AvatarController _avatarController;
    public float rayCastLength = 0.05f;
    
    void Start()
    {
        _avatarController = GetComponentInParent<AvatarController>();
    }
    
    void Update()
    {
        rayOrigins.ForEach(rayOrigin =>
        {
            RaycastHit hit;
            List<Ray> rays = new List<Ray>();
            rays.Add(new Ray(rayOrigin.position, rayOrigin.TransformDirection(rayOrigin.forward) * rayCastLength));
            rays.Add(new Ray(rayOrigin.position, rayOrigin.TransformDirection(rayOrigin.forward + rayOrigin.up) * rayCastLength));
            rays.Add(new Ray(rayOrigin.position, rayOrigin.TransformDirection(rayOrigin.forward -rayOrigin.up) * rayCastLength));
            rays.Add(new Ray(rayOrigin.position, rayOrigin.TransformDirection(rayOrigin.forward -rayOrigin.right) * rayCastLength));
            rays.Add(new Ray(rayOrigin.position, rayOrigin.TransformDirection(rayOrigin.forward + rayOrigin.right) * rayCastLength));

            rays.ForEach(ray =>
            {
                Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.green);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 0.1f))
                {
                    if (hit.collider.CompareTag("target"))
                    {
                        _avatarController.ProcessBoxHit(hit.collider.GetComponent<PunchingTarget>());
                    }
                    if (hit.collider.CompareTag("zone"))
                    {
                        hit.collider.GetComponent<JumpingJackZone>().handsInZone = true;
                    }
                }
            }); 
        });
    }

    void OnDrawGizmosSelected()
    {
        rayOrigins.ForEach(rayOrigin =>
        {
            Gizmos.color = Color.red;
            List<Ray> rays = new List<Ray>();
            rays.Add(new Ray(rayOrigin.position, rayOrigin.TransformDirection(rayOrigin.forward) * rayCastLength));

            rays.ForEach(ray => Gizmos.DrawRay(ray.origin, ray.direction * rayCastLength)); 
        });
    }
}