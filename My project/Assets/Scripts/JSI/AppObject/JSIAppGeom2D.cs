using JSI.Geom;
using UnityEngine;

namespace JSI.AppObject {
    public abstract class JSIAppGeom2D : JSIAppObject2D {
        // fields
        protected JSIGeom2D mGeom = null;
        public JSIGeom2D getGeom() {
            return this.mGeom;
        }
        public void setGeom(JSIGeom2D geom) {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }
        public Collider2D getCollider() {
            Collider2D collider = this.mGameObject.GetComponent<Collider2D>();
            return collider;
        }

        // constructor
        public JSIAppGeom2D(string name) : base(name) {
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