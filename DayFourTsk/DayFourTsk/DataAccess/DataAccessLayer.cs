using System.Data;
using Microsoft.Data.SqlClient;

namespace DayFourTsk.DataAccess
{
    public class DataAccessLayer
    {
        SqlConnection Con;
        SqlCommand Cmd;
        DataSet Ds;
        string Str;

        public DataAccessLayer(IConfiguration Config)
        {
            Str = Config.GetSection("ConnectionStrings")["DefaultConnection"] ?? throw new ArgumentNullException(nameof(Config), "Connection string cannot be null.");
            Con = new SqlConnection(Str);
            Cmd = new SqlCommand();
            Ds = new DataSet();
        }

        public bool InsertData(string StoredProcedure, string[] ParameterNames, string[] ParameterValues)
        {
            bool Result = false;
            try
            {
                if (ParameterNames.Length == ParameterValues.Length)
                {
                    Cmd.Connection = Con; Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = StoredProcedure;

                    for (int i = 0; i < ParameterNames.Length; i++)
                    {
                        Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                    }
                    Con.Open();
                    int A = Cmd.ExecuteNonQuery();
                    Con.Close();
                    if (A > 0)
                        Result = true;
                    else
                        Result = false;
                }
                else
                {
                    new Exception("ParameterNames and Values are not equal.");
                }
            }
            catch (Exception Ex)
            {
                new Exception($"Something went wrong,Exception :{Ex}");
                Result = false;
            }
            return Result;
        }
        public DataSet RetrivedData(string StoredProcedure, string[]? ParameterNames=null, string[]? ParameterValues=null)
        {
            using SqlConnection conn = new SqlConnection(Str);
            using SqlCommand cmd = new SqlCommand(StoredProcedure, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (ParameterNames != null && ParameterValues != null)
            {
                if (ParameterNames.Length != ParameterValues.Length)
                    throw new ArgumentException("Parameter names and values count do not match.");

                for (int i = 0; i < ParameterNames.Length; i++)
                {
                    cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                }
            }

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("No data retrieved from the database.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during data retrieval: {ex.Message}", ex);
            }

            return ds;
            //try
            //{
            //    Cmd.CommandType = CommandType.StoredProcedure; Cmd.CommandText = StoredProcedure; Cmd.Parameters.Clear();
            //    if (ParameterNames != null && ParameterValues != null)
            //    {
            //        if (ParameterNames.Length == ParameterValues.Length)
            //        {
            //            for (int i = 0; i < ParameterValues.Length; i++)
            //            {
            //                Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
            //            }
            //        }
            //        else
            //        {
            //            new Exception("ParameterNames and Values are not equal.");
            //        }
            //    }
            //    SqlDataAdapter Da=new SqlDataAdapter(Cmd);
            //    Da.Fill(Ds);
            //    if (Ds.Tables.Count == 0 || Ds.Tables[0].Rows.Count > 0)
            //        throw new Exception("No data retrived from DataBase");
            //}
            //catch (Exception Ex)
            //{
            //    new Exception($"Something went wrong,Exception :{Ex}");
            //}
            return Ds;
        }
        public string ExecuteScalar(string StoredProcedure, string[] ParameterNames, string[] ParameterValues)
        {
            string Result = string.Empty; Cmd.CommandType = CommandType.StoredProcedure; Cmd.CommandText = StoredProcedure;
            try
            {
                if (ParameterNames.Length == ParameterValues.Length)
                {
                    for (int i = 0; i < ParameterValues.Length; i++)
                    {
                        Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                    }
                }
                Con.Open();
                object obj = Cmd.ExecuteScalar();
                Con.Close();
                Result = obj?.ToString() ?? string.Empty;
            }
            catch (Exception Ex)
            {
                new Exception($"Something went wrong,Exception :{Ex}");
            }
            return Result;
        }
        public DataSet DataRead(string StoredProcedure, string[] ParameterNames, string[] ParameterValues)
        {

            try
            {
                Cmd.CommandType = CommandType.StoredProcedure;Cmd.CommandText = StoredProcedure;
                if (ParameterNames != null && ParameterValues != null)
                {
                    if (ParameterNames.Length == ParameterValues.Length)
                    {
                        for (int i = 0; i < ParameterValues.Length; i++)
                        {
                            Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                        }
                    }
                    else
                    {
                        new Exception("ParameterNames and Values are not equal.");
                    }
                }
                SqlDataReader Dr=Cmd.ExecuteReader();
                while (Dr.Read())
                {
                    DataRow row = Ds.Tables[0].NewRow();
                    for (int i = 0; i < Dr.FieldCount; i++)
                    {
                        row[i] = Dr[i];
                    }
                    Ds.Tables[0].Rows.Add(row);
                }
            }
            catch (Exception Ex)
            {
                new Exception($"Something went wrong,Exception :{Ex}");
            }
            return Ds;
        }
    }
}
