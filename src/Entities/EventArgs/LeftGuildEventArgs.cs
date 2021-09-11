using System;
using Discord;
using Discord.WebSocket;

namespace Vanslate.Entities
{
    public sealed class LeftGuildEventArgs : EventArgs
    {
        public SocketGuild Guild { get; }

        public LeftGuildEventArgs(SocketGuild guild) => Guild = guild;
    }
}