using JSI.AppObject;
using UnityEngine;

namespace JSI {
    public abstract class JSICameraPerson {
        // fields
        protected JSIAppNoGeom3D mCameraRig = null;
        public JSIAppNoGeom3D getCameraRig() {
            return this.mCameraRig;
        }
        protected Camera mCamera = null;
        public Camera getCamera() {
            return this.mCamera;
        }

        // constructor
        public JSICameraPerson(string name) {
            this.mCameraRig = new JSIAppNoGeom3D("CameraRig");
            this.mCameraRig.getGameObject().AddComponent<Camera>();
            this.mCamera = 
                this.mCameraRig.getGameObject().GetComponent<Camera>();
            this.defineInternalCameraParameters(); // camera attributes
            this.defineExternalCameraParameters(); // camera rig attributes
        }

        protected abstract void defineInternalCameraParameters();
        protected abstract void defineExternalCameraParameters();

        // utility methods
        public Vector3 getEye() {
            return this.mCameraRig.getGameObject().transform.position;
        }
        public void setEye(Vector3 eye) {
            this.mCameraRig.getGameObject().transform.position = eye;
        }
        public Vector3 getView() {
            return this.mCameraRig.getGameObject().transform.forward;
        }
        public void setView(Vector3 view) {
            this.mCameraRig.getGameObject().transform.rotation =
                Quaternion.LookRotation(view, Vector3.up);
        }
        public Vector3 getUp() {
            return this.mCameraRig.getGameObject().transform.up;
        }
        public Vector3 getRight() {
            return this.mCameraRig.getGameObject().transform.right;
        }
    }
}