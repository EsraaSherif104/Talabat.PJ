namespace Talabt.APIS.Extention
{
    public static class AddSwaggerExtention
    {
        public static WebApplication UseSwaggerMiddelWire (this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
