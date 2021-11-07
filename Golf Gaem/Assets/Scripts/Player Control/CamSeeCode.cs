using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSeeCode : MonoBehaviour
{
    public Transform player;
    public Transform[] obstructions;

    //[Header("Conecast Input")]
    //public float endRadius;


    void FixedUpdate()
    {
        GetObstructions();
    }

    internal void GetObstructions()
    {
        float characterDistance = Vector3.Distance(transform.position, player.transform.position);
        int layerMask = 1 << 6; //Raycast only gets colliders in layer 6 ("Terrain")

        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.position - transform.position, characterDistance, layerMask);

        if(hits.Length > 0) //if raycast detects objects between it and the player - Alice
        {
            for (int i = 0; i < obstructions.Length; i++) //Function cycles through all objects in provided array - Alice
            {
                   obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            
            obstructions = new Transform[hits.Length];

            for (int i = 0; i < obstructions.Length; i++)
            {
                obstructions[i] = hits[i].transform;
                obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }

        }
    }

    public static RaycastHit[] ConeCastAll(Vector3 origin, float maxRadius, Vector3 direction, float maxDistance, float coneAngle)
    {
        //USE: Works like a spherecast except it's a cone

        //CREDIT: Created by walterellisfun (https://github.com/walterellisfun/ConeCast/blob/master/ConeCastExtension.cs), minor modifications made by Christian Hotte

        RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin - new Vector3(0, 0, maxRadius), maxRadius, direction, maxDistance);
        List<RaycastHit> coneCastHitList = new List<RaycastHit>();

        if (sphereCastHits.Length > 0)
        {
            for (int i = 0; i < sphereCastHits.Length; i++)
            {
                //sphereCastHits[i].collider.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
                Vector3 hitPoint = sphereCastHits[i].point;
                Vector3 directionToHit = hitPoint - origin;
                float angleToHit = Vector3.Angle(direction, directionToHit);

                if (angleToHit < coneAngle)
                {
                    coneCastHitList.Add(sphereCastHits[i]);
                }
            }
        }

        RaycastHit[] coneCastHits = new RaycastHit[coneCastHitList.Count];
        coneCastHits = coneCastHitList.ToArray();

        return coneCastHits;
    }
}
