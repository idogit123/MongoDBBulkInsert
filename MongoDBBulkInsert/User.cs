using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MongoDBBulkInsert;

public class User
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public Address address { get; set; }
    public Contact contact { get; set; }
    public Job job { get; set; }
}

public class Address
{
    public string country { get; set; }
    public string city { get; set; }
    public string streetAdress { get; set; }
    public string zipCode { get; set; }
}

public class Contact
{
    public string phone { get; set; }
    public string email { get; set; }
    public string instegram { get; set; }
}

public class Job
{
    public string company { get; set; }
    public string title { get; set; }
    public string type { get; set; }
}