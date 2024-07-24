// See https://aka.ms/new-console-template for more information
using LiteDB;
using Models.DAL.Entities.User;

Console.WriteLine("Hello, World!");

var db = new LiteDatabase("D:\\C# Projects\\SpaceAvenger\\SpaceAvenger\\bin\\Debug\\net6.0-windows\\DataBase\\Local.db");

var col = db.GetCollection<User>().FindAll();

//db.GetCollection<User>().DeleteAll();

foreach (var user in col)
{ 
    Console.WriteLine(user);
}

Console.WriteLine("Finished");
