using UnityEngine;
using UnityEngine.EventSystems;


public class Cannon : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
    public GameObject m_shot;
    private float maxDistance;
    private bool shooting = false;
    private GameObject beam;
    private Ray ray;
    private GameObject touched;

    private void Start()
    {
        maxDistance = 100f;
    }


    public void Shoot()
    {
        if (shooting)
        {
            shooting = false;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Beam");
            foreach(GameObject obj in objects)
            {
                Destroy(obj);
            }

        }
        else
        {
            shooting = true;
            beam = Instantiate(m_shot);
        }
    }


    void LateUpdate () 
	{
        if (shooting)
        {
            ShootLaser(beam,m_muzzle.position,m_muzzle.forward);
        }
        else
        {

            if (touched != null && touched.tag == "Mirror")
            {
                touched.GetComponentInParent<Reflective>().DestroyBeam();
            }
        }
    }

    public void ShootLaser(GameObject beam,Vector3 position, Vector3 forward)
    {

        Ray ray = new Ray(position, forward);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        
        if(hitInfo.collider != null)
        {
            if (hitInfo.transform.gameObject.tag == "Mirror")
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
        else
        {
            var volumetric = beam.GetComponent<VolumetricLines.VolumetricLineBehavior>();
            volumetric.StartPos = position;
            volumetric.EndPos = position + maxDistance * forward;
        }
    }
 
    
}
