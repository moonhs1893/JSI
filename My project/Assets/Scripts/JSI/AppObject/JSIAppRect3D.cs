using JSI.Geom;
using UnityEngine;

namespace JSI.AppObject {
    public class JSIAppRect3D : JSIAppGeom3D {
        // fields
        public void setSize(float width, float height) {
            JSIRect3D rect = (JSIRect3D)this.mGeom;
            this.mGeom = new JSIRect3D(width, height, rect.getPos(),
                rect.getRot());
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red; // easily noticeable color
        public void setColor(Color c) {
            this.mColor = c;
            this.refreshRenderer();
        }

        // constructor 
        public JSIAppRect3D(string name, float width, float height,
            Color color) : base($"{ name }/Rect3D") {

            this.mGeom = new JSIRect3D(width, height, Vector3.zero,
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
            JSIRect3D rect = (JSIRect3D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = rect.calcMesh();
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