
using UnityEngine;
using UnityEngine.EventSystems;

public class Reflective : MonoBehaviour
{
    public bool winner;

    /* Add : array of materials and light colors */
    public GameObject[] laserColors = new GameObject[7];
    private char basicColor = 'w';
    private char color = 'w';
    /* endAdd */

    private float indiceEau = 1.39336f;
    private float maxDistance;
    private GameObject beam;
    private GameObject source;
    private bool isReflecting = false;
    private GameObject touched;
    private Vector3 position;
    private Vector3 forward;

    private void Start()
    {
        maxDistance = 100f;
    }



    public void ToggleBeam(GameObject source, Vector3 position, Vector3 forward, char color)
    {
        this.position = position;
        this.forward = forward;
        this.color = color;
        this.source = source;
        colorManager();
    }
    public void ToggleBeam()
    {
        if (isReflecting)
        {
            colorManager();
        }

    }

    public void DestroyBeam()
    {
        if (isReflecting)
        {

            if (beam != null)
                Destroy(beam);
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
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {
            switch (hitInfo.transform.tag)
            {
                case "Mirror":
                    if (!hitInfo.transform.IsChildOf(source.transform))
                    {
                        hitInfo.transform.gameObject.GetComponentInParent<Reflective>().ToggleBeam(this.gameObject, hitInfo.point, Utility.Reflect(forward, hitInfo.normal), color);
                    }
                    break;

                case "Water":
                    if (Utility.isRefracted(forward, hitInfo.normal, 1f, indiceEau))
                    {
                        if (hitInfo.transform != source.transform)
                        {

                            hitInfo.transform.gameObject.GetComponent<Refractive>().ToggleBeam(this.gameObject, hitInfo.point, Utility.Refract(forward, hitInfo.normal, 1f, indiceEau), basicColor, 1f);
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

            if (touched != null && touched != hitInfo.transform.gameObject)
            {
                if (touched != source)
                {
                    if (touched.tag == "Mirror")
                        touched.GetComponentInParent<Reflective>().DestroyBeam();
                    if (touched.tag == "Water")
                        touched.GetComponent<Refractive>().DestroyBeam();

                }
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

    private void colorManager()
    {
        /* 
         * - Recuperer la couleur du miroir
         * - r g b y c m w (couleur du laser)
         * - soustrair a la couleur du laser celle du miroir.
         */
        bool[] mirorColor = creatColorTab(getMirorColor());
        bool[] laserColor = creatColorTab(color);
        laserColor = substractColor(mirorColor, laserColor);
        color = backToColor(laserColor);

        if (!isReflecting)
        {
            Destroy(beam);
            switch (color)
            {
                case 'w':
                    beam = Instantiate(laserColors[0]);
                    break;
                case 'r':
                    beam = Instantiate(laserColors[1]);
                    break;
                case 'g':
                    beam = Instantiate(laserColors[2]);
                    break;
                case 'b':
                    beam = Instantiate(laserColors[3]);
                    break;
                case 'y':
                    beam = Instantiate(laserColors[4]);
                    break;
                case 'c':
                    beam = Instantiate(laserColors[5]);
                    break;
                case 'm':
                    beam = Instantiate(laserColors[6]);
                    break;
                default:
                    return;
            }
            ShootLaser();
            isReflecting = true;
        }

    }

    private bool[] creatColorTab(char color)
    {
        bool[] tabC = new bool[3];
        switch (color)
        {
            case 'r':
                tabC[0] = true;
                break;
            case 'g':
                tabC[1] = true;
                break;
            case 'b':
                tabC[2] = true;
                break;
            case 'y':
                tabC[0] = true;
                tabC[1] = true;
                break;
            case 'c':
                tabC[1] = true;
                tabC[2] = true;
                break;
            case 'm':
                tabC[0] = true;
                tabC[2] = true;
                break;
            case 'w':
                tabC[0] = true;
                tabC[1] = true;
                tabC[2] = true;
                break;
        }

        return tabC;
    }

    private char backToColor(bool[] tabC)
    {
        char color = 'v'; /* void, no color -> [false, false, false] */
        if (tabC[0])
        {
            if (tabC[1])
            {
                if (tabC[2])
                {
                    color = 'w';
                }
                else
                {
                    color = 'y';
                }
            }
            else
            {
                if (tabC[2])
                {
                    color = 'm';
                }
                else
                {
                    color = 'r';
                }
            }
        }
        else
        {
            if (tabC[1])
            {
                if (tabC[2])
                {
                    color = 'c';
                }
                else
                {
                    color = 'g';
                }
            }
            else
            {
                if (tabC[2])
                {
                    color = 'b';
                }
                else
                {
                    Destroy(beam);
                }
            }
        }

        return color;
    }

    private char getMirorColor()
    {
        char colorM = 'w';
        GameObject mirrors = transform.Find("MirrorTop").gameObject;
        int colorNumber = 0;
        if (mirrors == null) /* /!\ peut peut etre poser des soucis? */
        {
            return colorM;
        }
        for (int i = 0; i < mirrors.transform.childCount; i++)
        {
            if (mirrors.transform.GetChild(i).gameObject.activeSelf == true)
            {
                colorNumber = i;
                break;
            }
        }
        switch (colorNumber)
        {
            case 0:
                colorM = 'w';
                break;
            case 1:
                colorM = 'r';
                break;
            case 2:
                colorM = 'g';
                break;
            case 3:
                colorM = 'b';
                break;
            case 4:
                colorM = 'y';
                break;
            case 5:
                colorM = 'c';
                break;
            case 6:
                colorM = 'm';
                break;
            default:
                break;
        }
        return colorM;
    }

    private bool[] substractColor(bool[] l, bool[] m)
    {
        bool[] result = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            if (l[i] && m[i])
            {
                result[i] = true;
            }
        }
        return result;
    }

}




