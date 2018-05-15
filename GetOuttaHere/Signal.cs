using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace GetOuttaHere
{
    public class Signal
    {
        private int _ID;
        private string _tid;
        private DateTime _datetime;
        private int _sensorNumber;

        public Signal(int id, int sensorNumber)
        {
            //this.DateTime = datetime;
            this.ID = id;
            this.SensorNumber = sensorNumber;
        }
        public Signal()
        {
            
        }
        [DataMember]
        public DateTime DateTime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }
        [DataMember]
        public int SensorNumber
        {
            get { return _sensorNumber; }
            set { _sensorNumber = value; }
        }

        [DataMember]
        public string Tid
        {
            get { return _tid; }
            set { _tid = value; }
        }
        [DataMember]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
    }
}