using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIEmptyScenario : XScenario {
        // singleton pattern 
        private static JSIEmptyScenario mSingleton = null;
        public static JSIEmptyScenario getSingleton() {
            Debug.Assert(JSIEmptyScenario.mSingleton != null);
            return JSIEmptyScenario.mSingleton;
        }
        public static JSIEmptyScenario createSingleton(XApp app) {
            Debug.Assert(JSIEmptyScenario.mSingleton == null);
            JSIEmptyScenario.mSingleton = new JSIEmptyScenario(app);
            return JSIEmptyScenario.mSingleton;
        }
        private JSIEmptyScenario(XApp app) : base(app) {
        }

        protected override void addScenes() {
            this.addScene(JSIEmptyScenario.EmptyScene.createSingleton(this));
        }
    }
}