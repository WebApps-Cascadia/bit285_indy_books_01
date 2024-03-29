﻿namespace IndyBooks
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder InitializeDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<IndyBooks.Models.IndyBooksDataContext>();
                SeedData.Initialize(context);
            }
            catch (Exception e)
            {
                throw;
            }

            return app;
        }
    }
}
