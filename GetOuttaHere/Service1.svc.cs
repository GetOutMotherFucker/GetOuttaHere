using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GetOuttaHere
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SignalService1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SignalService1.svc or SignalService1.svc.cs at the Solution Explorer and start debugging.
    public class SignalService1 : IService1
    {

        public string GetData()
        {
            return "Signal: Beep beep bitch";
        }

        WebOperationContext webContext = WebOperationContext.Current;
        public static int nextId = 0;
        private static List<Signal> signalList = new List<Signal>()
        {    
            new Signal(01,33)
        };

        public IList<Signal> GetCustomers()
        {
            return signalList;
        }

        public Signal GetSignal(string id)
        {
            webContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
            int idNumber = int.Parse(id);
            Signal s = signalList.FirstOrDefault(signal => signal.ID == idNumber);
            //Set statuscode of response
            if (s == null) webContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;

            //Alternatively 
            //if (c==null) webContext.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)404;
            //Any number can be used for tyupe casting, even customized numbers like 420

            return s;
        }

        public Signal DeleteSignal(string id)
        {
            Signal s = GetSignal(id);
            if (s == null) return null;
            signalList.Remove(s);
            return s;

        }

        public Signal InsertCustomer(Signal signal)
        {
            signal.ID = SignalService1.nextId++;
            signalList.Add(signal);
            return signal;
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
