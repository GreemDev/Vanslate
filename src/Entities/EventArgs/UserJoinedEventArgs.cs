using Discord.WebSocket;
using System;

namespace Vanslate.Entities
{
    public sealed class UserJoinedEventArgs : EventArgs
    {
        public SocketGuildUser User { get; }
        public SocketGuild Guild { get; }

        public UserJoinedEventArgs(SocketGuildUser user)
        {
            User = user;
            Guild = user.Guild;
        }
    }
}