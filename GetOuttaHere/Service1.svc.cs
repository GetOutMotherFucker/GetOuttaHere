using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private const string _connectionString = "Server=tcp:hassan-server.database.windows.net,1433;Initial Catalog=SchoolDB;Persist Security Info=False;User ID=hassanrh;Password=Hemmeligt2303;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<Signal> GetData()
        {
            SqlConnection connection = new SqlConnection(_connectionString); //laver en ny sql connection, som er en connectionstring 
            connection.Open(); // her åbner vi vores connection

            SqlCommand getAllelements = new SqlCommand("SELECT * FROM SensorData", connection);
            SqlDataReader reader = getAllelements.ExecuteReader();

            List<Signal> signal = new List<Signal>();
            while (reader.Read())
            {
                Signal temptsSignal = new Signal();

                temptsSignal.ID = reader.GetInt32(0);
                temptsSignal.Tid = reader.GetString(1);
                temptsSignal.SensorNumber = reader.GetInt32(2);

                signalList.Add(temptsSignal);
            }

            connection.Close();
            return signalList;
        }
        public Signal GetSpecificSignal(string id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand getAllElements = new SqlCommand($"SELECT * FROM SensorData WHERE id = {id}", connection);
            SqlDataReader reader = getAllElements.ExecuteReader();

            Signal temptsSignal = new Signal();
            List<Signal> ObjList = new List<Signal>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    temptsSignal.ID = reader.GetInt32(0);
                    temptsSignal.Tid = reader.GetString(1);
                    temptsSignal.SensorNumber = reader.GetInt32(2);

                    signalList.Add(temptsSignal);
                }
            }

            connection.Close();
            return temptsSignal;
        }

        WebOperationContext webContext = WebOperationContext.Current;
        public static int nextId = 0;
        private static List<Signal> signalList = new List<Signal>()
        {    
            new Signal(01,33),new Signal(02,33)
        };

        public IList<Signal> GetSignals()
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
