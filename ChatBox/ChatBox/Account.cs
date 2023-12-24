using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatBox;

public class Account
{
    //private string email;
    //private string password;

    //public string Email { get => email; set => email = value; }
    //public string Password { get => password; set => password = value; }

    //public Account() 
    //{

    //}

    //public Account(string email, string password)
    //{
    //    this.Email = email;
    //    this.Password = password;
    //}
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
