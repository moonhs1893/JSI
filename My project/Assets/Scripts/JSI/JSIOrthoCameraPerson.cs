using UnityEngine;

namespace JSI {
    public class JSIOrthoCameraPerson : JSICameraPerson {
        // constants 
        public static readonly float NEAR = 0.1f; // in meter (10 cm)
        public static readonly float FAR = 2f; // in meter (2 m)
        public static readonly float SCREEN_CAMERA_DIST = 1.0f; // in meter

        // fields
        private float mScreenWidth = float.NaN;
        private float mScreenHeight = float.NaN;

        // constructor 
        public JSIOrthoCameraPerson() : base("PerspCameraPerson") {
        }

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = true;
            this.mCamera.depth = 1f; // rendering order
            this.mCamera.clearFlags = CameraClearFlags.Depth;
            this.mCamera.cullingMask = 32; // 100000. UI layer only

            this.mCamera.nearClipPlane = JSIOrthoCameraPerson.NEAR;
            this.mCamera.farClipPlane = JSIOrthoCameraPerson.FAR;
        }

        protected override void defineExternalCameraParameters() {
            this.update();
        }

        public void update() {
            if (Screen.width != this.mScreenWidth ||
                Screen.height != this.mScreenHeight) {

                // update the screen size.
                this.mScreenWidth = Screen.width;
                this.mScreenHeight = Screen.height;

                // update the screen camera.
                this.mCameraRig.getGameObject().transform.position =
                    new Vector3(this.mScreenWidth / 2f,
                        this.mScreenHeight / 2f,
                        -JSIOrthoCameraPerson.SCREEN_CAMERA_DIST);
                this.mCamera.orthographicSize = this.mScreenHeight / 2f;
            }
        }
    }
}