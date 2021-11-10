using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    // adapted from this tutorial: https://sharpcoderblog.com/blog/third-person-camera-in-unity-3d
    // make script so that it doesn't fully collide with wall so that it works good with Aaron's shader

    public Transform referenceTransform;
    public float collisionOffset = 0.3f; //To prevent Camera from clipping through Objects
    public float cameraSpeed = 15f; //How fast the Camera should snap into position if there are no obstacles

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform;
    float defaultDistance;

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
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));

            transform.localPosition = currentPos;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * cameraSpeed);
        }
    }
}
