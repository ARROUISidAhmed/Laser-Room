using UnityEngine;
using UnityEngine.EventSystems;


public class Cannon : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
    public GameObject m_shot;

    private bool shooting = false;
    private GameObject beam;
    private Ray ray;
    private GameObject touched;
    
    

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


    void FixedUpdate () 
	{
        if (shooting)
        {
            ShootLaser(beam,m_muzzle.position,m_muzzle.forward);
        }
	}

    public void ShootLaser(GameObject beam,Vector3 position, Vector3 forward)
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
