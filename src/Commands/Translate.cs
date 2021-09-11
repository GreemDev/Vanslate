using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepL;
using Discord;
using Gommon;
using Vanslate.Entities;
using Vanslate.Helpers;
using Vanslate.Interactions;

namespace Vanslate.Commands
{
    public sealed class TranslateCommand : ApplicationCommand
    {
        public sealed class MessageCommand : ApplicationCommand
        {
            public MessageCommand() : base("Translate This", ApplicationCommandType.Message) { }

            public override Task<bool> RunMessageChecksAsync(MessageCommandContext ctx) => Task.FromResult(ctx.User.IsVip());

            public override async Task HandleMessageCommandAsync(MessageCommandContext ctx)
            {
                var reply = ctx.CreateReplyBuilder(true);
                if (!await RunMessageChecksAsync(ctx))
                    reply.WithEmbed(x => x.WithTitle($"{DiscordHelper.X} You can't use this."));
                else
                {
                    reply.WithEmbed(x => x.AddField("Content", Format.Code(ctx.UserMessage.Content, string.Empty)))
                        .WithSelectMenu(new SelectMenuBuilder()
                            .WithCustomId("translate this:menu")
                            .WithPlaceholder("Select a language...")
                            .WithOptions(VanslateLanguage.All
                                .Select(lang => new SelectMenuOptionBuilder()
                                    .WithLabel($"{lang.Shorthand} - {lang.DeeplLang}")
                                    .WithEmote(lang.Emoji)
                                    .WithValue(lang.Shorthand)
                                ).ToList()));
                }

                await reply.RespondAsync();
            }

            public override async Task HandleComponentAsync(MessageComponentContext ctx)
            {
                if (ctx.CustomIdParts[1] != "menu")
                    await ctx.DeferAsync(true);
                else
                {
                    var lang = VanslateLanguage.Parse(ctx.SelectedMenuOptions.First());
                    var translation = await ctx.DeepL.TranslateAsync(
                        ctx.Message.Embeds.First().Fields.First().Value.Replace("`", ""),
                            lang.DeeplLang);

                    await ctx.CreateReplyBuilder(true)
                        .WithEmbeds(ctx.CreateEmbedBuilder(Format.Code(translation.Text, string.Empty))
                            .WithFooter($"Detected language: {translation.DetectedSourceLanguage}"))
                        .RespondAsync();
                }
            }
        }
        
        public TranslateCommand() : base("translate", "Translate text into a desired language.") { }

        public override async Task HandleSlashCommandAsync(SlashCommandContext ctx)
        {
            var lang = VanslateLanguage.Parse(ctx.Options["language"].GetAsString());
            var translation = await ctx.DeepL.TranslateAsync(ctx.Options["text"].GetAsString(), lang.DeeplLang);
            await ctx.CreateReplyBuilder(true)
                .WithEmbed(e => e.WithTitle("Translation")
                    .WithDescription(Format.Code(translation.Text))
                    .WithFooter($"Detected language: {translation.DetectedSourceLanguage}"))
                .RespondAsync();


        }

        public override SlashCommandSignature GetSignature(IServiceProvider provider)
            => SlashCommandSignature.Command(o =>
            {
                o.RequiredString("text", "The text to translate.");
                o.RequiredString("language", "The language to translate the text to.", b =>
                {
                    VanslateLanguage.All.ForEach(x => b.AddChoice(x.DeeplLang.ToString(), x.Shorthand));
                });
            });
    }
}