namespace Server.Services.Foundation.ResultRadioService
{
    public partial class ResultRadioService
    {
        public delegate Task OnAddResultRadio();
        public async Task TryCatch(OnAddResultRadio onAddResultRadio)
        {
            try
            {
                await onAddResultRadio();
            }
            catch ()
            {

            }
            catch ()
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
