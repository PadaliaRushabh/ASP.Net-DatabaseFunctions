using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class SqlFunctions
{
    SqlConnection sql_conn;
    DataSet sql_ds = new DataSet();
    DataTable sql_dt = new DataTable();
    SqlCommand sql_cmd = new SqlCommand();
    SqlDataAdapter sql_adpt = new SqlDataAdapter();
    SqlDataReader sql_read;

    public SqlObject()
    {

    }

    public void connection()
    {
        sql_conn = new SqlConnection(Sql_Connection.con.ToString());
        sql_conn.Open();
    }

    public SqlDataReader getprocedure(string pro_name)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        sql_read = sql_cmd.ExecuteReader();
        return sql_read;

    }
    public SqlDataReader getprocedure(string pro_name, string var_name, SqlDbType dt, dynamic value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        sql_cmd.Parameters.Add(var_name, dt).Value = value;
        sql_read = sql_cmd.ExecuteReader();
        return sql_read;

    }
    public SqlDataAdapter getprocedure_dir(string pro_name, string var_name, SqlDbType dt, dynamic value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        sql_cmd.Parameters.Add(var_name, dt).Value = value;
        sql_cmd.Parameters.Add(new SqlParameter("@rp", SqlDbType.VarChar, 10));
        sql_cmd.Parameters["@rp"].Direction = ParameterDirection.Output;
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_ds);
        return sql_adpt;

    }

    public SqlDataReader getprocedure(string pro_name, Dictionary<string, SqlDbType> para, dynamic[] value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        int count = 0;
        foreach (var pair in para)
        {
            sql_cmd.Parameters.Add(pair.Key, pair.Value).Value = value[count];
            count++;
        }
        sql_read = sql_cmd.ExecuteReader();
        return sql_read;
    }

    public DataSet getprocedure_ds(string pro_name, Dictionary<string, SqlDbType> para, dynamic[] value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        int count = 0;
        foreach (var pair in para)
        {
            sql_cmd.Parameters.Add(pair.Key, pair.Value).Value = value[count];
            count++;
        }
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_ds);

        return sql_ds;
    }
    public DataSet getprocedure_table(string pro_name,string[] para,string[] para_value,dynamic[] dt, string[] var_name)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        int count = 0;

        for (int i = 0; i < para.Length; i++)
        {

            sql_cmd.Parameters.AddWithValue(para[i], para_value[i]);
        }
        
        foreach (DataTable table in dt)
        {
            
            sql_cmd.Parameters.AddWithValue(var_name[count], table);
            count++;
        }
            using (SqlDataAdapter adp = new SqlDataAdapter(sql_cmd))
            {
                adp.Fill(sql_ds);
            }
        
        return sql_ds;
    }

    public DataSet getprocedure_ds(string pro_name, string var_name, SqlDbType dt, dynamic value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        sql_cmd.Parameters.Add(var_name, dt).Value = value;
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_ds);

        return sql_ds;

    }

    public DataTable getprocedure_dt(string pro_name, Dictionary<string, SqlDbType> para, dynamic[] value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        int count = 0;
        foreach (var pair in para)
        {
            sql_cmd.Parameters.Add(pair.Key, pair.Value).Value = value[count];
            count++;
        }
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_dt);

        return sql_dt;
    }

    public DataTable getprocedure_dt(string pro_name, string var_name, SqlDbType dt, dynamic value)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        sql_cmd.CommandType = CommandType.StoredProcedure;
        sql_cmd.Parameters.Add(var_name, dt).Value = value;
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_dt);

        return sql_dt;

    }

    public DataSet getprocedure_ds(string pro_name)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_ds);

        return sql_ds;

    }

    public DataTable getprocedure_dt(string pro_name)
    {
        connection();
        sql_cmd = new SqlCommand(pro_name, sql_conn);
        SqlDataAdapter sql_adpt = new SqlDataAdapter(sql_cmd);
        sql_adpt.Fill(sql_dt);

        return sql_dt;

    }
    public void binddata(dynamic web_objects, SqlDataReader sql_read)
    {
        web_objects.DataSource = sql_read;
        web_objects.DataBind();
    }

    public void binddata(dynamic web_objects, DataSet sql_ds)
    {
        web_objects.DataSource = sql_ds;
        web_objects.DataBind();
    }
    public void binddata(dynamic web_objects, DataTable  sql_dt)
    {
        web_objects.DataSource = sql_dt;
        web_objects.DataBind();
    }

    public void binddata(dynamic web_objects, DataSet sql_ds, string dtextfield, string dvalue)
    {
        web_objects.DataSource = sql_ds;
        web_objects.DataTextField = dtextfield;
        web_objects.DataValueField = dvalue;
        web_objects.DataBind();
    }

    public void binddata(dynamic web_objects, DataTable sql_dt, string dtextfield, string dvalue)
    {
        web_objects.DataSource = sql_dt;
        web_objects.DataTextField = dtextfield;
        web_objects.DataValueField = dvalue;
        web_objects.DataBind();
    }

    public void binddata(dynamic web_objects, SqlDataReader sql_read, string dtextfield, string dvalue)
    {
        web_objects.DataSource = sql_read;
        web_objects.DataTextField = dtextfield;
        web_objects.DataValueField = dvalue;
        web_objects.DataBind();
    }

}
