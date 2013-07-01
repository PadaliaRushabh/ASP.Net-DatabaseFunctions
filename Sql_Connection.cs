using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for myconnection
/// </summary>
public class Sql_Connection
{

    static public string con
    {
        get
        {
            //Your connection string here
            return @"Data Source='';Initial Catalog='';User ID='';Password=''";
        }

        set { }
    }

}
