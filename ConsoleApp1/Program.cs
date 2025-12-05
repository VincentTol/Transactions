using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionAssignment;

class Program
{
    static void Main()
    {
        var db = new Transactions();
        db.BeginTransaction();
        db.Put("a", 42);
        db.Commit();
        Console.WriteLine(db.Get("a")); // prints 42
        Console.ReadLine(); // keep window open
    }
}


