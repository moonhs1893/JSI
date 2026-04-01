using JSI.Geom;
using UnityEngine;

namespace JSI.AppObject {
    public class JSIAppCircle3D : JSIAppGeom3D {
        // constants
        public static readonly int NUM_SIDE = 64;

        // fields
        public void setRadius(float r) {
            JSICircle3D circle = (JSICircle3D)this.mGeom;
            this.mGeom = new JSICircle3D(r, circle.getPos(), circle.getRot());
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red; // easily noticeable color
        public void setColor(Color c) {
            this.mColor = c;
            this.refreshRenderer();
        }

        // constructor 
        public JSIAppCircle3D(string name, float radius, Color color) : 
            base($"{ name }/Circle3D") {

            this.mGeom = new JSICircle3D(radius, Vector3.zero,
                Quaternion.identity);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshCollider>();
        }

        protected override void refreshRenderer() {
            JSICircle3D circle = (JSICircle3D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = circle.calcMesh(JSIAppCircle3D.NUM_SIDE);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            MeshCollider mc = this.mGameObject.GetComponent<MeshCollider>();
            mc.sharedMesh = this.mGameObject.GetComponent<MeshFilter>().mesh;
        }
    }
}