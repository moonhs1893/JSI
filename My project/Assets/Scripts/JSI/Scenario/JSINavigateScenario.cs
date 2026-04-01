using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSINavigateScenario : XScenario {
        // singleton pattern 
        private static JSINavigateScenario mSingleton = null;
        public static JSINavigateScenario getSingleton() {
            Debug.Assert(JSINavigateScenario.mSingleton != null);
            return JSINavigateScenario.mSingleton;
        }
        public static JSINavigateScenario createSingleton(XApp app) {
            Debug.Assert(JSINavigateScenario.mSingleton == null);
            JSINavigateScenario.mSingleton = new JSINavigateScenario(app);
            return JSINavigateScenario.mSingleton;
        }
        private JSINavigateScenario(XApp app) : base(app) {
        }

        protected override void addScenes() {
            this.addScene(
                JSINavigateScenario.RotateReadyScene.createSingleton(this));
            this.addScene(
                JSINavigateScenario.TumbleCameraScene.createSingleton(this));
            this.addScene(
                JSINavigateScenario.TranslateReadyScene.createSingleton(this));
            this.addScene(
                JSINavigateScenario.DollyCameraScene.createSingleton(this));
        }
    }
}