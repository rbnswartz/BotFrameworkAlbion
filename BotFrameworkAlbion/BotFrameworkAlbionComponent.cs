using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFrameworkAlbion
{
    public class BotFrameworkAlbionComponent: BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DeclarativeType>(sp => new DeclarativeType<SearchAlbionPlayer>(SearchAlbionPlayer.Kind));
        }

    }
}
