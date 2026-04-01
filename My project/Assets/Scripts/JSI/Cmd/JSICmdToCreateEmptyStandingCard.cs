//using System.Text;
//using X;
//using UnityEngine;

//namespace JSI.Cmd {
//    public class JSICmdToCreateEmptyStandingCard : XLoggableCmd {
//        // fields
//        // ...

//        // private constructor
//        private JSICmdToCreateEmptyStandingCard(XApp app) : base(app) {
//        }

//        // static method to construct and execute this command
//        public static bool execute(XApp app) {
//            JSICmdToCreateEmptyStandingCard cmd = 
//                new JSICmdToCreateEmptyStandingCard(app);
//            return cmd.execute();
//        }

//        protected override bool defineCmd() {
//            JSIApp jsi = (JSIApp)this.mApp;
//            JSIPerspCameraPerson cp = jsi.getPerspCameraPerson();

//            // calculate the normal vector of the card plane. 
//            Vector3 normalDir =
//                Vector3.ProjectOnPlane(-cp.getView(), Vector3.up).normalized;

//            // define card dimensions. 
//            float cardWidth = 1f;
//            float cardHeight = 2f;
//            Vector3 cardCenter = new Vector3(0f, cardHeight / 2f, 0f);
//            Vector3 cardZDir = -normalDir;
//            Quaternion rot = Quaternion.LookRotation(cardZDir, Vector3.up);

//            // crate a new standing card.
//            JSIStandingCard sc = new JSIStandingCard("EmptyStandingCard",
//                cardWidth, cardHeight, cardCenter, rot, null);

//            // add the standing card to its manager. 
//            jsi.getStandingCardMgr().getStandingCards().Add(sc);

//            return true;
//        }

//        protected override string createLog() {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(this.GetType().Name).Append("\t");
//            return sb.ToString();
//        }

//    }
//}