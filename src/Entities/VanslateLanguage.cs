using System.Linq;
using DeepL;
using Discord;
using Gommon;
using Vanslate.Helpers;

namespace Vanslate.Entities
{
    public record VanslateLanguage
    {
        public static readonly VanslateLanguage German 
            = new(Language.German, "🇩🇪".ToEmoji(), "DE");

        public static readonly VanslateLanguage English 
            = new(Language.English, "🇬🇧".ToEmoji(), "EN");

        public static readonly VanslateLanguage French 
            = new(Language.French, "🇫🇷".ToEmoji(), "FR");

        public static readonly VanslateLanguage Italian 
            = new(Language.Italian, "🇮🇹".ToEmoji(), "IT");

        public static readonly VanslateLanguage Japanese
            = new(Language.Japanese, "🇯🇵".ToEmoji(), "JA");

        public static readonly VanslateLanguage Spanish
            = new(Language.Spanish, "🇪🇸".ToEmoji(), "ES");

        public static readonly VanslateLanguage Dutch
            = new(Language.Dutch, "🇳🇱".ToEmoji(), "NL");

        public static readonly VanslateLanguage Polish
            = new(Language.Polish, "🇵🇱".ToEmoji(), "PL");

        public static readonly VanslateLanguage Portuguese
            = new(Language.Portuguese, "🇵🇹".ToEmoji(), "PT");

        public static readonly VanslateLanguage Russian
            = new(Language.Russian, "🇷🇺".ToEmoji(), "RU");

        public static readonly VanslateLanguage Chinese
            = new(Language.Chinese, "🇨🇳".ToEmoji(), "ZH");

        public static readonly VanslateLanguage Bulgarian
            = new(Language.Bulgarian, "🇧🇬".ToEmoji(), "BG");

        public static readonly VanslateLanguage Czech
            = new(Language.Czech, "🇨🇿".ToEmoji(), "CS");

        public static readonly VanslateLanguage Danish
            = new(Language.Danish, "🇩🇰".ToEmoji(), "DA");

        public static readonly VanslateLanguage Greek
            = new(Language.Greek, "🇬🇷".ToEmoji(), "EL");

        public static readonly VanslateLanguage Estonian 
            = new(Language.Estonian, "🇪🇪".ToEmoji(), "ET");

        public static readonly VanslateLanguage Finnish
            = new(Language.Finnish, "🇫🇮".ToEmoji(), "FI");

        public static readonly VanslateLanguage Hungarian
            = new(Language.Hungarian, "🇭🇺".ToEmoji(), "HU");

        public static readonly VanslateLanguage Lithuanian
            = new(Language.Lithuanian, "🇱🇹".ToEmoji(), "LT");

        public static readonly VanslateLanguage Latvian
            = new(Language.Latvian, "🇱🇻".ToEmoji(), "LV");

        public static readonly VanslateLanguage Romanian
            = new(Language.Romanian, "🇷🇴".ToEmoji(), "RO");

        public static readonly VanslateLanguage Slovak
            = new(Language.Slovak, "🇸🇰".ToEmoji(), "SK");

        public static readonly VanslateLanguage Slovenian
            = new(Language.Slovenian, "🇸🇮".ToEmoji(), "SL");

        public static readonly VanslateLanguage Swedish
            = new(Language.Swedish, "🇸🇪".ToEmoji(), "SV");

        public static readonly VanslateLanguage[] All =
        {
            German, English, French, Italian, Japanese, Spanish, Dutch, Polish,
            Portuguese, Russian, Chinese, Bulgarian, Czech, Danish, Greek, Estonian,
            Finnish, Hungarian, Lithuanian, Latvian, Romanian, Slovak, Slovenian, Swedish
        };

        public static VanslateLanguage Parse(string nameOrShorthand) => All.FirstOrDefault(x =>
            x.DeeplLang.ToString().EqualsIgnoreCase(nameOrShorthand) || x.Shorthand.EqualsIgnoreCase(nameOrShorthand)
        );


        public VanslateLanguage(Language lang, Emoji emoji, string shorthand)
        {
            DeeplLang = lang;
            Emoji = emoji;
            Shorthand = shorthand;
        }

        public Language DeeplLang { get; }
        public Emoji Emoji { get; }
        public string Shorthand { get; }
    }
}