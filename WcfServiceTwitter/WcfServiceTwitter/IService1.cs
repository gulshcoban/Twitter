using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceTwitter
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string DeleteFollowed(int MemberID, int FollowerID);

        [OperationContract]
        string DeleteFollower(int MemberID, int FollowedID);
        [OperationContract]
        List<Followed> GetFollowerID(int MemberID);

        [OperationContract]
        List<Follower> GetFollowedID(int MemberID);

        [OperationContract]
        List<Member> GetFollowed(int MemberID);

        [OperationContract]
        List<Member> GetFollower(int MemberID);

        [OperationContract]
        List<Tweet> GetTweet(int MemberID);

        [OperationContract]
        int NumberOfFav(int TweetID);

        [OperationContract]
        int NumberOfTweet(int MemberID);

        [OperationContract]
        int NumberOfFollowed(int MemberID);

        [OperationContract]
        int NumberOfFollowers(int MemberID);

        [OperationContract]
        List<Member> GetOtherMember(int MemberID);

        [OperationContract]
        Member GetMember(int MemberID);

        [OperationContract]
        string MemberInsert(Member member);

        [OperationContract]
        string FollowedInsert(int MemberID,int FollowerID);

        [OperationContract]
        string FollowerInsert(int MemberID, int FollowedID);

        [OperationContract]
        string TweetInsert(Tweet tweet);

        [OperationContract]
        int isRight(Member member);

        [OperationContract]
        Boolean isThereAnAccount(Member member);

        [OperationContract]
        string TweetDelete(int tweetID);

        [OperationContract]
        int GetMemberID(Member member);

        [OperationContract]
        string MemberUpdate(Member member);

        [OperationContract]
        string FavInsert(int tweetID,int memberID);

        [OperationContract]
        List<Tweet> GetMemberTweet(int MemberID);

    }

    [DataContract]
    public class Member
    {
        [DataMember]
        public int MemberID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
    }

    [DataContract]
    public class Tweet
    {
        [DataMember]
        public int TweetID { get; set; }
        [DataMember]
        public int MemberID { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTime TweetDate { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class Favorite
    {
        [DataMember]
        public int FavoriteID { get; set; }
        [DataMember]
        public int TweetID { get; set; }
        [DataMember]
        public int MemberID { get; set; }
    }

    [DataContract]
    public class Follower
    {
        [DataMember]
        public int FollowerID { get; set; }
        [DataMember]
        public int MemberID { get; set; }
        [DataMember]
        public int FollowedID { get; set; }
    }

    [DataContract]
    public class Followed
    {
        [DataMember]
        public int FollowedID { get; set; }
        [DataMember]
        public int MemberID { get; set; }
        [DataMember]
        public int FollowerID { get; set; }
    }
}
