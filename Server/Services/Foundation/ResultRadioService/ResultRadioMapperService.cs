using DTO;
using Server.Managers.Storages.RadioResultManager;
using Server.Models.RadioMedical;
using Server.Models.ResultsRadio;

namespace Server.Services.Foundation.ResultRadioService
{
    public static class ResultRadioMapperService
    {
        public static ResultRadio MapperToResultRadio(Guid RadioId,string TypeFileUpload,RadioResultToAddDto resultToAddDto)
        {
            return new ResultRadio
            {
                Id=Guid.NewGuid(),
                FileResult=resultToAddDto.FileUpload,
                IdRadio=RadioId,
                fileType=TypeFileUpload
                 
            };
        }
    }
}
