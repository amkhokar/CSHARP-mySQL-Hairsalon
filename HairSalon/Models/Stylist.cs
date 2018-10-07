using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
        private string _stylistName;
        private int _stylistId;
        public Stylist(string StylistName, int StylistId = 0)
        {
            _stylistName = StylistName;
            _stylistId = StylistId;
        }
        public string GetStylist()
        {
            return _stylistName;
        }
        public int GetStylistId()
        {
            return _stylistId;
        }
        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = this.GetStylistId() == newStylist.GetStylistId();
                bool nameEquality = this.GetStylist() == newStylist.GetStylist();
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetStylist();
            return allHash.GetHashCode();
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.GetStylist();
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _stylistId = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Stylist> GetAllStylist()
        {
            List<Stylist> allStylist = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int StylistId = rdr.GetInt32(0);
                string StylistName = rdr.GetString(1);
                Stylist newStylist = new Stylist(StylistName, StylistId);
                allStylist.Add(newStylist);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allStylist;
        }
        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists where id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int StylistId = 0;
            string StylistName = "";
            while(rdr.Read())
            {
                StylistId = rdr.GetInt32(0);
                StylistName = rdr.GetString(1);
            }
            Stylist newStylist = new Stylist (StylistName, StylistId);   
            conn.Close();
            if (conn!=null)
            {
                conn.Dispose();
            }
            return newStylist;
        }
        public List<Client> GetClient()
        {
            List<Client> StylistClient = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clients.* FROM stylists
            JOIN stylists_clients ON (stylists.id = stylists_clients.stylist_id)
            JOIN clients ON (stylists_clients.client_id = clients.id)
            WHERE stylists.id = @stylistId;";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _stylistId;
            cmd.Parameters.Add(stylistIdParameter);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                Client newClient = new Client(clientName, clientId);
                StylistClient.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return StylistClient;
        }
   
        public void AddClient(Client newClient)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_clients(stylist_id, client_id) VALUES (@stylistId, @clientId);";
            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _stylistId;
            cmd.Parameters.Add(stylistIdParameter);
            MySqlParameter clientIdParameter = new MySqlParameter();
            clientIdParameter.ParameterName = "clientId";
            clientIdParameter.Value = newClient.GetClientId();
            cmd.Parameters.Add(clientIdParameter);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Specialty> GetSpecialty()
        {
            List<Specialty> specialties = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
            JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
            JOIN specialties ON (stylists_specialties.specialty_id = specialties.id)
            WHERE stylists.id = @stylistsIdParameter;";
            MySqlParameter stylistsIdParameter = new MySqlParameter();
            stylistsIdParameter.ParameterName = "@stylistsIdParameter";
            stylistsIdParameter.Value = this._stylistId;
            cmd.Parameters.Add(stylistsIdParameter);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int specialtiesId = rdr.GetInt32(0);
                string specialtiesName = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(specialtiesName, specialtiesId);
                specialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }  
            return specialties;
        }
        public void AddSpecialty (Specialty newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);";
            MySqlParameter specialties_id = new MySqlParameter();
            specialties_id.ParameterName = "@SpecialtyId";
            specialties_id.Value = newSpecialty.GetId();
            cmd.Parameters.Add(specialties_id);
            MySqlParameter Stylists_id = new MySqlParameter();
            Stylists_id.ParameterName = "@StylistId";
            Stylists_id.Value = _stylistId;
            cmd.Parameters.Add(Stylists_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
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
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @stylistId; DELETE FROM stylists_specialties WHERE stylist_id= @stylistId; DELETE FROM stylists_clients WHERE stylist_id = @stylistId;";
            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylistId";
            stylistId.Value = _stylistId;
            cmd.Parameters.Add(stylistId);
            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }
        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _stylistId;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _stylistName = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
