using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIEmptyScenario : XScenario {
        public class EmptyScene : JSIScene {
            // singleton pattern 
            private static EmptyScene mSingleton = null;
            public static EmptyScene getSingleton() {
                Debug.Assert(EmptyScene.mSingleton != null);
                return EmptyScene.mSingleton;
            }
            public static EmptyScene createSingleton(XScenario scenario) {
                Debug.Assert(EmptyScene.mSingleton == null);
                EmptyScene.mSingleton = new EmptyScene(scenario);
                return EmptyScene.mSingleton;
            }
            private EmptyScene(XScenario scenario) : base(scenario) {
            }

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
            }

            public override void handlePenUp(Vector2 pt) {
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
}