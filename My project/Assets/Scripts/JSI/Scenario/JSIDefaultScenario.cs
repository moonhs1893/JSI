using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIDefaultScenario : XScenario {
        // singleton pattern 
        private static JSIDefaultScenario mSingleton = null;
        public static JSIDefaultScenario getSingleton() {
            Debug.Assert(JSIDefaultScenario.mSingleton != null);
            return JSIDefaultScenario.mSingleton;
        }
        public static JSIDefaultScenario createSingleton(XApp app) {
            Debug.Assert(JSIDefaultScenario.mSingleton == null);
            JSIDefaultScenario.mSingleton = new JSIDefaultScenario(app);
            return JSIDefaultScenario.mSingleton;
        }
        private JSIDefaultScenario(XApp app) : base(app) {
        }

        protected override void addScenes() {
            this.addScene(JSIDefaultScenario.ReadyScene.createSingleton(this));
        }
    }
}