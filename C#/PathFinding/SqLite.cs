using System;
using System.Data.SQLite;

namespace PathFinding
{
    class SqLite
    {
        public static void Start()
        {
            
            CreateTable();
        }

        public static void CreateTable()
        {
            string cs = @"URI=file:C:\Users\fatim\Desktop\MyPractice\C#\SChallenge\Pathfinding.db";
            

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS paths";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE paths(id INTEGER PRIMARY KEY, 
                    source_node TEXT, target_node TEXT, distance INT, path TEXT)";
            cmd.ExecuteNonQuery();
        }

        public static int CreateRow(string source, string target, int distance, string path)
        {
            string cs = @"URI=file:C:\Users\fatim\Desktop\MyPractice\C#\SChallenge\Pathfinding.db";

            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = "INSERT INTO paths(source_node, target_node, distance, path) VALUES(@source, @target, @distance, @path)";

            cmd.Parameters.AddWithValue("@source", source);
            cmd.Parameters.AddWithValue("@target", target);
            cmd.Parameters.AddWithValue("@distance", distance);
            cmd.Parameters.AddWithValue("@path", path);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            cmd.CommandText = "select last_insert_rowid()";
            Int64 LastRowId = (Int64)cmd.ExecuteScalar();
            int LastRow = (int)LastRowId;

            Console.WriteLine("row inserted");
            
            return LastRow;
        }
    }
}
