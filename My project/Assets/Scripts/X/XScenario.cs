using System.Collections.Generic;

namespace X {
    public abstract class XScenario {
        // fields
        protected XApp mApp = null;
        public XApp getApp() {
            return this.mApp;
        }
        protected List<XScene> mScenes = null;
        protected XScene mInitScene = null;
        public XScene getInitScene() {
            return this.mInitScene;
        }
        public void setInitScene(XScene initScene) {
            this.mInitScene = initScene;
        }

        // constructor
        protected XScenario(XApp app) {
            this.mApp = app;
            this.mScenes = new List<XScene>();
            this.addScenes();
        }

        // abstract methods
        protected abstract void addScenes();

        // concrete methods
        protected void addScene(XScene scene) {
            this.mScenes.Add(scene);
        }
        public string getName() {
            return this.GetType().Name;
        }
    }
}