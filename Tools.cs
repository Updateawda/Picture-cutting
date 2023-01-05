using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    //判断图片切割是否完整
    public static bool InnerGraphByAngle(Vector2 point, Vector2[] polygon)
    {
        List<Vector2> edges = new List<Vector2>();
        for (int i = 0; i < polygon.Length; i++)
        {
            edges.Add(polygon[i] - point);
        }
        float angle = 0;
        for (int i = 0; i < edges.Count-1; i++)
        {
            angle += Vector2.Angle(edges[i], edges[i + 1]);
        }
        angle += Vector2.Angle(edges[edges.Count-1], edges[0]);
        return angle>359&&angle<361;
    }
    //判断两个线段是否有交点
    public static bool GetCrossPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 result)
    {
        result = Vector2.zero;
        /** 1 解线性方程组, 求线段交点. **/
        // 如果分母为0 则平行或共线, 不相交  
        double denominator = (b.y - a.y) * (d.x - c.x) - (a.x - b.x) * (c.y - d.y);
        if (denominator == 0)
        {
            return false;
        }

        // 线段所在直线的交点坐标 (x , y)      
        double x = ((b.x - a.x) * (d.x - c.x) * (c.y - a.y)
                    + (b.y - a.y) * (d.x - c.x) * a.x
                    - (d.y - c.y) * (b.x - a.x) * c.x) / denominator;
        double y = -((b.y - a.y) * (d.y - c.y) * (c.x - a.x)
                    + (b.x - a.x) * (d.y - c.y) * a.y
                    - (d.x - c.x) * (b.y - a.y) * c.y) / denominator;

        /** 2 判断交点是否在两条线段上 **/
        if (
            // 交点在线段1上  
            (x - a.x) * (x - b.x) <= 0 && (y - a.y) * (y - b.y) <= 0
             // 且交点也在线段2上  
             && (x - c.x) * (x - d.x) <= 0 && (y - c.y) * (y - d.y) <= 0
            )
        {
            result = new Vector2((float)x, (float)y);
            // 返回交点p  
            return true;
        }
        //否则不相交  
        return false;


    }

}
