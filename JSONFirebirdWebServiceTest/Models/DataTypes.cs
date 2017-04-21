using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class DataTypes
    {
        public int ID { get; set; }
        public bool active { get; set; }
        public float price { get; set; }
        public DateTime datetime { get; set; }
        public DateTime date { get; set; }
        public TimeSpan clock { get; set; }
        public string textvariable { get; set; }
        public byte[] blobfieldtext { get; set; }
        public byte[] blobfieldbinary { get; set; }

    }
}