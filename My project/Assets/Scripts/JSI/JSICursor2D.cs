using JSI.AppObject;
using UnityEngine;

namespace JSI {
    public class JSICursor2D : JSIAppCircle2D {
        // constants
        public static readonly float RADIUS = 2f;
        //public static readonly Color COLOR = Color.clear;
        public static readonly Color COLOR = Color.red;

        // fields
        private JSIApp mJSI = null;

        // constructor
        public JSICursor2D(JSIApp jsi) : base("Cursor2D",
            JSICursor2D.RADIUS, JSICursor2D.COLOR) {
            this.mJSI = jsi;
        }
        
        // methods
        public bool intersects(JSIAppGeom2D appGeom) {
            return this.getCollider().IsTouching(appGeom.getCollider());
        }

        public bool hits(JSIAppGeom3D appGeom3D) {
            Vector2 ctr = this.mGameObject.transform.position;
            JSIPerspCameraPerson cp = this.mJSI.getPerspCameraPerson();
            Ray ray = cp.getCamera().ScreenPointToRay(ctr);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            if (collider.Raycast(ray, out hit, Mathf.Infinity)) {
                //JSIUtil.createDebugSphere(hit.point);
                return true;
            } else {
                return false;
            }
        }

        public RaycastHit calcHit(JSIAppGeom3D appGeom3D) {
            Vector2 ctr = this.mGameObject.transform.position;
            JSIPerspCameraPerson cp = this.mJSI.getPerspCameraPerson();
            Ray ray = cp.getCamera().ScreenPointToRay(ctr);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            collider.Raycast(ray, out hit, Mathf.Infinity);
            return hit;
        }
    }
}