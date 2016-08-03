using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddAccount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }
    }


    public class Account
    {
        public string id_or_MailAddress;
        public string password;
        public string remarks;

        public Account(string id, string pa, string re)
        {
            id_or_MailAddress = id;
            password = pa;
            remarks = re;
        }
    }


    public class Data
    {
        static string localPath;

        //================================================================================
        //	データの初期化
        //================================================================================
        public static void Setup()
        {
            localPath = Application.ExecutablePath;
        }


        static Account StringToAccount(string str)
        {
            string[] strs = str.Split ('\n');
            Account data = new Account (strs[0], strs[1], strs[2]);
            return data;
        }

        static string AccountToString(Account data)
        {
            return data.id_or_MailAddress + data.password + data.remarks;
        }


        //================================================================================
        //	データの読み込み
        //================================================================================
        public static Account Load(string fileName)
        {
            string path = localPath + "/" + fileName + ".txt";
            string str = File.ReadAllText(path);
            return StringToAccount(str);
        }

        //================================================================================
        //	データの保存
        //================================================================================
        public static void Save(string fileName, Account data)
        {
            string path = localPath + "/" + fileName + ".txt";
            StreamWriter fileWriter = new StreamWriter(path);
            fileWriter.WriteLine(AccountToString(data));
            fileWriter.Close();
        }
    }
}
