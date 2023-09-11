using System.Collections.Generic;
using UnityEngine;

public static class CirclePointsGenerator
{
    /// <summary>
    /// �~�i���j�㗐�����W���Z�o
    /// </summary>
    /// <param name="radius">�~�̔��a</param>
    /// <param name="center">�~�̒��S</param>
    /// <param name="numPoints">�擾���������W�̐�</param>
    /// <param name="minAngle">�_�Ɠ_�̊Ԋu(�p�x)</param>
    /// <returns></returns>
    public static List<Vector2> GeneratePoints(float radius, Vector2 center, int numPoints, float minAngle)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < numPoints; i++)
        {
            Vector2 point;//���W
            float angle;//�����p�x
            bool isValid;//��������
            do
            {
                isValid = true;
                angle = Random.Range(0f, 2f * Mathf.PI);//�����p�x����
                point = new Vector2(center.x + radius * Mathf.Cos(angle), center.y + radius * Mathf.Sin(angle));//���W�擾
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
    /// �~�̒��S����_�܂ł̎ː��̕������Z�o(return �p�x)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static float GetPointAngle(Vector2 center, Vector2 point)
    {
        float angle = Mathf.Atan2(point.y - center.y, point.x - center.x);//���W�A��

        float angleInDegrees = angle * 180f / Mathf.PI;//�p�x
        //angleInDegrees = (angleInDegrees + 360) % 360;//0����R�U�O�܂ł͈͓̔��Ɋm��
        return angleInDegrees;
    }
}
