/*
e.g.
{
    "time": "#",
    "scenario": "#",
    "scene": "#",
    "cmd": "#",
    "timeTakenInMs": #,
    "data": {
        "member1": #,
        "member2": #,
        ...
    }
}
*/
namespace X {
    public class XLog : XJson {
        public XLog(string time, string scenario, string scene, string cmd,
            double timeTakenInMs, XJson data) : base() {

            this.addMember("time", time);
            this.addMember("scenario", scenario);
            this.addMember("scene", scene);
            this.addMember("cmd", cmd);
            this.addMember("timeTakenInMs", timeTakenInMs);
            this.addMember("data", data);
        }
    }
}