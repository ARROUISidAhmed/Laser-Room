using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reflective : MonoBehaviour
{
    public GameObject beamPrefab;
    private GameObject beam;
    private bool reflecting = false;
    private GameObject touched;
    private Vector3 position;
    private Vector3 forward;


    public void ToggleBeam(Vector3 position, Vector3 forward)
    {
        this.position = position;
        this.forward = forward;
        if (!reflecting)
        {

            beam = Instantiate(beamPrefab);
            ShootLaser();
            reflecting = true;
        }
    }

    public void DestroyBeam()
    {
        if (reflecting)
        {
            Destroy(beam);
            reflecting = false;
        }
    }

    void FixedUpdate()
    {
        if (reflecting)
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


        if (hitInfo.transform.gameObject != null && hitInfo.transform.gameObject.tag == "Mirror")
        {
            hitInfo.transform.gameObject.GetComponentInParent<Reflective>().ToggleBeam(hitInfo.point, Utility.Reflect(forward, hitInfo.normal));
        }

        if (touched != null && touched.tag == "Mirror" && !touched.Equals(hitInfo.transform.gameObject))
        {
            touched.GetComponentInParent<Reflective>().DestroyBeam();
        }
        touched = hitInfo.transform.gameObject;


        var volumetric = beam.GetComponent<VolumetricLines.VolumetricLineBehavior>();
        volumetric.StartPos = position;
        volumetric.EndPos = hitInfo.point;
    }
}
