using JSI.Geom;
using UnityEngine;

namespace JSI.AppObject {
    public abstract class JSIAppGeom3D : JSIAppObject3D {
        // fields
        protected JSIGeom3D mGeom = null;
        public JSIGeom3D getGeom() {
            return this.mGeom;
        }
        public void setGeom(JSIGeom3D geom) {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }
        public Collider getCollider() {
            Collider collider = this.mGameObject.GetComponent<Collider>();
            return collider;
        }

        // constructor
        public JSIAppGeom3D(string name) : base(name) {
        }

        // methods
        public void refreshAtGeomChange() {
            this.refreshRenderer();
            this.refreshCollider();
        }
        protected abstract void refreshRenderer();
        protected abstract void refreshCollider();
    }
}