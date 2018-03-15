using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointGizmoEditor {
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
	private static void DrawPointGizmos(Point i_point, GizmoType i_gizmoType)
    {
        Color color = Color.white;

        //選択されていないときは色をタイプごとに変える
        if ((i_gizmoType & GizmoType.NonSelected) != 0)
        {
            switch (i_point.Type)
            {
                case Point.Etype.Item:
                    color = Color.green;
                    break;
                case Point.Etype.Magic:
                    color = Color.cyan;
                    break;
                case Point.Etype.Trap:
                    color = Color.yellow;
                    break;
                default:
                    break;
            }
        }
        Gizmos.color = color;

        Vector3 pos = i_point.transform.position;
        Gizmos.DrawSphere(pos, 0.1f);
        Gizmos.DrawWireSphere(pos, i_point.Radius);
    }
}
