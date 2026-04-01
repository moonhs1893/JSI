using JSI.File;

/*
Implementation
- each snapshot has the list of all standing cards visible at that momement
- each snapshot has the reference of the previous & next snapshot (linked list)
                                current
                                   v
o <--> o <--> o <--> o <--> o <--> o
- by performing undo, current snapshot pointer moves to previous snapshot
- by performing redo, current snapshot pointer moves to next snapshot
- when snapshot moves from one snapshot to another (e.g. A --> B),
  the delta between two snapshots can be found,
  and appropriate add & remove commands are executed
           current
              v
o <--> o <--> o <--> o <--> o <--> o
              B                    A
- when the user makes changes (add or remove standing cards) after undoing,
  a new snapshot is made, and that snapshot is made current snapshot
                  current
                     v
                /--> o
               /
o <--> o <--> o <--> o <--> o <--> o
*/
namespace JSI {
    public class JSISnapshotMgr {
        // constants
        private static readonly int MAX_NUM_SNAPSHOT = 30;

        // fields
        private JSIApp mJSI;
        private JSISnapshot mCurSnapshot;
        public JSISnapshot getCurSnapshot() {
            return this.mCurSnapshot;
        }

        // constructor
        public JSISnapshotMgr(JSIApp jsi) {
            this.mJSI = jsi;
            this.mCurSnapshot = new JSISnapshot(this.mJSI);
        }

        // methods
        public bool takeSnapshot() {
            JSISnapshot nextSnapshot = new JSISnapshot(this.mJSI);

            if (!this.mCurSnapshot.checkSnapshotIsSameAs(nextSnapshot)) {
                this.mCurSnapshot.setNextSnapshot(nextSnapshot);
                nextSnapshot.setPrevSnapshot(this.mCurSnapshot);
                this.mCurSnapshot = nextSnapshot;

                // set max number of snapshots
                if (this.countSnapshots() > JSISnapshotMgr.MAX_NUM_SNAPSHOT) {
                    this.calcOldestSnapshot().getNextSnapshot().setPrevSnapshot(
                        null);
                }

                return true;
            } else {
                return false;
            }
        }

        private int countSnapshots() {
            int count = 1;
            JSISnapshot snapshot = this.mCurSnapshot;
            while (snapshot.getPrevSnapshot() != null) {
                count++;
                snapshot = snapshot.getPrevSnapshot();
            }
            return count;
        }

        private JSISnapshot calcOldestSnapshot() {
            JSISnapshot snapshot = this.mCurSnapshot;
            while (snapshot.getPrevSnapshot() != null) {
                snapshot = snapshot.getPrevSnapshot();
            }
            return snapshot;
        }

        public bool undo() {
            if (this.mCurSnapshot.getPrevSnapshot() == null) {
                return false;
            }

            this.applySnapshot(this.mCurSnapshot.getPrevSnapshot());
            this.mCurSnapshot = this.mCurSnapshot.getPrevSnapshot();
            return true;
        }

        public bool redo() {
            if (this.mCurSnapshot.getNextSnapshot() == null) {
                return false;
            }

            this.applySnapshot(this.mCurSnapshot.getNextSnapshot());
            this.mCurSnapshot = this.mCurSnapshot.getNextSnapshot();
            return true;
        }

        public void restartHistory() {
            this.takeSnapshot();
            this.mCurSnapshot.setPrevSnapshot(null);
        }

        private void applySnapshot(JSISnapshot toSnapshot) {
            this.applyDiff(this.mCurSnapshot, toSnapshot);
        }

        private void applyDiff(JSISnapshot fromSnapshot, JSISnapshot toSnapshot) {
            // if "from snapshot" has cards that are not in "to snapshot",
            // remove them
            foreach (JSISerializableStandingCard sSc in fromSnapshot.
                standingCards) {

                if (!toSnapshot.containsStandingCard(sSc)) {
                    JSIStandingCard sc = this.mJSI.getStandingCardMgr().findById(
                        sSc.id);
                    this.mJSI.getStandingCardMgr().getStandingCards().Remove(sc);
                    sc.destroyGameObject();
                }
            }
            // if "to snapshot" has cards that are not in "from snapshot",
            // add them
            foreach (JSISerializableStandingCard sSc in toSnapshot.
                standingCards) {
                if (!fromSnapshot.containsStandingCard(sSc)) {
                    this.mJSI.getStandingCardMgr().getStandingCards().Add(sSc.
                        toStandingCard());
                }
            }
        }
    }
}