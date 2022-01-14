using AdaptiveExpressions.Properties;
using BotFrameworkAlbion.Models;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotFrameworkAlbion
{
    internal class SearchAlbionPlayer: Dialog
    {
        [JsonConstructor]
        public SearchAlbionPlayer([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0): base()
        {
            RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        }
        [JsonProperty("$kind")]
        public const string Kind = nameof(SearchAlbionPlayer);
        [JsonProperty("Username")]
        public StringExpression Username { get; set; }
        [JsonProperty("UserFound")]
        public StringExpression UserFound { get; set; }
        [JsonProperty("UserId")]
        public StringExpression UserId { get; set; }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            var username = Username.GetValue(dc.State);
            var client = new AlbionOnlineApiClient();
            var result = await client.SearchAsync(username);
            Player player = null;
            foreach(var i in result.players)
            {
                if (i.Name == username)
                {
                    player = i;
                }
            }
            if (this.UserFound != null)
            {
                dc.State.SetValue(this.UserFound.GetValue(dc.State), player != null);
            }
            if (player != null)
            {
                SafeSetOutput(dc, this.UserId, player.Id);
            }
            return await dc.EndDialogAsync();
        }
        private void SafeSetOutput(DialogContext dc, StringExpression input, object value)
        {
            if (input == null)
            {
                return;
            }
            dc.State.SetValue(input.GetValue(dc.State), value);
        }
    }
}
