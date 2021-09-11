using System;
using Discord;
using Discord.WebSocket;

namespace Vanslate.Entities
{
    public sealed class JoinedGuildEventArgs : EventArgs
    {
        public SocketGuild Guild { get; }

        public JoinedGuildEventArgs(SocketGuild guild) => Guild = guild;
    }
}