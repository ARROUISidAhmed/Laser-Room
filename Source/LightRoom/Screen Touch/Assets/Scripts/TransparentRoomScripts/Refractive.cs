
using UnityEngine;
using UnityEngine.EventSystems;

public class Refractive : MonoBehaviour
{
    public bool winner;

    public float indiceEau = 1.39336f;
    public float indiceAir = 1f;



    public GameObject[] laserColors = new GameObject[7];
    private char basicColor = 'w';
    private char color = 'w';
    private float indiceIncident;
    private float maxDistance;
    private GameObject beam;
    private GameObject source;
    private bool isRefracting = false;
    private GameObject touched;
    private Vector3 position;
    private Vector3 forward;

    private void Start()
    {
        maxDistance = 100f;
    }



    public void ToggleBeam(GameObject source, Vector3 position, Vector3 forward, char color, float indice)
    {
        this.position = position;
        this.forward = forward;
        this.color = color;
        this.source = source;
        this.indiceIncident = indice;
        if (!isRefracting)
        {
            beam = Instantiate(laserColors[0]);
            isRefracting = true;
            ShootLaser();
        }

    }


    public void DestroyBeam()
    {
        if (isRefracting)
        {
            if (beam != null)
                Destroy(beam);
            isRefracting = false;

        }
    }

    void LateUpdate()
    {
        if (isRefracting)
        {
            ShootLaser();
        }
        else
        {
            if (touched != null)
            {
                if (touched != source)
                {
                    if (touched.tag == "Mirror")
                        touched.GetComponentInParent<Reflective>().DestroyBeam();
                    if (touched.tag == "Water")
                        touched.GetComponent<Refractive>().DestroyBeam();

                }
            }
        }
    }



    public void ShootLaser()
    {

        Ray ray = new Ray(position, forward);
        GetComponent<Collider>().enabled = false;
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        if (hitInfo.collider != null)
        {

            switch (hitInfo.transform.tag)
            {
                case "Mirror":
                    if (!hitInfo.transform.IsChildOf(source.transform))
                    {
                        hitInfo.transform.gameObject.GetComponentInParent<Reflective>().ToggleBeam(gameObject, hitInfo.point, Utility.Reflect(forward, hitInfo.normal), color);
                    }
                    break;
                case "Water":

                    if (Utility.isRefracted(forward, hitInfo.normal, indiceIncident, indiceEau))
                    {
                        if (hitInfo.transform != source.transform)
                        {

                            hitInfo.transform.gameObject.GetComponent<Refractive>().ToggleBeam(gameObject, hitInfo.point, Utility.Refract(forward, hitInfo.normal, indiceIncident, indiceEau), basicColor, indiceEau);
                        }

                    }
                    break;

                case "Target":
                    if (winner)
                    {
                        hitInfo.transform.gameObject.GetComponent<Winning>().Win();
                    }
                    break;
                default:
                    break;
            }
            if (touched != null && !touched.Equals(hitInfo.transform.gameObject))
                if (!touched.Equals(source))
                {
                    if (touched.tag == "Mirror")
                        touched.GetComponentInParent<Reflective>().DestroyBeam();
                    if (touched.tag == "Water")
                        touched.GetComponent<Refractive>().DestroyBeam();

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
        
        GetComponent<Collider>().enabled = true;
    }
}
