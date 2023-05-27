using Client.Services.Exceptions;
using Client.Services.Foundations.CityService;
using Client.Services.Foundations.LocalStorageService;
using Client.Services.Foundations.UserService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Client.Pages
{
    public class HomeComponentBase : ComponentBase
    {
        protected int CityIdSelected = 7;
        protected string Entry = null;
        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected string Index = null;
        protected List<CityDto> CityList = new List<CityDto>(); 
        protected List<DoctorSearchDto> ListDoctorsAvailbleTest = null;
        protected List<DoctorSearchDto> ListDoctorsAvailble = new List<DoctorSearchDto>();
        public DoctorSearchDto DoctorsAvailble = null;
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public ICityService cityService { get; set; }   
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public ILocalStorageServices localStorageServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.IsLoading = true;
                this.CityList = await this.cityService.GetAllCities();
                this.ListDoctorsAvailble = await this.userService.GetListDoctorAvailble(CityIdSelected);
                this.ListDoctorsAvailbleTest = this.ListDoctorsAvailble;
                this.IsLoading = false;

            }
            catch (BadRequestException e)
            {
                this.ErrorMessage = e.Message;
            }
        }
        protected async Task  OnSelectCity(ChangeEventArgs e)
        {
            int cityId = 0;
            int.TryParse(e.Value.ToString(), out CityIdSelected);
            this.IsLoading = true;
            this.ListDoctorsAvailble = await this.userService.GetListDoctorAvailble(CityIdSelected);
            this.ListDoctorsAvailbleTest = this.ListDoctorsAvailble;
            this.IsLoading = false;
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
                this.ListDoctorsAvailble = this.ListDoctorsAvailble.Where(e => e.LastName.ToString().ToLower().StartsWith(this.Entry.ToLower()) || e.FirstName.ToString().ToLower().StartsWith(this.Entry.ToLower())).ToList();
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
