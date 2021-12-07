using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutoutObject : MonoBehaviour
{
    public float cutoutSize;
    public float falloffSize;

    [SerializeField]
    private Transform targetObject;
    
    [SerializeField]
    private Transform minView;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    internal Transform[] lastHits = new Transform[0];

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        if (lastHits.Length >0)
        for (int i = 0; i < lastHits.Length; i++) //Function cycles through all objects in provided array - Alice
        {
            if (lastHits[i])
            {
                Material[] materials = lastHits[i].GetComponent<Renderer>().materials;

                for (int m = 0; m < materials.Length; ++m)
                {
                    Debug.Log(lastHits[i].name);
                    materials[m].SetVector("_CutoutPos", cutoutPos);
                    materials[m].SetFloat("_CutoutSize", 0f);
                    materials[m].SetFloat("_FalloffSize", 0f);
                }
            }
        }

        lastHits = new Transform[hitObjects.Length];

        if (hitObjects.Length > 0) //if raycast detects objects between it and the player - Alice
        {
            for (int i = 0; i < lastHits.Length; ++i)
            {
                lastHits[i] = hitObjects[i].transform;

                Material[] materials = lastHits[i].GetComponent<Renderer>().materials;

                for (int m = 0; m < materials.Length; ++m)
                {
                    materials[m].SetVector("_CutoutPos", cutoutPos);
                    materials[m].SetFloat("_CutoutSize", cutoutSize);
                    materials[m].SetFloat("_FalloffSize", falloffSize);
                }
            }
        }
    }
}
