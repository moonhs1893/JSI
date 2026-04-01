using JSI.Geom;
using System.Collections.Generic;
using UnityEngine;

namespace JSI.AppObject {
    public class JSIAppPolyline2D : JSIAppGeom2D {
        // fields
        private float mWidth = float.NaN;
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = Color.red; // easily noticeable color
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setPts(List<Vector2> pts) {
            this.mGeom = new JSIPolyline2D(pts);
            this.refreshAtGeomChange();
        }

        // constructor
        public JSIAppPolyline2D(string name, List<Vector2> pts, 
            float width, Color color) : base($"{ name }/Polyline2D") {

            this.mGeom = new JSIPolyline2D(pts);
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
            this.mGameObject.AddComponent<EdgeCollider2D>();
        }

        protected override void refreshRenderer() {
            JSIPolyline2D polyline = (JSIPolyline2D) this.mGeom;
            List<Vector2> pt2Ds = polyline.getPts();
            List<Vector3> pt3Ds = new List<Vector3>();
            for (int i = 0; i < pt2Ds.Count; i++) {
                Vector2 pt2D = pt2Ds[i];
                Vector3 pt3D = new Vector3(pt2D.x, pt2D.y, 0f);
                pt3Ds.Add(pt3D);
            }

            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera
            lr.positionCount = pt3Ds.Count;
            lr.SetPositions(pt3Ds.ToArray());
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("Unlit/Color"));
            lr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            JSIPolyline2D polyline = (JSIPolyline2D)this.mGeom;
            EdgeCollider2D ec = this.mGameObject.GetComponent<EdgeCollider2D>();
            ec.points = polyline.getPts().ToArray();
        }
    }
}