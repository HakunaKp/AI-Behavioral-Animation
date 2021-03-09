using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetection : MonoBehaviour {

    private float maxAngle;
    private float maxRadius;
    
    private static void Start() {
    }

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[10];
        LayerMask mask = LayerMask.GetMask("Players");
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps, mask);

        for (int i = 0; i < count + 1; i++)
        {

            if (overlaps[i] != null)
            {

                if (overlaps[i].transform == target)
                {

                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {

                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {

                            if (hit.transform == target)
                            {
                                return true;
                            }

                        }


                    }


                }

            }

        }
        return false;
    }

}