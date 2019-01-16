using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Assignment1ASR
{
    class Database
    {
        private static SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=s3674270;database=s3674270;pwd=abc123;");

        public static SqlConnection Dblink()
        {
            return conn;
        }

        public static void readRoom()
        {
            SqlCommand queryRoom = new SqlCommand("select * from room", conn);

            SqlDataReader read;
            Program.roomList.Clear();
            try
            {
                conn.Open();

                read = queryRoom.ExecuteReader();

                while (read.Read())
                {
                    Program.roomList.Add(new Room(read["roomid"].ToString()));
                }

                read.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public static void readSlot()
        {
            Program.slotList.Clear();
            SqlCommand query = new SqlCommand("select * from slot", conn);
            SqlDataReader read;
            try
            {
                conn.Open();
                read = query.ExecuteReader();
                while (read.Read())
                {
                    Program.slotList.Add(new Slots(read["roomid"].ToString(), read["date"].ToString(),
                        read["starttime"].ToString(),
                        read["endtime"].ToString(),
                        read["staffid"].ToString(),
                        read["bookedinstudentid"].ToString()));
                }
                read.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void readStaff()
        {
            SqlCommand query = new SqlCommand("select * from [user] where userid like 'e%' ", conn);
            SqlDataReader read;

            Program.staffList.Clear();

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    Program.staffList.Add(new Staffs(read["Userid"].ToString(), read["Name"].ToString(), read["Email"].ToString()));
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void readStudent()
        {
            SqlCommand query = new SqlCommand("select * from [user] where userid like 's%' ", conn);
            SqlDataReader read;

            Program.studentList.Clear();

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    Program.studentList.Add(new Students(read["Userid"].ToString(), read["Name"].ToString(), read["Email"].ToString()));
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void executeSql(SqlCommand cmd)
        {
            try
            {
                Database.Dblink().Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                if (Database.Dblink().State == ConnectionState.Open)
                {
                    Database.Dblink().Close();
                }
            }
        }
    }
}
