using DTO;
using Server.Models.CabinetMedicals;

namespace Server.Services.Foundation.CabinetMedicalService
{
    public static class CabinetMedicalMapperService
    {
        public static CabinetMedicalDto MapperToCabinetMedicalDto(CabinetMedical cabinetMedical)
        {
            return new CabinetMedicalDto
            {
                Adress = cabinetMedical.Adress,
                Image = cabinetMedical.image,
                JobTime = cabinetMedical.JobTime,
                mapAdress = cabinetMedical.MapAdress,
                name = cabinetMedical.NameCabinet,
                phoneNumber = cabinetMedical.numberPhone,
                Services = cabinetMedical.Services,
                Status = (StatusCabinet)cabinetMedical.statusService



            };
        }
        public static CabinetMedical MapperToNewCabinetMedical(this CabinetMedicalDto cabinetMedicalDto, CabinetMedical cabinetMedical)
        {
            cabinetMedical.NameCabinet = cabinetMedicalDto.name;
            cabinetMedical.JobTime = cabinetMedicalDto.JobTime;
            cabinetMedical.Services = cabinetMedicalDto.Services;
            cabinetMedical.statusService = (StatusService)cabinetMedicalDto.Status;
            cabinetMedical.Adress = cabinetMedicalDto.Adress;
            cabinetMedical.numberPhone = cabinetMedicalDto.phoneNumber;
            cabinetMedical.MapAdress = cabinetMedicalDto.mapAdress;
            cabinetMedical.image = cabinetMedicalDto.Image;
            return cabinetMedical;
        }
    }
}
