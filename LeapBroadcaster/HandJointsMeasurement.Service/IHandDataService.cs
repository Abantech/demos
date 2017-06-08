using System.ServiceModel;
using System.ServiceModel.Web;

namespace HandJointsMeasurement.Service
{
    [ServiceContract]
    public interface IHandDataService
    { 
        [OperationContract]
        [WebGet(UriTemplate = "StartSession?operatorid={operatorID}")]
        SessionData StartSession(string operatorID);

        [OperationContract]
        [WebGet(UriTemplate = "EndSession?sessionid={sessionID}")]
        SessionSummary EndSession(string sessionID);

        [OperationContract]
        [WebGet(UriTemplate = "GetFrame?sessionid={sessionID}&includeimages={includeImages}&includedepth={includeDepth}")]
        ProcessedFrame GetFrame(string sessionID, bool includeImages, bool includeDepth);
    }
}
