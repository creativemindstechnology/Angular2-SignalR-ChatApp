using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Angular2SignalR.HubConnections
{
    [HubName("chatMessaging")]
    public class ChatMessagingHub : Hub
    {        
        public async Task JoinGroup(User user)
        {
            await Groups.Add(Context.ConnectionId, user.GroupName);
            
            user.IsMember = true;
            
            Clients.Client(user.UserId).onGroupAccepted(user);

            //Notify Members
            Clients.Group(user.GroupName, user.UserId).notifyMembers(user);            
        }

        public void NotifyMembers(User user)
        {
            Clients.Group(user.GroupName, user.UserId).notifyNewMembers(user);
        }

        public void AddMember(IEnumerable<User> members)
        {
            Clients.Group(members.First().GroupName).onMemberAdded(members);
        }

        public void SendMessage(MessageData message)
        {
            message.DateCreated = DateTime.Now;
            Clients.Group(message.UserInfo.GroupName).onRecieved(message);
        }

        public void OldMessages(IEnumerable<MessageData> oldMessages)
        {            
            Clients.Group(oldMessages.First().UserInfo.GroupName).loadOldMessages(oldMessages);
        }


        public async Task LeaveGroup(User user)
        {
            await Groups.Remove(user.UserId, user.GroupName);

            user.IsMember = false;

            Clients.Caller.onGroupLeave(user);
            Clients.Group(user.GroupName).onGroupLeave(user);
        }

        public override Task OnConnected()
        {
            Trace.WriteLine(Context.ConnectionId + " - connected");

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Trace.WriteLine(Context.ConnectionId + " - disconnected");            
            Clients.AllExcept(Context.ConnectionId).onDisconnected(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Trace.WriteLine(Context.ConnectionId + " - reconnected");
            return base.OnReconnected();
        }
    }


    public class MessageData
    {
        public User UserInfo { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Photo
        {
            get { return HashEmailForGravatar(this.Email); }          
        }
        public string GroupName { get; set; }
        public bool IsMember { get; set; }
        
        private string HashEmailForGravatar(string email)
        {
            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Create a new instance of the MD5CryptoServiceProvider object.                   
            using (MD5 md5Hasher = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.  
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

                // Loop through each byte of the hashed data  
                // and format each one as a hexadecimal string.  
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}