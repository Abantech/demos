using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandJointsMeasurement
{
    public class SessionManager
    {
        private static Dictionary<string, Session> sessions = new Dictionary<string, Session>();

        public static string CreateSession(string operatorID)
        {
            Session newSession = new Session(operatorID);

            sessions.Add(newSession.SessionID, newSession);

            return newSession.SessionID;
        }

        public static ProcessedFrame GetLatestFrame(string ID)
        {
            return sessions[ID].GetLatestFrame<ProcessedFrame>();
        }

        public static void StartSession(string ID)
        {
            sessions[ID].Start();
        }

        public static bool HasSession(string ID)
        {
            return sessions.ContainsKey(ID) && sessions[ID].IsRunning;
        }

        public static SessionSummary EndSession(string sessionID)
        {
            return sessions[sessionID].End();
        }
    }
}
