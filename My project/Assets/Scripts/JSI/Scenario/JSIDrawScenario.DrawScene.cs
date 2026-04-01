using JSI.Cmd;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIDrawScenario : XScenario {
        public class DrawScene : JSIScene {
            // singleton pattern 
            private static DrawScene mSingleton = null;
            public static DrawScene getSingleton() {
                Debug.Assert(DrawScene.mSingleton != null);
                return DrawScene.mSingleton;
            }
            public static DrawScene createSingleton(XScenario scenario) {
                Debug.Assert(DrawScene.mSingleton == null);
                DrawScene.mSingleton = new DrawScene(scenario);
                return DrawScene.mSingleton;
            }
            private DrawScene(XScenario scenario) : base(scenario) {
            }

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToUpdateCurPtCurve2D.execute(jsi);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToAddCurPtCurve2DToPtCurve2Ds.execute(jsi);
                XCmdToChangeScene.execute(jsi, this.mReturnScene, null);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
}