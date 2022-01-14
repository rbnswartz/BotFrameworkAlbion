using AdaptiveExpressions.Properties;
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
    public class GetAlbionPlayerInformation: Dialog
    {
        [JsonConstructor]
        public GetAlbionPlayerInformation([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0): base()
        {
            RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        }
        [JsonProperty("$kind")]
        public const string Kind = nameof(GetAlbionPlayerInformation);
        [JsonProperty("UserId")]
        public StringExpression UserId { get; set; }
        [JsonProperty("PvPFame")]
        public StringExpression PvPFame { get; set; }
        [JsonProperty("PvEFame")]
        public StringExpression PvEFame { get; set; }
        [JsonProperty("CraftingFame")]
        public StringExpression CraftingFame { get; set; }
        [JsonProperty("GatheringFame")]
        public StringExpression GatheringFame { get; set; }
        [JsonProperty("FameRatio")]
        public StringExpression FameRation { get; set; }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            var client = new AlbionOnlineApiClient();
            var userId = UserId.GetValue(dc.State);
            var player = await client.GetPlayerAsync(userId);
            if (player != null)
            {
                SafeSetOutput(dc, this.PvPFame, player.KillFame);
                SafeSetOutput(dc, this.PvEFame, player.LifetimeStatistics.PvE.Total);
                SafeSetOutput(dc, this.FameRation, player.FameRatio);
                SafeSetOutput(dc, this.GatheringFame, player.LifetimeStatistics.Gathering.All.Total);
                SafeSetOutput(dc, this.CraftingFame, player.LifetimeStatistics.Crafting.Total);
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
