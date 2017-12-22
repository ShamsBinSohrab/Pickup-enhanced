﻿using Oracle.ManagedDataAccess.Client;
using Pickup_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle_Repository
{
    public class UserRepository<TUser> where TUser : User
    {
        OracleConnection con = OraDataContext.GetInstance();

        public virtual List<User> GetAll()
        {
            List<User> list = new List<User>();

            con.Open();
            OracleCommand cmd = con.CreateCommand();

            if (typeof(TUser) == typeof(Buyer))
            {
                cmd.CommandText = "select * from buyers";
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Buyer {

                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Gender = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),
                        AreaId = reader.GetInt32(6)

                    });
                }
            }

            else if (typeof(TUser) == typeof(Seller))
            {
                cmd.CommandText = "select * from sellers";

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Seller
                    {

                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Gender = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),
                        AreaId = reader.GetInt32(6)

                    });
                }
            }

            con.Close();

            return list;

        }

        public virtual User Get(int? id)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            User user = null;

            if (typeof(TUser)==typeof(Buyer))
            {
                cmd.CommandText = "select * from buyers where id=" + id;

                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new Buyer()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Gender = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),
                        AreaId = reader.GetInt32(6)
                    };
                }
            }

            else if (typeof(TUser) == typeof(Seller))
            {
                cmd.CommandText = "select * from sellers where id=" + id;

                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new Seller()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Gender = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),
                        AreaId = reader.GetInt32(6)
                    };
                }

            }

            con.Close();
            return user;
        }

        public virtual int Insert(TUser user)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            if (typeof(TUser) == typeof(Buyer))
            {
                cmd.CommandText = "insert into buyers values(DEFAULT,'"+user.FirstName+"','"+user.LastName+"','"+user.Gender+"','"+user.Email+"','"+user.Phone+"',"+user.AreaId+")";
            }

            else if (typeof(TUser) == typeof(Seller))
            {
                cmd.CommandText = "insert into sellers values(DEFAULT,'" + user.FirstName + "','" + user.LastName + "','" + user.Gender + "','" + user.Email + "','" + user.Phone + "'," + user.AreaId + ")";
            }

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result;
        }


    }
}
