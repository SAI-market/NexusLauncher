using NexusLauncher.DAL;
using NexusLauncher.Models;
using System.Collections.Generic;

namespace NexusLauncher.BLL
{
    public class FriendService
    {
        private readonly FriendshipDAL _friendDal;
        private readonly MessageDAL _messageDal;

        public FriendService()
        {
            _friendDal = new FriendshipDAL();
            _messageDal = new MessageDAL();
        }

        public List<FriendViewModel> GetFriendsForUser(int userId) =>
            _friendDal.GetFriendsForUser(userId);

        public List<FriendViewModel> GetAllUsersWithFriendStatus(int currentUserId) =>
            _friendDal.GetAllUsersWithFriendStatus(currentUserId);

        public bool SendFriendRequest(int fromUserId, int toUserId) =>
            _friendDal.CreateFriendRequest(fromUserId, toUserId);

        public List<FriendRequestDto> GetReceivedRequests(int currentUserId) =>
            _friendDal.GetReceivedRequests(currentUserId);

        public bool AcceptRequest(int friendshipId, int currentUserId) =>
            _friendDal.AcceptRequest(friendshipId);

        public bool RejectRequest(int friendshipId, int currentUserId) =>
            _friendDal.RejectRequest(friendshipId);

        // Mensajería - wrappers sencillos
        public int SendMessage(Message m) => _messageDal.CreateMessage(m);
        public List<Message> GetConversation(int a, int b, int limit = 100) => _messageDal.GetConversation(a, b, limit);
        public List<Message> GetUnreadMessages(int userId) => _messageDal.GetUnreadMessagesForUser(userId);
        public bool MarkMessageRead(int messageId) => _messageDal.MarkAsRead(messageId);
    }
}
