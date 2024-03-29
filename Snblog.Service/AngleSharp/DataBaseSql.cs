﻿using System.IO;
using MySql.Data.MySqlClient;

namespace Snblog.Service.AngleSharp
{
    public class DataBaseSql
    {
        public DataBaseSql()
        {
        }

        /// <summary>
        /// 数据备份
        /// </summary>
        /// <param name="path">备份路径默认null</param>
        /// <returns>备份结果</returns>

        public static string SqlBackups(string path)
        {
            const string sqlUrl = "server=localhost;User= root;pwd= woshishui;database=snblog;";
            string time = DateTime.Now.ToString("d").Replace("/","-");
             //path = Assembly.GetEntryAssembly().Location;
             path = Directory.GetCurrentDirectory();
            string file = path + $"/mysql/{time}_blog.sql";

            using MySqlConnection conn = new(sqlUrl);
            using MySqlCommand cmd = new();
            using MySqlBackup mb = new(cmd);

            cmd.Connection = conn;
            conn.Open();
            mb.ExportToFile(file);
            conn.Close();
            return "true";
        }

        public static string SqlRestore(string ip,string user,string pwd,string database)
        {
            string sqlUrl = "server=" + ip + ";User=" + user + ";pwd=" + pwd + ";database=" + database + ";";
            const string file = ".//mysql/" + "blog.sql";
            using MySqlConnection conn = new(sqlUrl);
            using MySqlCommand cmd = new();
            using MySqlBackup mb = new(cmd);
            cmd.Connection = conn;
            conn.Open();
            mb.ImportFromFile(file);
            conn.Close();
            return "true";
        }
    }
}
