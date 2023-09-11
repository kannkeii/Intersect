using System.Collections.Generic;
using UnityEngine;

public static class CirclePointsGenerator
{
    /// <summary>
    /// ‰~iüjã—”À•W‚ğZo
    /// </summary>
    /// <param name="radius">‰~‚Ì”¼Œa</param>
    /// <param name="center">‰~‚Ì’†S</param>
    /// <param name="numPoints">æ“¾‚µ‚½‚¢À•W‚Ì”</param>
    /// <param name="minAngle">“_‚Æ“_‚ÌŠÔŠu(Šp“x)</param>
    /// <returns></returns>
    public static List<Vector2> GeneratePoints(float radius, Vector2 center, int numPoints, float minAngle)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < numPoints; i++)
        {
            Vector2 point;//À•W
            float angle;//—”Šp“x
            bool isValid;//¬Œ÷”»’è
            do
            {
                isValid = true;
                angle = Random.Range(0f, 2f * Mathf.PI);//—”Šp“x¶¬
                point = new Vector2(center.x + radius * Mathf.Cos(angle), center.y + radius * Mathf.Sin(angle));//À•Wæ“¾
                foreach (Vector2 p in points)
                {
                    float angleBetweenPoints = Vector2.Angle(p - center, point - center);
                    if (angleBetweenPoints < minAngle)
                    {
                        isValid = false;
                        break;
                    }
                }
            } while (!isValid);
            points.Add(point);
        }
        return points;
    }

    /// <summary>
    /// ‰~‚Ì’†S‚©‚ç“_‚Ü‚Å‚ÌËü‚Ì•ûŒü‚ğZo(return Šp“x)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static float GetPointAngle(Vector2 center, Vector2 point)
    {
        float angle = Mathf.Atan2(point.y - center.y, point.x - center.x);//ƒ‰ƒWƒAƒ“

        float angleInDegrees = angle * 180f / Mathf.PI;//Šp“x
        //angleInDegrees = (angleInDegrees + 360) % 360;//0‚©‚ç‚R‚U‚O‚Ü‚Å‚Ì”ÍˆÍ“à‚ÉŠm•Û
        return angleInDegrees;
    }
}
