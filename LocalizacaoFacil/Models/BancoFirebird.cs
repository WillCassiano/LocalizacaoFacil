using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace LocalizacaoFacil.Models
{
    public class BancoFirebird
    {
        // String de conexão com o banco
        // Variavel que faz a conexão com o banco
        // Variavel que responsável pelas transações no banco

        FbConnectionStringBuilder stConnection;

        public FbConnection connection;
        public FbCommand command;

        public BancoFirebird()
        {
            stConnection = new FbConnectionStringBuilder();
            stConnection.DataSource = "localhost";
            stConnection.Database = @"C:\ecosis\dados\ecodados.eco";
            stConnection.UserID = "SYSDBA";
            stConnection.Password = "masterkey";

            connection = new FbConnection(stConnection.ToString());
            command = new FbCommand();
            command.Connection = connection;
        }

        public void Open()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public FbDataReader Query(string sql)
        {            
            command.CommandText = sql;

            if (connection.State != System.Data.ConnectionState.Open)
                Open();

            return command.ExecuteReader();
        }

        public void Close()
        {
            connection.Close();
        }

    }
}
