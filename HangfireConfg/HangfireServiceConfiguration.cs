using Hangfire;
using Hangfire.Console;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

namespace HangfireProject.HangfireConfg
{
    public static class HangfireService
    {
        public static void ConfigureHangfireService(this WebApplicationBuilder builder)
        {
            var mongoUrlBuilder = new MongoUrlBuilder(builder.Configuration.GetValue<string>("ConnectionStrings:HangfireConnection"));
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseConsole()
                .UseMongoStorage(mongoClient, "hangfire", new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new MigrateMongoMigrationStrategy(),
                        BackupStrategy = new CollectionMongoBackupStrategy()
                    },
                    Prefix = "hangfire.hangfire",
                    CheckConnection = false
                })
            );

            builder.Services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Hangfire";
            });
        }
    }

}
