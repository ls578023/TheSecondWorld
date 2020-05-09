using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameFramework
{
    [AddComponentMenu("UI/Effects/Text Vertical Gradient Color")]
    [RequireComponent(typeof(Text))]
    public class TextVerticalGradientThreeColor : BaseMeshEffect
    {
        public Color colorTop = Color.red;
        public Color colorCenterTop = Color.red;
        public Color colorCenter = Color.blue;
        public Color colorBottomTop = Color.red;
        public Color colorBottom = Color.green;

        public bool MultiplyTextColor = false;

        [RangeAttribute(0, 1)]
        public float centerPoint = 0.3f;
        public static float CorolcenterPoint = 0.8f;
        protected TextVerticalGradientThreeColor()
        {

        }

        public static Color32 Multiply(Color32 a, Color32 b)
        {
            a.r = (byte)((a.r * b.r) >> 8);
            a.g = (byte)((a.g * b.g) >> 8);
            a.b = (byte)((a.b * b.b) >> 8);
            a.a = (byte)((a.a * b.a) >> 8);
            return a;
        }

        private void ModifyVertices(VertexHelper vh)
        {
            List<UIVertex> verts = new List<UIVertex>(vh.currentVertCount);

            vh.GetUIVertexStream(verts);
            vh.Clear();
            int step = 6;
            for (int i = 0; i < verts.Count; i += step)
            {
                //6 point
                var tl = multiplyColor(verts[i + 0], colorTop);
                var tr = multiplyColor(verts[i + 1], colorTop);
                var bl = multiplyColor(verts[i + 4], colorBottom);
                var br = multiplyColor(verts[i + 3], colorBottom);

                var cttl = calcCenterVertex(verts[i + 0], verts[i + 4], 0.85f, colorTop);
                var cttr = calcCenterVertex(verts[i + 1], verts[i + 3], 0.85f, colorTop);

                var ctl = calcCenterVertex(verts[i + 0], verts[i + 4], 0.5f, colorCenterTop);
                var ctr = calcCenterVertex(verts[i + 1], verts[i + 3], 0.5f, colorCenterTop);


                var ccl = calcCenterVertex(verts[i + 0], verts[i + 4], 0.3f, colorCenter);
                var ccr = calcCenterVertex(verts[i + 1], verts[i + 3], 0.3f, colorCenter);

                var cBl = calcCenterVertex(verts[i + 0], verts[i + 4], 0.1f, colorBottomTop);
                var cBr = calcCenterVertex(verts[i + 1], verts[i + 3], 0.1f, colorBottomTop);


                vh.AddVert(tl);
                vh.AddVert(tr);
                vh.AddVert(cttr);
                vh.AddVert(cttr);
                vh.AddVert(cttl);
                vh.AddVert(tl);

                vh.AddVert(cttl);
                vh.AddVert(cttr);
                vh.AddVert(ctr);
                vh.AddVert(ctr);
                vh.AddVert(ctl);
                vh.AddVert(cttl);

                vh.AddVert(ctl);
                vh.AddVert(ctr);
                vh.AddVert(ccr);
                vh.AddVert(ccr);
                vh.AddVert(ccl);
                vh.AddVert(ctl);

                vh.AddVert(ccl);
                vh.AddVert(ccr);
                vh.AddVert(cBr);
                vh.AddVert(cBr);
                vh.AddVert(cBl);
                vh.AddVert(ccl);


                vh.AddVert(cBl);
                vh.AddVert(cBr);
                vh.AddVert(br);
                vh.AddVert(br);
                vh.AddVert(bl);
                vh.AddVert(cBl);
            }
            for (int i = 0; i < vh.currentVertCount; i += 30)
            {
                vh.AddTriangle(i + 0, i + 1, i + 2);
                vh.AddTriangle(i + 3, i + 4, i + 5);
                vh.AddTriangle(i + 6, i + 7, i + 8);
                vh.AddTriangle(i + 9, i + 10, i + 11);
                vh.AddTriangle(i + 12, i + 13, i + 14);
                vh.AddTriangle(i + 15, i + 16, i + 17);
                vh.AddTriangle(i + 18, i + 19, i + 20);
                vh.AddTriangle(i + 21, i + 22, i + 23);
                vh.AddTriangle(i + 24, i + 25, i + 26);
                vh.AddTriangle(i + 27, i + 28, i + 29);
            }

        }

        private UIVertex multiplyColor(UIVertex vertex, Color color)
        {
            if (MultiplyTextColor)
                vertex.color = Multiply(vertex.color, color);
            else
                vertex.color = color;
            return vertex;
        }

        private UIVertex calcCenterVertex(UIVertex top, UIVertex bottom,float PointPos, Color Color)
        {
            UIVertex center =new UIVertex();
            center.position = new Vector3(top.position.x, (top.position.y - bottom.position.y) * PointPos + bottom.position.y);
            center.uv0 = new Vector2((top.uv0.x - bottom.uv0.x) * PointPos + bottom.uv0.x, (top.uv0.y - bottom.uv0.y) * PointPos + bottom.uv0.y);
            center.color = Color;

            return center;
        }

        #region implemented abstract members of BaseMeshEffect

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!this.IsActive())
            {
                return;
            }


            ModifyVertices(vh);
        }

        #endregion
    }
}