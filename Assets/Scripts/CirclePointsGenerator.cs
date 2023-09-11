using System.Collections.Generic;
using UnityEngine;

public static class CirclePointsGenerator
{
    /// <summary>
    /// 円（線）上乱数座標を算出
    /// </summary>
    /// <param name="radius">円の半径</param>
    /// <param name="center">円の中心</param>
    /// <param name="numPoints">取得したい座標の数</param>
    /// <param name="minAngle">点と点の間隔(角度)</param>
    /// <returns></returns>
    public static List<Vector2> GeneratePoints(float radius, Vector2 center, int numPoints, float minAngle)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < numPoints; i++)
        {
            Vector2 point;//座標
            float angle;//乱数角度
            bool isValid;//成功判定
            do
            {
                isValid = true;
                angle = Random.Range(0f, 2f * Mathf.PI);//乱数角度生成
                point = new Vector2(center.x + radius * Mathf.Cos(angle), center.y + radius * Mathf.Sin(angle));//座標取得
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
    /// 円の中心から点までの射線の方向を算出(return 角度)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static float GetPointAngle(Vector2 center, Vector2 point)
    {
        float angle = Mathf.Atan2(point.y - center.y, point.x - center.x);//ラジアン

        float angleInDegrees = angle * 180f / Mathf.PI;//角度
        
        return angleInDegrees;
    }
}
