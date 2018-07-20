using System.Security.Principal;

namespace CSharp.com
{
    public class userService
    {
        public static user Login(string userName, string password){
                     if(userName==null)return null;
                      if(userName.ToLower()=="halyhuang"){
                          var result=new user(){UserID="halyhuang"};
                          return result;
                      }else{
                          return null;
                      }
        }
    }
    public class user:IIdentity{
        public string UserID{get;set;}
        public string AuthenticationType { get;set; }
        public bool IsAuthenticated { get; set;}
        public string Name { get;set; }
    }
}