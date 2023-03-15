using System;
using System.Data.SqlClient;
using System.Data;


namespace TestDbManyToMany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=DESKTOP-G5HUDL2\SQLEXPRESS; Initial Catalog=db_test; Integrated Security=True");

            SqlConnection.Open();

            SqlCommand command = SqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;

            command.CommandText =
                @"SELECT posts.id, posts.post_title, tags.tag_name
                FROM posts LEFT JOIN posts_tags
                ON posts_tags.post_id=posts.id
                LEFT JOIN tags ON posts_tags.tag_id=tags.id";

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                object productName = reader.GetValue(0);
                object categoryName = reader.GetValue(1);

                if (categoryName.GetType() == typeof(DBNull))
                {
                    categoryName = "<без категории>";
                }

                Console.WriteLine($"{productName} - {categoryName}");
            }

            SqlConnection.Close();

        }
    }
}
