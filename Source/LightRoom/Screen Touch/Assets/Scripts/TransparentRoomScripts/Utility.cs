using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{

    public static bool isRefracted(Vector3 inc, Vector3 norm, float IRefract, float ORefract)
    {
        float alpha = IRefract / ORefract;
        float cos1 = Vector3.Dot(norm, -inc);
        float cos2 = Mathf.Sqrt(1 - alpha * alpha * (1 - cos1 * cos1));
        return cos2 > 0;
    }

    public static Vector3 Refract(Vector3 inc, Vector3 norm, float IRefract, float ORefract)
    {
        float alpha = IRefract / ORefract;
        float cos1 = Vector3.Dot(norm, -inc);
        float cos2 = Mathf.Sqrt(1 - alpha * alpha * (1 - cos1 * cos1));
        if (cos1 > 0)
            return alpha * inc + (alpha * cos1 - cos2) * norm;
        else
            return alpha * inc + (alpha * cos1 + cos2) * norm;
    }

    public static Vector3 Reflect(Vector3 inc, Vector3 norm)
    {
        return inc + 2 * Vector3.Dot(norm, -inc) * norm;
    }

    
}
