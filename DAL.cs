using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class DAL
    {   
        public bool isProcall=false;
        List<SqlParameter> paralist= new List<SqlParameter>(); //sqlparameter is a class used to pass the parameter to stored procedure in sql data base 
       // public ConnectionState state = ConnectionState.Closed;
        

        public SqlConnection GetConnection() 
        {
            string ConnectionString = ConfigurationManager.AppSettings["SqlConnectionString"];
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            return con;
            
        }

        public void ClearParameters()
        {
            paralist.Clear();
        }

        public void AddParameters(string paraname,string value)   //paraname is action and value is query i.e select,update and insert
        {
            paralist.Add(new SqlParameter(paraname, value));
        }

        public SqlCommand GetCommand(string Query) 
        {
            SqlCommand cmd = new SqlCommand();  //SqlCommand used for connected data access
            if (isProcall)                       //we donot used dataset and data table with sqlcommand
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(paralist.ToArray());
            }

            else
                cmd.CommandType = CommandType.Text;

            cmd.CommandText=Query;
            cmd.Connection= GetConnection();
            return cmd;
        }

        private DataSet GetTables(string Query)
        {
            SqlDataAdapter da = new SqlDataAdapter(GetCommand(Query));//query pass karte he dataadpter ke parametre me
            DataSet ds=new DataSet();             //sqldataadapter used for disconnected data access
            da.Fill(ds);                          //data adapter ke sath hum dataset aur datatable ko use karte hai
            return ds;
        }

        private DataTable GetTable(string Query)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd=GetCommand(Query);
            cmd.Connection.Open();

            SqlDataReader rdr =cmd.ExecuteReader();   //whenever sqldata reader is used connection should be open cumlulsory
            if (rdr != null && rdr.HasRows)
                dt.Load(rdr);

            cmd.Connection.Close();

            return dt;
        }

        public object GetID(string Query)
        {
            SqlCommand cmd = GetCommand(Query);
            cmd.Connection.Open();

            object retval =cmd.ExecuteScalar();
            cmd.Connection.Close();

            return retval;
        }

        public SqlDataReader GetReader(string Query)
        {
            SqlCommand cmd = GetCommand(Query);
            cmd.Connection.Open();
            SqlDataReader rdr =cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return rdr;
        }

        public int ExecuteQuery(string Query)
        {
            SqlCommand cmd= GetCommand(Query);
            cmd.Connection.Open();

            int retval = cmd.ExecuteNonQuery();  //it returns no of rows affected
            cmd.Connection.Close();

            return retval;  //executequery return integer representing no.of rows affected by sql statmnt
            //use this method if we use insert.delete,update statment
        }
    }
}
//dataset and data adapter is disconnected architecture and are used for read from database or to store data in database
//connection,command,parametre,dtareader are connectted archi
//fun vs stroed procedure