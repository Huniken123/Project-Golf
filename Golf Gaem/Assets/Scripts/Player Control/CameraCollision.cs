using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    // adapted from this tutorial: https://sharpcoderblog.com/blog/third-person-camera-in-unity-3d
    // make script so that it doesn't fully collide with wall so that it works good with Aaron's shader

    public Transform ctrlTransform; //transform of control point
    public float castRadius = 0.3f; //To prevent Camera from clipping through Objects
    public float cameraSpeed = 15f; //How fast the Camera should snap into position if there are no obstacles
    public Transform minCamPos;

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform;
    float defaultDistance;
    LayerMask lm = 1 << 1;

    void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;    
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }

    void LateUpdate()
    {
        Vector3 currentPos = defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - ctrlTransform.position;
        if (Physics.SphereCast(ctrlTransform.position, castRadius, dirTmp, out hit, defaultDistance, ~lm, QueryTriggerInteraction.Ignore))
        {
            float minZ = minCamPos.position.z;
            currentPos = (directionNormalized * (hit.distance - castRadius));

            if (Mathf.Abs(minZ) > Mathf.Abs(currentPos.z))
                transform.position = minCamPos.position;
            else
                transform.localPosition = currentPos;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * cameraSpeed);
        }
    }
}
