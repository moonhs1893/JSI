using UnityEngine;

namespace X {
    public abstract class XApp : MonoBehaviour {
        public abstract XScenarioMgr getScenarioMgr();
        public abstract XLogMgr getLogMgr();
    }
}
