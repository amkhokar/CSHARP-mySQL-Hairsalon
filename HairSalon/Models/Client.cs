using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        private string _clientName;
        private int _clientId;
        public Client(string newClient, int ClientId = 0)
        {
            _clientName = newClient;
            _clientId = ClientId;
        }
        public string GetClient()
        {
            return _clientName;
        }
        public int GetClientId()
        {
            return _clientId;
        }
        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client)otherClient;
                bool idEquality = this.GetClientId() == newClient.GetClientId();
                bool nameEquality = this.GetClient() == newClient.GetClient();
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetClient();
            return allHash.GetHashCode();
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name) VALUES (@name);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._clientName;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _clientId = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Client> GetAllClient()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                Client newClient = new Client(clientName, clientId);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients where id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
            }
            Client foundClient = new Client(clientName, clientId);
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return foundClient;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @clientId; DELETE FROM stylists_clients WHERE client_id = @clientId;";
            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@clientId";
            clientId.Value = _clientId;
            cmd.Parameters.Add(clientId);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }
        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _clientId;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _clientName = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Stylist> GetStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM clients
            JOIN stylists_clients ON (clients.id = stylists_clients.client_id)
            JOIN stylists ON (stylists_clients.stylist_id = stylists.id)
            WHERE clients.id = @clientIdParameter;";
            MySqlParameter clientIdParameter = new MySqlParameter();
            clientIdParameter.ParameterName = "@clientIdParameter";
            clientIdParameter.Value = this.GetClientId();
            cmd.Parameters.Add(clientIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> stylists = new List<Stylist> {};
            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                Stylist newStylist = new Stylist(stylistName, stylistId);
                stylists.Add(newStylist);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return stylists;
        }
        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_clients (client_id, stylist_id) VALUES (@ClientId, @StylistId);";
            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@ClientId";
            clientId.Value = _clientId;
            cmd.Parameters.Add(clientId);
            MySqlParameter Stylists_id = new MySqlParameter();
            Stylists_id.ParameterName = "@StylistId";
            Stylists_id.Value = newStylist.GetStylistId();
            cmd.Parameters.Add(Stylists_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}