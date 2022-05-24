using UnityEngine;
using UnityEngine.EventSystems;


public class Cannon : MonoBehaviour
{

    public Transform m_cannonRot;
    public Transform m_muzzle;
    public GameObject m_shot;

    /* Add + L. toToggler function */
    private char beamColor = 'w';

    private float maxDistance = 100f;
    private bool shooting = false;
    private GameObject beam;
    private Ray ray;
    private GameObject touched;

    private float indiceEau = 1.39336f;


    public void Shoot()
    {
        if (shooting)
        {
            Destroy(beam);
            foreach (Reflective reflective in FindObjectsOfType<Reflective>())
                reflective.DestroyBeam();

            foreach (Refractive refractive in FindObjectsOfType<Refractive>())
                refractive.DestroyBeam();

            shooting = false;
        }
        else
        {
            shooting = true;
            beam = Instantiate(m_shot);
            ShootLaser(beam, m_muzzle.position, m_muzzle.forward);
        }
    }


    void FixedUpdate()
    {
        if (shooting)
        {
            ShootLaser(beam, m_muzzle.position, m_muzzle.forward);
        }
    }


    public void ShootLaser(GameObject beam, Vector3 position, Vector3 forward)
    {

        Ray ray = new Ray(position, forward);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);


        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.gameObject != null && hitInfo.transform.gameObject.tag == "Mirror")
            {
                hitInfo.transform.gameObject.GetComponentInParent<Reflective>().ToggleBeam(this.gameObject, hitInfo.point, Utility.Reflect(forward, hitInfo.normal), beamColor);
            }
            if (hitInfo.transform.gameObject != null && hitInfo.transform.gameObject.tag == "Water")
            {
                if (Utility.isRefracted(forward, hitInfo.normal, 1f, indiceEau))
                {


                    hitInfo.transform.gameObject.GetComponent<Refractive>().ToggleBeam(this.gameObject, hitInfo.point, Utility.Refract(forward, hitInfo.normal, 1f, indiceEau), beamColor, 1f);


                }
            }
            if (touched != null && touched != hitInfo.transform.gameObject)
            {
                if (touched.tag == "Mirror")
                    touched.GetComponentInParent<Reflective>().DestroyBeam();
                if (touched.tag == "Water")
                    touched.GetComponent<Refractive>().DestroyBeam();
            }
            touched = hitInfo.transform.gameObject;

            var volumetric = beam.GetComponent<VolumetricLines.VolumetricLineBehavior>();
            volumetric.StartPos = position;
            volumetric.EndPos = hitInfo.point;


        }
        else
        {
            var volumetric = beam.GetComponent<VolumetricLines.VolumetricLineBehavior>();
            volumetric.StartPos = position;
            volumetric.EndPos = position + maxDistance * forward;
        }

    }

}
