using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoidHelper
{
    const int numViewDirections2D = 54;
    public static readonly Vector2[] directions;

    static BoidHelper()
    {
        directions = new Vector2[BoidHelper.numViewDirections2D];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numViewDirections2D; i++)
        {
            //float t = (float)i / numViewDirections2D;
            //float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            //float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            //float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float x = Mathf.Cos(azimuth);
            float y = Mathf.Sin(azimuth);
            //float z = Mathf.Cos(inclination);
            directions[i] = new Vector2(x, y);
        }
    }
}
