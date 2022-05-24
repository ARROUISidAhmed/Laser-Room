using UnityEngine;

public class FollowAndRotate : MonoBehaviour {
    public Transform target;
    public float followSpeed = 0.5f;
    public float angularSpeed = 0.5f;


    private float positionOffSet = 3f;
    private float angularOffSet  = 0f;
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    private void LateUpdate()
    {

        Vector3 desiredPosition = target.position;
        Quaternion desiredRotation = target.rotation;
        /*
        DetectTouchMovement.Calculate();
        if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 0)
        { // zoom
            positionOffSet = Mathf.Lerp(3f,5f, DetectTouchMovement.pinchDistanceDelta);
        }
        if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
        { // rotate
            angularOffSet = Mathf.Lerp(-60f,60f,DetectTouchMovement.turnAngleDelta);
        }
        */
        desiredPosition -= (positionOffSet * target.forward);
        desiredRotation *= Quaternion.AngleAxis(angularOffSet,target.up);

        myTransform.position = Vector3.Lerp(myTransform.position, desiredPosition,followSpeed * Time.deltaTime );
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, desiredRotation, angularSpeed * Time.deltaTime);
    }
    
}
