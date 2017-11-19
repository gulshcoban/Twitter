using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Configuration;

namespace WcfServiceTwitter
{

    public class Service1 : IService1
    {
        
        public string DeleteFollowed(int MemberID,int FollowerID)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Followed WHERE MemberID = @MemberID AND FollowerID=@FollowerID", con);
            cmd.Parameters.AddWithValue("MemberID", MemberID);
            cmd.Parameters.AddWithValue("FollowerID", FollowerID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = FollowerID + " details deleted successfully";
            }
            else
            {
                message = FollowerID + "details not deleted successfully";
            }
            con.Close();
            return message;
        }
        public string DeleteFollower(int MemberID, int FollowedID)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Follower WHERE MemberID = @MemberID AND FollowedID=@FollowedID", con);
            cmd.Parameters.AddWithValue("MemberID", MemberID);
            cmd.Parameters.AddWithValue("FollowedID", FollowedID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = FollowedID + " details deleted successfully";
            }
            else
            {
                message = FollowedID + "details not deleted successfully";
            }
            con.Close();
            return message;
        }
        public List<Tweet> GetTweet(int MemberID)
        {
            List<Followed> followed = GetFollowerID(MemberID);

            List<Tweet> tweets = new List<Tweet>();
            foreach (var item in followed)
            {
                string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                var FollowerID = @item.FollowerID;
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tweet INNER JOIN Member ON Tweet.MemberID = Member.MemberID WHERE Member.MemberID = @FollowerID  ORDER BY TweetDate DESC", con);
                cmd.Parameters.AddWithValue("@FollowerID", FollowerID);
                SqlDataReader reader = cmd.ExecuteReader(); 
                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        Tweet tweet = new Tweet();

                        tweet.TweetID = Convert.ToInt32(reader[0]);
                        tweet.MemberID = Convert.ToInt32(reader[1]);
                        tweet.Content = reader[2].ToString();
                        tweet.TweetDate = Convert.ToDateTime(reader[3]);
                        tweet.Name = reader[6].ToString();

                        tweets.Add(tweet);
                    }
                }
                reader.Close();
                con.Close();
            }


            return tweets;
        }

        public List<Followed> GetFollowerID (int MemberID)
        {
            List<Followed> followed = new List<Followed>();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Followed INNER JOIN Member ON Followed.MemberID = Member.MemberID WHERE Member.MemberID = @MemberID ORDER BY Followed.FollowerID", con);
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    Followed f = new Followed();
                    f.FollowerID = Convert.ToInt32(reader[2]);
                    followed.Add(f);
                }
            }
            con.Close();
            return followed;
        }
        public List<Follower> GetFollowedID(int MemberID)
        {
            List<Follower> follower = new List<Follower>();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Follower INNER JOIN Member ON Follower.MemberID = Member.MemberID WHERE Member.MemberID = @MemberID", con);
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    Follower f = new Follower();
                    f.FollowedID = Convert.ToInt32(reader[2]);
                    follower.Add(f);
                }
            }
            con.Close();
            return follower;
        }
        public string MemberInsert(Member member)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Member(Name, UserName, Email,Password) values(@Name, @UserName,@Email, @Password)", con);
            cmd.Parameters.AddWithValue("@Name", member.Name);
            cmd.Parameters.AddWithValue("@UserName", member.UserName);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@Password", member.Password);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = member.Name + " details inserted successfully";
            }
            else
            {
                message = member.Name + "details not inserted successfully";
            }
            con.Close();
            return message;
        }

        public string TweetInsert(Tweet tweet)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Tweet(Content, TweetDate, MemberID) values(@Content, @TweetDate,@MemberID)", con);
            cmd.Parameters.AddWithValue("@Content", tweet.Content);
            cmd.Parameters.AddWithValue("@TweetDate", tweet.TweetDate);
            cmd.Parameters.AddWithValue("@MemberID", tweet.MemberID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = tweet.Content + " details inserted successfully";
            }
            else
            {
                message = tweet.Content + "details not inserted successfully";
            }
            con.Close();
            return message;
        }

        public int isRight(Member member)
        {
            int i = 0;
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserName,Password FROM Member", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    Member members = new Member()
                    {
                        UserName = reader[0].ToString(),
                        Password = reader[1].ToString()
                    };
                    if (members.UserName == member.UserName && members.Password == member.Password)
                    {
                        i = 1;
                    }
                }
            }
            con.Close();
            return i;
        }

        public bool isThereAnAccount(Member member)
        {
            bool message = true;
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Member", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    Member members = new Member()
                    {
                        Name = reader[1].ToString(),
                        UserName = reader[2].ToString(),
                        Email = reader[3].ToString(),
                        Password = reader[4].ToString()
                    };
                    if (members.UserName == member.UserName && members.Password == member.Password)
                    {
                        message = false;
                    }
                }
            }
            return message;
        }

        public string TweetDelete(int tweetID)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Tweet WHERE TweetID=@tweetID", con);
            cmd.Parameters.AddWithValue("TweetID", tweetID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = tweetID + " details deleted successfully";
            }
            else
            {
                message = tweetID + "details not deleted successfully";
            }
            con.Close();
            return message;
        }

        public int GetMemberID(Member member)
        {
            int message = 0;
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT MemberID,UserName,Password FROM Member", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    Member members = new Member()
                    {
                        MemberID = Convert.ToInt32(reader[0]),
                        UserName = reader[1].ToString(),
                        Password = reader[2].ToString()
                    };
                    if (members.UserName == member.UserName && members.Password == member.Password)
                    {
                        message = members.MemberID;
                    }
                }
            }
            con.Close();
            return message;
        }

        public Member GetMember(int MemberID)
        {
            Member member = new Member();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Member Where MemberID = @MemberID", con);
            cmd.Parameters.AddWithValue("MemberID", MemberID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                        member.MemberID = Convert.ToInt32(reader[0]);
                        member.Name = reader[1].ToString();
                        member.UserName = reader[2].ToString();
                        member.Email = reader[3].ToString();
                        member.Password = reader[4].ToString();
                }
            }
            
            con.Close();
            return member;
        
        }

        public string FavInsert(int tweetID,int memberID)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Favorite(TweetID,MemberID) values(@TweetID, @MemberID)", con);
            cmd.Parameters.AddWithValue("@TweetID", tweetID);
            cmd.Parameters.AddWithValue("@MemberID", memberID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = tweetID + " details inserted successfully";
            }
            else
            {
                message = tweetID + "details not inserted successfully";
            }
            con.Close();
            return message;
        }

         public string MemberUpdate(Member member)
         {
             string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
             SqlCommand cmd = new SqlCommand("Update Member SET Name=@Name, UserName=@UserName, Email=@Email, Password=@Password WHERE MemberID=@MemberID", con);
             cmd.Parameters.AddWithValue("@MemberID", member.MemberID);
             cmd.Parameters.AddWithValue("@Name", member.Name);
             cmd.Parameters.AddWithValue("@UserName", member.UserName);
             cmd.Parameters.AddWithValue("@Email", member.Email);
             cmd.Parameters.AddWithValue("@Password", member.Password);
             int result = cmd.ExecuteNonQuery();
             if (result == 1)
             {
                 message = member.Name + " details updated successfully";
             }
             else
             {
                 message = member.Name  + "details not updated successfully";
             }
             con.Close();
             return message;
         }
        public List<Tweet> GetMemberTweet(int MemberID)
        {
            List<Tweet> tweets = new List<Tweet>();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tweet WHERE MemberID = @MemberID ORDER BY TweetDate DESC ", con);
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    Tweet tweet = new Tweet()
                    {
                        TweetID = Convert.ToInt32(reader[0]),
                        MemberID = Convert.ToInt32(reader[1]),
                        Content = reader[2].ToString(),
                        TweetDate = Convert.ToDateTime(reader[3])
                    };
                    tweets.Add(tweet);
                }
            }
            con.Close();
            return tweets;
        }
        public List<Member> GetOtherMember(int MemberID)
        {
            List<Followed> followed = GetFollowerID(MemberID);
            List<int> follow = new List<int>();
            List<Member> members = new List<Member>();
            foreach (var item in followed)
            {
                follow.Add(item.FollowerID);
            }
            var count = follow.Count();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
                
                SqlCommand cmd = new SqlCommand("SELECT * FROM Member WHERE Member.MemberID != @MemberID ", con);
                cmd.Parameters.AddWithValue("MemberID", MemberID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                int i = 0;
                    while (reader.Read() == true)
                    {
                    if (count == 0)
                    {
                        Member member = new Member();
                        member.MemberID = Convert.ToInt32(reader[0]);
                        member.Name = reader[1].ToString();
                        member.UserName = reader[2].ToString();
                        members.Add(member);

                    }
                    else
                    {
                        if (Convert.ToInt32(reader[0]) == follow[i])
                        {
                            if (i + 1 != count)
                                i++;

                            continue;
                        }
                        else
                        {
                            Member member = new Member();
                            member.MemberID = Convert.ToInt32(reader[0]);
                            member.Name = reader[1].ToString();
                            member.UserName = reader[2].ToString();
                            members.Add(member);
                        }
                    }
                    }
                }

                con.Close();
            
            return members;
        }

        public int NumberOfTweet(int MemberID)
        {
            Member member = new Member();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tweet WHERE MemberID = @MemberID", con);
            cmd.Parameters.AddWithValue("MemberID", MemberID);
            int result = cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    result = Convert.ToInt32(reader[0]);
                }
            }
            con.Close();
            return result;
        }

        public int NumberOfFollowed(int MemberID)
        {
            Member member = new Member();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Followed WHERE MemberID = @MemberID", con);
            cmd.Parameters.AddWithValue("MemberID", MemberID);
            int result = cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    result = Convert.ToInt32(reader[0]);
                }
            }
            con.Close();
            return result;
        }

        public int NumberOfFollowers(int MemberID)
        {
            Member member = new Member();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Follower WHERE MemberID = @MemberID", con);
            cmd.Parameters.AddWithValue("MemberID", MemberID);
            int result = cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    result = Convert.ToInt32(reader[0]);
                }
            }
            con.Close();
            return result;
        }

        public string FollowedInsert(int MemberID, int FollowerID)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Followed(MemberID,FollowerID) values(@MemberID,@FollowerID)", con);
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            cmd.Parameters.AddWithValue("@FollowerID", FollowerID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = MemberID + " details inserted successfully";
            }
            else
            {
                message = MemberID + "details not inserted successfully";
            }
            con.Close();
            return message;
        }

        public string FollowerInsert(int MemberID, int FollowedID)
        {
            string message = "";
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Follower(MemberID,FollowedID) values(@MemberID,@FollowedID)", con);
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            cmd.Parameters.AddWithValue("@FollowedID", FollowedID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                message = MemberID + " details inserted successfully";
            }
            else
            {
                message = MemberID + "details not inserted successfully";
            }
            con.Close();
            return message;
        }

        public int NumberOfFav(int TweetID)
        {
            Tweet tweet = new Tweet();
            string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Favorite WHERE TweetID = @TweetID ", con);
            cmd.Parameters.AddWithValue("@TweetID", TweetID);
            int result = cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read() == true)
                {
                    result = Convert.ToInt32(reader[0]);
                }
            }
            con.Close();
            return result;
        }

        public List<Member> GetFollowed(int MemberID)
        {
            List<Followed> followed = GetFollowerID(MemberID);

            List<Member> members = new List<Member>();
            foreach (var item in followed)
            {
                string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                var FollowerID = @item.FollowerID;
                SqlCommand cmd = new SqlCommand("SELECT * FROM Member WHERE Member.MemberID = @FollowerID", con);
                cmd.Parameters.AddWithValue("@FollowerID", FollowerID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        Member member = new Member();
                        member.MemberID = Convert.ToInt32(reader[0]);
                        member.Name = reader[1].ToString();
                        member.UserName = reader[2].ToString();
                        member.Email = reader[3].ToString();
                        member.Password = reader[4].ToString();
                        members.Add(member);
                    }
                }
                reader.Close();
                con.Close();
            }


            return members;
        }
        public List<Member> GetFollower(int MemberID)
        {
            List<Follower> follower = GetFollowedID(MemberID);

            List<Member> members = new List<Member>();
            foreach (var item in follower)
            {
                string connectionString = @"Data Source=TOSHIBA;Integrated Security=True;Initial Catalog=Staniel;";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                var FollowedID = @item.FollowedID;
                SqlCommand cmd = new SqlCommand("SELECT * FROM Member WHERE Member.MemberID = @FollowedID", con);
                cmd.Parameters.AddWithValue("@FollowedID", FollowedID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        Member member = new Member();
                        member.MemberID = Convert.ToInt32(reader[0]);
                        member.Name = reader[1].ToString();
                        member.UserName = reader[2].ToString();
                        member.Email = reader[3].ToString();
                        member.Password = reader[4].ToString();
                        members.Add(member);
                    }
                }
                reader.Close();
                con.Close();
            }


            return members;
        }
    }
}
