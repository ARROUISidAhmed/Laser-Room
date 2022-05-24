
using UnityEngine;
using UnityEngine.EventSystems;

public class Reflective : MonoBehaviour 
{
    public GameObject beamPrefab;
    public bool winner = false;
    private float maxDistance;
    private GameObject beam;
    private bool isReflecting = false;
    private GameObject touched;
    private Vector3 position;
    private Vector3 forward;

    private void Start()
    {
        maxDistance = 100f;
    }

    public void ToggleBeam(Vector3 position, Vector3 forward)
    {
        this.position = position;
        this.forward = forward;

        if (!isReflecting && beam == null)
        {

            beam = Instantiate(beamPrefab);
            ShootLaser();
            isReflecting = true;
        }
    }

    public void DestroyBeam()
    {
        if (isReflecting)
        {
            if(beam != null)
            {
                Destroy(beam);
            }
            isReflecting = false;
        }
    }

    void LateUpdate()
    {
        if (isReflecting)
        {
            ShootLaser();
        }
        else
        {

            if (touched != null && touched.tag == "Mirror")
            {
                touched.GetComponentInParent<Reflective>().DestroyBeam();
            }
        }
    }

    public void ShootLaser()
    {

        Ray ray = new Ray(position, forward);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {

            switch (hitInfo.transform.gameObject.tag)
            {
                case "Mirror":
                    hitInfo.transform.gameObject.GetComponentInParent<Reflective>().ToggleBeam(hitInfo.point, Utility.Reflect(forward, hitInfo.normal));
                    break;
                case "Target":
                    hitInfo.transform.gameObject.GetComponent<Winning>().Win();
                    break;
                default:
                    break;
            }

            if (touched != null && touched.tag == "Mirror" && !touched.Equals(hitInfo.transform.gameObject))
            {
                touched.GetComponentInParent<Reflective>().DestroyBeam();
            }
            touched = hitInfo.transform.gameObject;

            if (beam != null)
            {
                var volumetric = beam.GetComponent<VolumetricLines.VolumetricLineBehavior>();
                volumetric.StartPos = position;
                volumetric.EndPos = hitInfo.point;
            }

        }
        else
        {
            if (beam != null)
            {
                var volumetric = beam.GetComponent<VolumetricLines.VolumetricLineBehavior>();
                volumetric.StartPos = position;
                volumetric.EndPos = position + maxDistance * forward;
            }
        }
    }

}
