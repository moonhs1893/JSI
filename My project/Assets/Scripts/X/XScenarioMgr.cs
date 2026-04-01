using System.Collections.Generic;

namespace X {
    public abstract class XScenarioMgr {
        // fields
        protected XApp mApp = null;
        protected List<XScenario> mScenarios = null;
        protected XScene mCurScene = null;
        public XScene getCurScene() {
            return this.mCurScene;
        }
        public void setCurScene(XScene scene) {
            if (this.mCurScene != null) {
                this.mCurScene.wrapUp();
            }
            // current scene should be changed before calling getReady(),
            // because when commands execute in getReady(), the scenario/scene
            // logged would be that of the previous scene
            // also, upon app init, logged scenario/scene would be null
            this.mCurScene = scene;
            scene.getReady();
        }

        // contructor
        protected XScenarioMgr(XApp app) {
            this.mApp = app;
            this.mScenarios = new List<XScenario>();
            this.addScenarios();
            this.setInitCurScene();
        }

        // abstract methods
        protected abstract void addScenarios();
        protected abstract void setInitCurScene();

        // concrete methods
        protected void addScenario(XScenario scenario) {
            this.mScenarios.Add(scenario);
        }
    }
}
