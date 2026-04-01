using JSI.Geom;
using UnityEngine;

namespace JSI.AppObject {
    public class JSIAppCircle2D : JSIAppGeom2D {
        // constants
        public static readonly int NUM_SIDE = 64;

        // fields
        public void setRadius(float r) {
            JSICircle2D circle = (JSICircle2D)this.mGeom;
            this.mGeom = new JSICircle2D(r, circle.getPos(), circle.getRot());
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red; // easily noticeable color
        public void setColor(Color c) {
            this.mColor = c;
            this.refreshRenderer();
        }

        // constructor 
        public JSIAppCircle2D(string name, float radius, Color color) : 
            base($"{ name }/Circle2D") {

            this.mGeom = new JSICircle2D(radius, Vector2.zero,
                Quaternion.identity);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<CircleCollider2D>();
        }

        protected override void refreshRenderer() {
            JSICircle2D circle = (JSICircle2D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = circle.calcMesh(JSIAppCircle2D.NUM_SIDE);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            JSICircle2D circle = (JSICircle2D)this.mGeom;
            CircleCollider2D cc =
                this.mGameObject.GetComponent<CircleCollider2D>();
            cc.radius = circle.getRadius();
        }
    }
}