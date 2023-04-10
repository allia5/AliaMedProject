using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using Client.Services.Foundations.UserService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Client.Pages
{
    public class HomeComponentBase : ComponentBase
    {
        protected string Entry = null;
        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected string Index = null;
        protected List<DoctorSearchDto> ListDoctorsAvailbleTest = null;
        protected List<DoctorSearchDto> ListDoctorsAvailble = new List<DoctorSearchDto>();
        // protected List<CabinetSearchDto> ListCabinetSearch = new List<CabinetSearchDto>();
        public DoctorSearchDto DoctorsAvailble = null;
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public ILocalStorageServices localStorageServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.ListDoctorsAvailble = await this.userService.GetListDoctorAvailble();
                this.ListDoctorsAvailbleTest = this.ListDoctorsAvailble;
                this.IsLoading = false;

            }
            catch (BadRequestException e)
            {
                this.ErrorMessage = e.Message;
            }
        }
        public async Task OnOpenInformation(string IdUser)
        {
            this.DoctorsAvailble = new DoctorSearchDto();
            var Doctor = this.ListDoctorsAvailble.Where(e => e.Id == IdUser).First();
            this.DoctorsAvailble = Doctor;


        }
        protected async Task OnSearch(ChangeEventArgs args)
        {
            this.ListDoctorsAvailble = ListDoctorsAvailbleTest;
            Entry = (string)args.Value;
            if (Entry.IsNullOrEmpty())
            {
                this.ListDoctorsAvailble = ListDoctorsAvailbleTest;
            }
            else
            {
                this.ListDoctorsAvailble = this.ListDoctorsAvailble.Where(e => e.LastName.ToString().StartsWith(this.Entry) || e.FirstName.ToString().StartsWith(this.Entry)).ToList();
            }

            
        }
        public async Task OnSelctionReservation(string IdUserDoctor, string IdCabinet, string IdJob)
        {
            try
            {
                if (!IsInvalid(IdUserDoctor) && !IsInvalid(IdCabinet) && !IsInvalid(IdJob))
                {
                    await this.localStorageServices.SetItemAsync<KeysReservationMedicalDto>("KeysReservationMedical", new KeysReservationMedicalDto { IdCabinet = IdCabinet, IdJob = IdJob, IdUserDoctor = IdUserDoctor });
                    this.navigationManager.NavigateTo("/PlanningMedicalInformation", forceLoad: true);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }


        }
        public static bool IsInvalid(string Entry)
        {
            if (Entry.IsNullOrEmpty())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
