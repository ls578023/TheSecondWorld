using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 不规则点击区域图片
/// </summary>
[RequireComponent(typeof(PolygonCollider2D))]
public class UIPolygonImage : Image
{
    private PolygonCollider2D areaPolygon;

    protected UIPolygonImage()
    {
        useLegacyMeshGeneration = true;
    }

    private PolygonCollider2D Polygon
    {
        get
        {
            if (areaPolygon != null)
                return areaPolygon;

            areaPolygon = GetComponent<PolygonCollider2D>();
            return areaPolygon;
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return Polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        transform.localPosition = Vector3.zero;
        var w = rectTransform.sizeDelta.x * 0.5f + 0.1f;
        var h = rectTransform.sizeDelta.y * 0.5f + 0.1f;
        Polygon.points = new[]
        {
            new Vector2(-w, -h),
            new Vector2(w, -h),
            new Vector2(w, h),
            new Vector2(-w, h)
        };
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(UIPolygonImage), true)]
public class CustomRaycastFilterInspector : Editor
{
    public override void OnInspectorGUI()
    {
    }
}

public class NonRectAngularButtonImageHelper
{
    [MenuItem("GameObject/★UI扩展★/★增加组件★/UIPolygonImage", false, 10)]
    public static void CreateNonRectAngularButtonImage()
    {
        var goRoot = Selection.activeGameObject;
        if (goRoot == null)
            return;

        var button = goRoot.GetComponent<Button>();

        if (button == null)
        {
            Debug.Log("Selecting Object is not a button!");
            return;
        }

        // 关闭原来button的射线检测
        var graphics = goRoot.GetComponentsInChildren<Graphic>();
        foreach (var graphic in graphics)
        {
            graphic.raycastTarget = false;
        }

        var polygon = new GameObject("UIPolygonImage");
        polygon.AddComponent<PolygonCollider2D>();
        UIPolygonImage image = polygon.AddComponent<UIPolygonImage>();
        polygon.transform.SetParent(goRoot.transform, false);        
        polygon.transform.SetAsLastSibling();
        image.rectTransform.anchorMin = Vector2.zero;
        image.rectTransform.anchorMax = Vector2.one;
        image.rectTransform.offsetMin = Vector2.zero;
        image.rectTransform.offsetMax = Vector2.zero;
    }
}
#endif
